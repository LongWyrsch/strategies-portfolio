using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Strategies.Domain.Persistence;
using Strategies.Domain.Presentation;

namespace Strategies.Domain;

// This class contains methods to read CSV files with chart data and import them into the database.
public class CsvChartDataReader
{
    private readonly IFileHandler _fileHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageService _messageService;
    private readonly Directories _directories;

    public CsvChartDataReader(IFileHandler fileHandler, IUnitOfWork unitOfWork, IMessageService messageService, Directories directories)
    {
        _fileHandler = fileHandler;
        _unitOfWork = unitOfWork;
        _messageService = messageService;
        _directories = directories;
    }

    public void ImportAllCsvToDatabase()
    {
        var directories = new (string path, string source)[]
        {
            (_directories.CryptodatadownloadHourData, "CandleCsvReaderMapper2024OneHour"),
            (_directories.TradingViewMarch2024Data, "CandleCsvReaderMapper2024"),
            (_directories.TradingViewJune2022Data, "CandleCsvReaderMapper2022")
        };

        foreach (var directory in directories)
        {
            ReadFirstLineAndCheckIfIdentical(directory.path);
            var files = Directory.EnumerateFiles(directory.path, "*.csv").ToArray();
            var allCandles = new ConcurrentBag<(List<Candle> Candles, string Exchange, string Symbol, string Resolution, DateTime DateDownloaded)>();

            Parallel.ForEach(files, filePath =>
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                // Extract the desired parts of the file name
                var parts = fileName.Split(new[] { ',', '_', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string exchange = parts[0].ToUpper(); // e.g. "BINANCE"
                string symbol = parts[1]; // e.g. "AAVEBTC"
                string resolution = parts[2] == "1h" ? "60" : parts[2]; // e.g. "180"

                // get the file's date created
                var dateCreated = File.GetCreationTime(filePath);

                // Process the CSV file
                var csvData = ReadCsv(filePath, directory.source);

                // Add the exchange, symbol, and resolution to each candle
                foreach (var candle in csvData)
                {
                    candle.Exchange = exchange;
                    candle.Symbol = symbol;
                    candle.Resolution = resolution;
                    candle.DateDownloaded = dateCreated;
                }

                // Collect all data first
                allCandles.Add((csvData, exchange, symbol, resolution, dateCreated));
            });

            // Insert all data into the database
            foreach (var groupedCandles in allCandles.GroupBy(x => (x.Exchange, x.Symbol, x.Resolution, x.DateDownloaded)))
            {
                // Combine all grouped candles for batch insert
                var combinedCandles = groupedCandles.SelectMany(g => g.Candles).ToList();
                var first = groupedCandles.First();

                SaveChartData(combinedCandles, first.Exchange, first.Symbol, first.Resolution, first.DateDownloaded);
            }
        }
    }

    public List<Candle> ReadCsv(string path, string source)
    {
        using var reader = new StreamReader(path);

        var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        using (csvReader)
        {
            // Use reflection to get the type by its name
            var mapType = Type.GetType("Strategies.Domain." + source);

            if (mapType == null)
            {
                throw new Exception("Invalid source.");
            }

            // Register the class map dynamically
            var registerMethod = typeof(CsvContext).GetMethod("RegisterClassMap", Type.EmptyTypes);
            var genericRegisterMethod = registerMethod.MakeGenericMethod(mapType);
            genericRegisterMethod.Invoke(csvReader.Context, null);

            IEnumerable<Candle> records = csvReader.GetRecords<Candle>();

            try
            {
                _messageService.WriteLine($"[[Background]] CSV read to memory: {Path.GetFileNameWithoutExtension(path)}.csv ✔️");
                return records.ToList();
            }
            catch (Exception ex)
            {
                _messageService.WriteLine($"[red]Error reading CSV file: {path}[/]");
                throw;
            }
        }
        // The StreamReader is automatically closed at the end of the using block.
    }

    public void SaveChartData(List<Candle> candles, string exchange, string symbol, string resolution, DateTime dateDownloaded)
    {
        var candleRepository = _unitOfWork.Repository<Candle>();

        // check if the database already contains data from the same chart (downloaded on the same date)
        var existingSimilarChartData = candleRepository.GetAllQueryableAsNoTracking()
            .Where(t => t.Symbol == symbol && t.Exchange == exchange && t.Resolution == resolution && t.DateDownloaded == dateDownloaded)
            .Select(t => new
            {
                t.Symbol,
                t.Exchange,
                t.Resolution,
                t.DateDownloaded
            })
            .Distinct()
            .ToList();

        // check if existingSimilarChartData contains anything
        if (existingSimilarChartData.Count > 0)
        {
            _messageService.WriteLine($"[[Background]] [red]The database already contains data for {symbol} on {exchange} with resolution {resolution} downloaded on {dateDownloaded}.[/]");
            return;
        }

        // Try catch block to handle invalid database updates
        try
        {
            candleRepository.BulkAddRange(candles);
            _unitOfWork.SaveChanges();
        }
        catch (Exception ex)
        {
            _messageService.WriteLine($"[red]Error saving data for {symbol}, {exchange}, {resolution}, downloaded on {dateDownloaded}.[/]");
            _messageService.WriteLine($"[red]{ex.Message}[/]");
            return;
        }

        _messageService.WriteLine($"[[Background]][green] {candles.Count} candles saved to database for {symbol}, {exchange}, {resolution}, downloaded on {dateDownloaded} ✔️[/]");
    }

    // When reading all data from a source into the database, it is assumed that all CSV have the same columns.
    // This method reads the first line of each CSV file in a directory and checks if they are identical.
    // It should therefore run before importing the data into the database.
    public void ReadFirstLineAndCheckIfIdentical(string directoryPath)
    {
        _fileHandler.CheckCryptomatorAvailability();

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Directory does not exist.");
            return;
        }

        // List to store the first line of each file
        List<string> firstLines = new List<string>();

        // Get all csv files in the directory
        string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");

        foreach (string file in csvFiles)
        {
            // Read the first line of the file
            string firstLine = File.ReadLines(file).FirstOrDefault();

            if (firstLine != null)
            {
                // Print the first line to the console
                Console.WriteLine($"First line of {Path.GetFileName(file)}: {firstLine}");

                // Replace /Volume \w*/ with /Volume/
                firstLine = Regex.Replace(firstLine, @"Volume \w*", "Volume");

                // Add the line to the list
                firstLines.Add(firstLine);
            }
        }

        // Check if all first lines are identical
        bool areAllIdentical = firstLines.Count > 0 && firstLines.Distinct().Count() == 1;

        if (areAllIdentical)
        {
            Console.WriteLine("All first lines are identical.");
        }
        else
        {
            Console.WriteLine("First lines are not identical.");
            throw new Exception("First lines are not identical.");
        }

    }
}