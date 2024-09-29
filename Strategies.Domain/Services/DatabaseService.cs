using System.Diagnostics;
using System.Linq.Expressions;
using Strategies.Domain.Persistence;

namespace Strategies.Domain;

public class DatabaseService
{
    // private readonly IUnitOfWork _unitOfWork;
    private readonly Supabase.Client _supabaseClient;
    public DatabaseService(IUnitOfWork unitOfWork, Supabase.Client supabaseClient)
    {
        // _unitOfWork = unitOfWork;
        _supabaseClient = supabaseClient;
    }

    #region GetData method explanation
    // Case 1:For hourly resolutions, the data downloaded from cryptodatadownload.com can be used and covers the entire trading history. Therefore, there is no need to combine it with other data sources.
    // Case 2: For large resolutions (e.g. 1W) or young coins, the data downloaded from TradingView (March 2024) is sufficient. 
    //
    // Case 3: However, for other small resolutions (e.g. 3h), data has to be combined from 2 sources - TradingView (June 2022) and cryptodatadownload.com (February 2024).
    //     Data from June 2022:     coin's inception <-------------------------------------> June 2022
    //                                                                      <---overlap---->
    //     Data from March 2024:                                       2020 <---------------------------------> March 2024
    //
    // The method GetData will return Candle objects from the database by combining data from different sources when necessary. It will return the most up-to-date data first.
    // GetData processes the groups in groupedCandles based on the DateDownloaded property of the Candle objects, in descending order. This means that the group with the most recent DateDownloaded value is processed first, followed by the next most recent, and so on. Here's how it works step by step:
    // 1. Data Retrieval: The method retrieves all Candle objects from the database that match the specified symbol and resolution.
    // 2. Grouping: The Candle objects are then grouped by their DateDownloaded property. This means all Candle objects with the same DateDownloaded value will be in the same group.
    // 3. Ordering of Groups: These groups are ordered in descending order by their DateDownloaded value. Therefore, the group containing the Candle objects with the most recent DateDownloaded value is placed first in the groupedCandles collection.
    // 4. Processing of Groups: In the outer foreach loop, the groups are iterated over in this descending order. Within each group, it then iterates over each Candle object (foreach (var candle in group)). For each Candle, it checks if a Candle with the same date doesn't already exist in combinedCandles. If not, it adds the Candle to combinedCandles.
    // 5. Result: The method returns the combinedCandles list, which contains Candle objects from different groups ensuring no duplicate dates are included, prioritizing Candle objects from the most recently downloaded groups.
    // In summary, the groupedCandles are processed starting from the group with the most recent DateDownloaded to the least recent, ensuring that the most up-to-date data is considered first in combinedCandles.
    #endregion
    public async Task<List<Candle>> GetCombinedOHLC(string symbol, string resolution, DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        if (startDateTime == null) startDateTime = new DateTime(2000, 1, 1);
        if (endDateTime == null) endDateTime = new DateTime(2099, 1, 1);

        IQueryable<Candle>? allCandles;

        // Create a named function to resuse in this method
        async Task<IQueryable<Candle>> getCandlesAsync()
        {
            var data = await _supabaseClient.From<SupabaseCandle>()
                .Where(c => c.Symbol == symbol)
                .Where(c => c.Resolution == resolution)
                .Get();


            // Convert the SupabaseCandle object to a Candle object using the explicit operator
            var candles = data.Models.Select(c => (Candle)c).AsQueryable();

            return candles;
        }

        // For hourly resolutions, the data downloaded from cryptodatadownload.com can be used and covers the entire trading history. Therefore, there is no need to combine it with other data sources.
        if (resolution == "60")
        {
            var supabaseCandles = await getCandlesAsync();

            // Only use data downloaded from cryptodatadownload.com/data/binance/ (downloaded in February 2024)
            allCandles = supabaseCandles.Where(c => c.DateDownloaded.Month == 2 && c.DateDownloaded.Year == 2024);
        }
        // else if (resolution == "2D")
        // {
        //     // Only use data downloaded from TradingView in June 2022
        //     Expression<Func<Candle, bool>> predicate = c =>
        //         c.Symbol == symbol &&
        //         c.Resolution == resolution &&
        //         c.DateDownloaded.Year == 2022;

        //     allCandles = getCandles(predicate);
        // }
        else if (resolution == "360" || resolution == "720" || resolution == "1D" || resolution == "3D" || resolution == "1W")
        {
            var supabaseCandles = await getCandlesAsync();

            // Only use data downloaded from TradingView in March 2024
            allCandles = supabaseCandles.Where(c => c.DateDownloaded.Year == 2024);
        }
        else
        {
            // Only for resolution of 180
            // Get the data from the database by combining data from different sources when necessary.
            // NOTE: Not all coins were downloaded BOTH in 2022 and March 2024. Those that were downloaded once (in either date) won't need to be combined.

            IQueryable<Candle> candlesQueryable = await getCandlesAsync();
            List<Candle> candles = candlesQueryable.ToList();

            var groupedCandles = candles
                .GroupBy(candle => candle.DateDownloaded)
                .OrderByDescending(group => group.Key); // Most recent first

            List<Candle> combinedCandles = [];

            // Using a HashSet to keep track of the dates of the candles in the combinedCandles list dramatically improves performance.
            // Using LINQ combinedCandles.Any(c => c.Date == candle.Date), the nested foreach loop took 2 seconds.
            // Using a HashSet combinedCandlesDates.Contains(candle.Date), the nested foreach loop took 0.002 seconds.
            var combinedCandlesDates = new HashSet<DateTime>(combinedCandles.Select(c => c.Date));
            bool isFirstIteration = true;
            foreach (var group in groupedCandles)
            {
                // Add the first group of candles (i.e. the most recent source of data) to the combinedCandles list and skip to the next group
                if (isFirstIteration)
                {
                    combinedCandles.AddRange(group);
                    combinedCandlesDates.UnionWith(group.Select(c => c.Date));
                    isFirstIteration = false;
                    continue;
                }

                // If there is a gap between the date ranges of the sources, break the loop
                if (HasDateGap(group, combinedCandles, resolution)) break;

                foreach (var candle in group)
                {
                    // Add candle to combinedCandles if it doesn't already exist for that date
                    if (!combinedCandlesDates.Contains(candle.Date)) // Using HashSet for performance
                    {
                        combinedCandles.Add(candle);
                        combinedCandlesDates.Add(candle.Date);
                    }
                }
            }

            allCandles = combinedCandles.AsQueryable();
        }

        
        return allCandles
            .OrderBy(c => c.Date)
            .Where(c => c.Date >= startDateTime && c.Date <= endDateTime)
            .ToList();
    }

