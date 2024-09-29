using System.Text.RegularExpressions;
using Strategies.Domain.Persistence;
using Strategies.Domain.Presentation;

namespace Strategies.Domain;

public class ChartDataAnalyzer
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageService _messageService;
    public ChartDataAnalyzer(IUnitOfWork unitOfWork, IMessageService messageService)
    {
        _unitOfWork = unitOfWork;
        _messageService = messageService;
    }

    // === PURPOSE ===
    // This is the only method in this class. The purpose is to make sure that the data downloaded from TradingView in June 2022 and March 2024 is consistent.
    // Data downloaded in June 2022 span from a coin's inception to June 2022.
    // Data downloaded in March 2024 spans from 10K candles back to March 2024. For high resolution data (e.g. 3h), the data will start from 2020 only.
    // This means that for high resolution data (e.g. 3h), there will be an overlap of data from 2020 to June 2022.
    // Since the data was downloaded from TradingView for Binance, the data should be the same.
    // This is what this method checks by printing in the console the differences in the OHLCV values for the overlapping candles.
    // All data must first be saved in the database.
    public void CheckOverlappingCandles()
    {
        // Get a list of all unique combinations of symbol, exchange, resolution and datedownloaded
        var uniqueCombinations = _unitOfWork.Repository<Candle>().GetAllQueryableAsNoTracking()
            .Select(c => new { c.Symbol, c.Exchange, c.Resolution, c.DateDownloaded })
            .Distinct()
            .ToList();

        // In uniqueCombinations, create all possible pairs of uniqueCombinations and filter to keep those that have the same symbol, exchange, resolution but different datedownloaded.
        var overlappingCandles = uniqueCombinations
            .SelectMany((c1, i) => uniqueCombinations.Skip(i + 1), (c1, c2) => new { c1, c2 })
            .Where(c => c.c1.Symbol == c.c2.Symbol && c.c1.Exchange == c.c2.Exchange && c.c1.Resolution == c.c2.Resolution && c.c1.DateDownloaded != c.c2.DateDownloaded)
            .ToList();

        foreach (var x in overlappingCandles)
        {
            _messageService.WriteLine($"{x.c1.Symbol} - {x.c1.Exchange} - {x.c1.Resolution} downloaded on {x.c1.DateDownloaded} and {x.c2.DateDownloaded}");
        }

        // // If there are any overlapping candles, print a message to the console
        // if (overlappingCandles.Count > 0)
        // {
        //     foreach (var pair in overlappingCandles)
        //     {
        //         Console.WriteLine($"The database contains data for {pair.c1.Symbol} on {pair.c1.Exchange} with resolution {pair.c1.Resolution} downloaded on {pair.c1.DateDownloaded} and {pair.c2.DateDownloaded}");
        //     }
        // }
        // else
        // {
        //     Console.WriteLine("No overlapping candles found in the database");
        // }

        // print the number of pairs
        _messageService.WriteLine($"The database contains {overlappingCandles.Count} pairs of overlapping candles");

        int i = 1;
        foreach (var pair in overlappingCandles)
        {
            // For 1h resolution, data from cryptodatadownload.com can be used and covers the entire trading hisotry. Therefore, no need to combine with other data source.
            if (pair.c1.Resolution == "60")
            {
                i++;
                continue;
            }

            // Example: 1/3) Checking pair: BTCUSDT - Binance - 1D downloaded on 2022-06-29 and 2022-06-30
            _messageService.WriteLine($"{i}/{overlappingCandles.Count}) Checking pair: {pair.c1.Symbol} - {pair.c1.Exchange} - {pair.c1.Resolution} downloaded on {pair.c1.DateDownloaded} and {pair.c2.DateDownloaded}");

            // For each uniqueCombinations is a pair, retrieve the candles from the database and save them in a variable
            var candles1 = _unitOfWork.Repository<Candle>().GetAllQueryableAsNoTracking()
                .Where(c => c.Symbol == pair.c1.Symbol && c.Exchange == pair.c1.Exchange && c.Resolution == pair.c1.Resolution && c.DateDownloaded == pair.c1.DateDownloaded)
                .ToList();
            var candles2 = _unitOfWork.Repository<Candle>().GetAllQueryableAsNoTracking()
                .Where(c => c.Symbol == pair.c2.Symbol && c.Exchange == pair.c2.Exchange && c.Resolution == pair.c2.Resolution && c.DateDownloaded == pair.c2.DateDownloaded)
                .ToList();

            // Join the two lists of candles on the date.
            // Ignore the last candle in each list as it may not be complete.
            var matched = candles1.Take(candles1.Count() - 1)
                .Join(
                    candles2.Take(candles2.Count() - 1),
                    candle1 => candle1.Date,
                    candle2 => candle2.Date,
                    (candle1, candle2) => new { candle1, candle2 }
                );

            // Loop through matched to check if the OHLCV values are the same
            int countDivergentCandles = 0;
            foreach (var match in matched)
            {
                if (match.candle1.Open != match.candle2.Open || match.candle1.High != match.candle2.High || match.candle1.Low != match.candle2.Low || match.candle1.Close != match.candle2.Close)
                {
                    countDivergentCandles++;

                    // _messageService.WriteLine($"[red]The following candles have different OHLC values[/]");
                    // _messageService.WriteLine($"[red]        {match.candle1.Date}, {match.candle1.Open}, {match.candle1.High}, {match.candle1.Low}, {match.candle1.Close}[/]");
                    // _messageService.WriteLine($"[red]        {match.candle2.Date}, {match.candle2.Open}, {match.candle2.High}, {match.candle2.Low}, {match.candle2.Close}[/]");
                }
            }
            if (countDivergentCandles > 0)
            {
                _messageService.WriteLine($"[red]{countDivergentCandles} candles have different OHLC values[/]");
            }

            i++;
        }
    }
}