    // When data from different sources is combined in order to cover as much as trding history as possible, this method checks if there is a gap between the date range of the sources.
    // There cannot be any gaps between the date ranges of the sources. 
    internal bool HasDateGap(IGrouping<DateTime, Candle>? group, List<Candle>? combinedCandles, string resolution)
    {
        if (group == null || combinedCandles == null) throw new ArgumentNullException();

        var latestCandle = group.OrderByDescending(c => c.Date).First();
        var oldestCandle = group.OrderBy(c => c.Date).First();
        var processedLatestCandle = combinedCandles.OrderByDescending(c => c.Date).FirstOrDefault();
        var processedOldestCandle = combinedCandles.OrderBy(c => c.Date).FirstOrDefault();

        var timeSpan = resolution switch
        {
            "60" => TimeSpan.FromMinutes(60),
            "180" => TimeSpan.FromMinutes(180),
            "360" => TimeSpan.FromMinutes(360),
            "720" => TimeSpan.FromMinutes(720),
            "1D" => TimeSpan.FromDays(1),
            // "2D" => TimeSpan.FromDays(2),
            "3D" => TimeSpan.FromDays(3),
            "1W" => TimeSpan.FromDays(7),
            _ => throw new Exception("Invalid resolution")
        };

        #region GapCondition method explanation
        // Senario 1: overlap
        //                    date2          date1
        //        <--------older source-------->
        //                     <--------newer source-------->
        //
        // Senario 2: no overlap
        // If inverval = resolution, then there is no gap.
        //                          date1                date2
        //        <---older source--->|<-----interval----->|<---newer source--->
        #endregion
        bool GapCondition(DateTime date1, DateTime date2)
        {
            TimeSpan interval = date2 - date1;
            if (interval > timeSpan || interval.Ticks % timeSpan.Ticks != 0
            )
                return true;
            else
                return false;
        }

        if (processedOldestCandle?.Date > latestCandle.Date && GapCondition(latestCandle.Date, processedOldestCandle.Date))
            return true;
        else if (oldestCandle.Date > processedLatestCandle?.Date && GapCondition(processedLatestCandle.Date, oldestCandle.Date))
            // Should theoretically not happen as the groups are ordered by DateDownloaded in descending order 
            return true;
        else
            return false;
    }

    async public Task<List<Trade>> GetSingleChartTrades(string symbol, string resolution, int strategy)
    {
        string resultsId = Utilities.GenerateResultsId(symbol, resolution, strategy, OutlierRemovalMethods.None);

        // Get all records who's foreign key is the resultsId from the Trades table
        // var trades = _unitOfWork.Repository<Trade>().GetAllQueryableAsNoTracking().Where(t => t.ResultsId == resultsId).ToList();
        var tradesResponse = await _supabaseClient.From<SupabaseTrade>()
            .Where(t => t.ResultsId == resultsId)
            .Get();

        // Convert the SupabaseTrade objects to Trade objects using the explicit operator
        List<Trade> trades = tradesResponse.Models.Select(t => (Trade)t).ToList();

        if (trades.Count == 0) return [];

        return trades;
    }



    async public Task<Results> GetSingleChartResults(string symbol, string resolution, int strategy, string outlierRemovalMethod)
    {
        var resultsResponse = await _supabaseClient.From<SupabaseResults>()
            .Where(r => r.Symbol == symbol)
            .Where(r => r.Resolution == resolution)
            .Where(r => r.Strategy == strategy)
            .Get();

        // Convert the SupabaseResults object to a Results object using the explicit operator
        List<Results> mixedResults = resultsResponse.Models.Select(r => (Results)r).ToList();

        Results? results = mixedResults.FirstOrDefault(r => r.OutlierRemovalMethod == outlierRemovalMethod);

        if (results == null) return new Results();

        return results;
    }

    async public Task<IQueryable<Results>> GetAllResults()
    {
        var resultsResponse = await _supabaseClient.From<SupabaseResults>().Get();

        // Convert the SupabaseResults object to a Results object using the explicit operator
        IQueryable<Results> results = resultsResponse.Models.Select(r => (Results)r).AsQueryable();

        if (results == null) return null;

        return results;
    }

    async public Task<List<Results>> GetResultsByIds(List<int> ids)
    {
        var resultsResponse = await _supabaseClient.From<SupabaseResults>()
            .Get();
        
        // Convert the SupabaseResults object to a Results object using the explicit operator
        var results = resultsResponse.Models.Select(r => (Results)r).Where(r => ids.Contains(r.Strategy)).ToList();

        if (results == null) return [];

        return results;
    }

    async public Task<IQueryable<Trade>> GetAllTrades()
    {
        var tradesResponse = await _supabaseClient.From<SupabaseTrade>().Where(t => t.Resolution != "60").Get();
        var trades = tradesResponse.Models.Select(t => (Trade)t).AsQueryable();

        if (trades == null) return null;

        return trades;
    }
}