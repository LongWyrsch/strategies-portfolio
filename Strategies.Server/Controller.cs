using Microsoft.AspNetCore.Mvc;
using Strategies.Domain;
using Strategies.Server.DTO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Strategies.Controllers;


[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly DatabaseService _databaseService;
    protected readonly int[]StrategiesToInclude = [27,39,53,110,217,222,225,246,249];
    // protected readonly int[]StrategiesToInclude = [1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50];

    protected BaseController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
}

[Route("api/[controller]")]
[ApiController]
public class GetChartTradingDetailsController : BaseController
{
    public GetChartTradingDetailsController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    async public Task<ActionResult<List<ReturnChartTradingDetails>>> GetChartTradingDetails([FromQuery] string symbol, [FromQuery] string resolution, [FromQuery] int strategy, [FromQuery] string outlierRemovalMethod = "None")
    {
        // Get OHLC data to plot price chart
        var candles = await _databaseService.GetCombinedOHLC(symbol, resolution, new DateTime(2010, 1, 1), new DateTime(2099, 1, 1));
        if (candles == null || candles.Count() == 0)
        {
            return NotFound();
        }

        // Prepare a list of the same length as candles to show when trading occurred
        List<Trade> trades = await _databaseService.GetSingleChartTrades(symbol, resolution, strategy);
        List<double> tradingCandles = []; // Plotly trace showing when in trade.
        List<string> tradeData = []; // string with data about 5the trade on the last candle of the trade.

        if (outlierRemovalMethod == OutlierRemovalMethods.StdDev3)
            trades = trades.Where(t => !t.IsOutlierStdDev3).ToList();
        if (outlierRemovalMethod == OutlierRemovalMethods.StdDev4)
            trades = trades.Where(t => !t.IsOutlierStdDev4).ToList();

        // Each trade element in trades has a start and end date that represent a time range where trading occurred. Outside of this range, the trade is not active.
        // tradingCandles should have the same length as candles.
        // tradingCandles should copy the close values of candles only when the date value at the candles index is within a trading peroiod. Otherwise, the close value should be NaN.
        foreach (var candle in candles)
        {
            tradeData.Add("");
            bool isTrading = false;
            foreach (var trade in trades)
            {
                if (candle.Date >= trade.Start && candle.Date <= trade.End)
                {
                    isTrading = true;
                    if (candle.Date == trade.End)
                    {
                        // Replace last element in tradeData with the trade's profit and duration
                        // Format the string, trade.Profit should have at most 2 decimal places
                        tradeData[^1] = $"Profit: {Math.Round(trade.Profit, 2)}</br>Duration: {trade.DurationInHours / 24} days";
                    }
                    break;
                }
            }

            if (isTrading)
                tradingCandles.Add(candle.Close);
            else
                tradingCandles.Add(0);
        }

        if (candles.Count() != tradingCandles.Count)
        {
            return BadRequest("The number of candles and trading trace do not match.");
        }

        Domain.Results results = await _databaseService.GetSingleChartResults(symbol, resolution, strategy, outlierRemovalMethod);

        // Combine the price series, trading series, trades data, and results into a single object to return to the client.
        ReturnChartTradingDetails returnChartTradingDetails = new()
        {
            TraceData = candles.Select((candle, i) => new TraceData
            {
                Date = candle.Date,
                Close = candle.Close,
                Trades = tradingCandles[i],
                TradeData = tradeData[i]
            }).ToList(),
            Results = results,
            ProfitList = trades.Select(t => t.Profit).ToList()
        };

        return Ok(returnChartTradingDetails);
    }
}

[Route("api/[controller]")]
[ApiController]
public class GetResultsForStrategiesController : BaseController
{
    public GetResultsForStrategiesController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    async public Task<ActionResult<List<Domain.Results>>> GetResultsForStrategies([FromQuery] string strategies, [FromQuery] string? symbols, [FromQuery] string outlierRemovalMethod)
    {
        if (strategies.Length == 0) return BadRequest("No strategies provided.");
        int[] strategiesArray = strategies.Split(',').Select(int.Parse).ToArray();

        IQueryable<Domain.Results> results;
        if (symbols == null || symbols.Length == 0 || symbols == "null")
        {
            results = await _databaseService.GetAllResults();
            results = results.Where(r => strategiesArray.Contains(r.Strategy));
        }
        else
        {
            string[] symbolArray = symbols.Split(',');
            results = await _databaseService.GetAllResults();
            results = results.Where(r => strategiesArray.Contains(r.Strategy) && symbolArray.Contains(r.Symbol));
        }
        
        if (outlierRemovalMethod == OutlierRemovalMethods.StdDev4)
            results = results.Where(r => r.OutlierRemovalMethod == OutlierRemovalMethods.StdDev4);
        else if (outlierRemovalMethod == OutlierRemovalMethods.StdDev3)
            results = results.Where(r => r.OutlierRemovalMethod == OutlierRemovalMethods.StdDev3);
        else
            results = results.Where(r => r.OutlierRemovalMethod == OutlierRemovalMethods.None);

        return Ok(results.ToList());
    }
}

[Route("api/[controller]")]
[ApiController]
public class GetSelectResultsController : BaseController
{
    public GetSelectResultsController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    async public Task<ActionResult<List<Domain.Results>>> GetSelectResults([FromQuery] string strategyIds)
    {
        List<int> strategyIdsList = strategyIds.Split(',').Select(int.Parse).ToList();

        List<Domain.Results>? allResults = await _databaseService.GetResultsByIds(strategyIdsList);

        return Ok(allResults);
    }
}



[Route("api/[controller]")]
[ApiController]
public class GetAllResultsByStrategyController : BaseController
{
    public GetAllResultsByStrategyController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    async public Task<ActionResult<List<ReturnResultsByStrategy>>> GetAllResultsByStrategy([FromQuery] string outlierRemovalMethod = "None", string? strategies = null)
    {
        if (strategies == null) return BadRequest("No strategies provided.");
        int[] strategiesList = strategies.Split(',').Select(int.Parse).ToArray();

        IQueryable<Domain.Results> allResults = await _databaseService.GetAllResults();
        allResults = allResults.Where(r => strategiesList.Contains(r.Strategy));
        if (outlierRemovalMethod == OutlierRemovalMethods.StdDev3)
            allResults = allResults.Where(r => r.OutlierRemovalMethod == OutlierRemovalMethods.StdDev3);
        else if (outlierRemovalMethod == OutlierRemovalMethods.StdDev4)
            allResults = allResults.Where(r => r.OutlierRemovalMethod == OutlierRemovalMethods.StdDev4);
        else
            allResults = allResults.Where(r => r.OutlierRemovalMethod == OutlierRemovalMethods.None);

        List<List<StrategyResults>> allResultsByResolution = allResults.ToList()
            .GroupBy(results => results.Strategy)
            .OrderBy(groupByStrategy => groupByStrategy.Key)
            .Select(groupByStrategy => groupByStrategy
                .GroupBy(results => results.Resolution)
                .OrderBy(groupByResolution => groupByResolution.Key)
                .Select(groupByResolution => new StrategyResults
                    {
                        Strategy = groupByStrategy.Key,
                        Resolution = groupByResolution.Key,
                        AverageTradeCount = groupByResolution.Average(results => results.TradeCount),
                        AverageWinRate = groupByResolution.Average(results => results.WinRate),
                        AveragePerformance = groupByResolution.Average(results => results.Performance),

                        // Note: Outliers in performance may not be the same as outliers in win rate.
                        AverageWinRateNoOutlierResults = StatisticsCalculator.RemoveTopOutliersStdDev(groupByResolution.Select(results => results.WinRate ?? 0).ToList(), 4).Item1.Average(winRate => winRate),
                        AveragePerformanceNoOutlierResults = StatisticsCalculator.RemoveTopOutliersStdDev(groupByResolution.Select(results => results.Performance ?? 0).ToList(), 4).Item1.Average(performance => performance),
                    }
                )
                .ToList())
            .ToList();

        ReturnResultsByStrategy returnAllResultsByStrategy = new()
        {
            Results60 = allResultsByResolution.SelectMany(g => g).Where(r => r.Resolution == "60").ToList(),
            Results180 = allResultsByResolution.SelectMany(g => g).Where(r => r.Resolution == "180").ToList(),
            Results360 = allResultsByResolution.SelectMany(g => g).Where(r => r.Resolution == "360").ToList(),
            Results720 = allResultsByResolution.SelectMany(g => g).Where(r => r.Resolution == "720").ToList(),
            Results1D = allResultsByResolution.SelectMany(g => g).Where(r => r.Resolution == "1D").ToList(),
            Results3D = allResultsByResolution.SelectMany(g => g).Where(r => r.Resolution == "3D").ToList(),
            Results1W = allResultsByResolution.SelectMany(g => g).Where(r => r.Resolution == "1W").ToList(),
        };

        return Ok(returnAllResultsByStrategy);
    }
}
[Route("api/[controller]")]
[ApiController]
public class GetSymbolResultsByStrategyController : BaseController
{
    public GetSymbolResultsByStrategyController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetSymbolResultsByStrategy([FromQuery] string outlierRemovalMethod = "None", [FromQuery] string? symbol = null, [FromQuery] string? strategies = null)
    {
        List<int> strategiesList;
        if (strategies == null)
        {
            var supabaseResult = await _databaseService.GetAllResults();
            strategiesList = supabaseResult.Select(r => r.Strategy).Distinct().ToList();
        }
        else 
        {
            strategiesList = strategies.Split(',').Select(int.Parse).ToList();
        }
        
        IQueryable<Domain.Results> allResults = await _databaseService.GetAllResults();
        if (outlierRemovalMethod == OutlierRemovalMethods.StdDev3)
            allResults = allResults.Where(results => results.Symbol == symbol && strategiesList.Contains(results.Strategy) && results.OutlierRemovalMethod == OutlierRemovalMethods.StdDev3);
        else if (outlierRemovalMethod == OutlierRemovalMethods.StdDev4)
            allResults = allResults.Where(results => results.Symbol == symbol && strategiesList.Contains(results.Strategy) && results.OutlierRemovalMethod == OutlierRemovalMethods.StdDev4);
        else
            allResults = allResults.Where(results => results.Symbol == symbol && strategiesList.Contains(results.Strategy) && results.OutlierRemovalMethod == OutlierRemovalMethods.None);

        var results = allResults.GroupBy(results => results.Resolution).ToDictionary(
            grouping => grouping.Key, // The resolution is the key
            grouping => grouping.OrderBy(result => result.Strategy).ToList()
            );

        return Ok(results);
    }
}

[Route("api/[controller]")]
[ApiController]
public class GetAllResultsByStrategyBoxPlotController : BaseController
{
    public GetAllResultsByStrategyBoxPlotController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetAllResultsByStrategyBoxPlot([FromQuery] string strategies, [FromQuery] string symbols)
    {
        if (strategies.Length == 0) return BadRequest("No strategies provided.");

        int[] strategiesInt = strategies.Split(',').Select(int.Parse).ToArray();

        IQueryable<Domain.Results> allResults = await _databaseService.GetAllResults();
        if (symbols == null || symbols.Length == 0 || symbols == "null")
        {
            allResults = allResults.Where(r => strategiesInt.Contains(r.Strategy));
        }
        else 
        {
            string[] symbolsString = symbols.Split(',');;
            allResults = allResults.Where(r => strategiesInt.Contains(r.Strategy) && symbolsString.Contains(r.Symbol));
        }

        var allResultsByResolution = allResults
            .GroupBy(result => result.Resolution)
            .ToDictionary(
                grouping => grouping.Key, // The resolution is the key
                grouping => 
                    {
                        var orderedResults = grouping.OrderBy(result => result.Strategy);
                        var allTrades = orderedResults.Where(result => result.OutlierRemovalMethod == OutlierRemovalMethods.None);
                        var noOutliersStdDev3 = orderedResults.Where(result => result.OutlierRemovalMethod == OutlierRemovalMethods.StdDev3);
                        var noOutliersStdDev4 = orderedResults.Where(result => result.OutlierRemovalMethod == OutlierRemovalMethods.StdDev4);
                        return new {
                                Performance = new
                                {
                                    None = allTrades.Select(result => result.Performance).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.Performance).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.Performance).ToList(),
                                },
                                WinRate = new
                                {
                                    None = allTrades.Select(result => result.WinRate).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.WinRate).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.WinRate).ToList(),
                                },
                                Strategy = new
                                {
                                    None = allTrades.Select(result => result.Strategy).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.Strategy).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.Strategy).ToList()
                                },
                                Symbol = new
                                {
                                    None = allTrades.Select(result => result.Symbol).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.Symbol).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.Symbol).ToList()
                                },
                                TradeCount = new
                                {
                                    None = allTrades.Select(result => result.TradeCount).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.TradeCount).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.TradeCount).ToList()
                                },
                                chartTotalDays = new
                                {
                                    None = allTrades.Select(result => result.ChartTotalDays).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.ChartTotalDays).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.ChartTotalDays).ToList()
                                }
                        };
                    }
                );

        return Ok(allResultsByResolution);
    }
}
[Route("api/[controller]")]
[ApiController]
public class GetParallelCoordinatesController : BaseController
{
    public GetParallelCoordinatesController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetParallelCoordinates([FromQuery] string strategies, [FromQuery] string symbols)
    {
        if (strategies.Length == 0) return BadRequest("No strategies provided.");

        int[] strategiesInt = strategies.Split(',').Select(int.Parse).ToArray();

        IQueryable<Domain.Results> allResults = await _databaseService.GetAllResults();
        if (symbols == null || symbols.Length == 0 || symbols == "null")
        {
            allResults = allResults.Where(r => strategiesInt.Contains(r.Strategy));
        }
        else 
        {
            string[] symbolsString = symbols.Split(',');;
            allResults = allResults.Where(r => strategiesInt.Contains(r.Strategy) && symbolsString.Contains(r.Symbol));
        }


        allResults = allResults.OrderBy(result => result.OutlierRemovalMethod).ThenBy(result => result.Strategy).ThenBy(result => result.Symbol);
        var uniqueOutlierRemovalMethods = allResults.Select(result => result.OutlierRemovalMethod).Distinct().ToList();
        var uniqueSymbols = allResults.Select(result => result.Symbol).Distinct().ToList();
        var uniqueStrategies = allResults.Select(result => result.Strategy).Distinct().ToList();
        var uniqueResolutions = allResults.Select(result => result.Resolution).Distinct().ToList();

        var allCombinations = from outlierRemovalMethod in uniqueOutlierRemovalMethods
                             from symbol in uniqueSymbols
                             from strategy in uniqueStrategies
                             from resolution in uniqueResolutions
                             select new { OutlierRemovalMethod = outlierRemovalMethod, Symbol = symbol, Strategy = strategy, Resolution = resolution };

        var missingCombinations = allCombinations.Except(allResults.Select(result => new { OutlierRemovalMethod = result.OutlierRemovalMethod, Symbol = result.Symbol, Strategy = result.Strategy, Resolution = result.Resolution })).ToList();

        List<Domain.Results> filledResults = allResults.ToList();
        foreach (var combination in missingCombinations)
        {
            filledResults.Add(new Domain.Results
            {
                OutlierRemovalMethod = combination.OutlierRemovalMethod,
                Symbol = combination.Symbol,
                Strategy = combination.Strategy,
                Resolution = combination.Resolution,
                Performance = 1,
                TradeCount = 0,
                WinRate = 0
            });
        }

        var returnObject = filledResults
            .GroupBy(results => results.OutlierRemovalMethod)
            .ToDictionary(
                grouping => grouping.Key, 
                grouping => grouping
                    .GroupBy(results => results.Strategy)
                    .ToDictionary(
                        grouping => grouping.Key,
                        grouping => new
                        {
                            Performance = grouping.Select(results => results.Performance).ToList(),
                            Resolution = grouping.Select(results => results.Resolution).ToList(),
                            WinRate = grouping.Select(results => results.WinRate).ToList(),
                            Symbol = grouping.Select(results => results.Symbol).ToList()
                        }
                    )
                );

        return Ok(returnObject);
    }
}


// [Route("api/[controller]")]
// [ApiController]
// public class GetAllTradesBySymbolBoxPlotController : BaseController
// {
//     public GetAllTradesBySymbolBoxPlotController(DatabaseService databaseService)
//         : base(databaseService)
//     {
//     }

//     [HttpGet]
//     public ActionResult GetAllTradesBySymbolBoxPlot([FromQuery] string outlierRemovalMethod = "None", string symbol = "ETHBTC")
//     {
//         IQueryable<Trade> allTrades;
//         allTrades = _databaseService.GetAllTrades().Where(r => r.Symbol == symbol);
//         allTrades = allTrades.Where(r => StrategiesToInclude.Contains(r.Strategy));

//         var allTradesByResolution = allTrades
//             .OrderBy(trade => trade.Strategy)
//             .GroupBy(trade => trade.Resolution)
//             // .OrderBy(grouping => grouping.Key)
//             .ToDictionary(
//                 grouping => grouping.Key, // The resolution is the key
//                 grouping => new // The value is an anonymous object with Performance and Strategy lists
//                 {
//                     Profit = new
//                     {
//                         None = grouping
//                             .Select(trade => trade.Profit)
//                             .ToList(),
//                         StdDev3 = grouping
//                             .Where(trade => trade.IsOutlierStdDev3 == false)
//                             .Select(trade => trade.Profit)
//                             .ToList(),
//                         StdDev4 = grouping
//                             .Where(trade => trade.IsOutlierStdDev4 == false)
//                             .Select(trade => trade.Profit)
//                             .ToList(),
//                     },
//                     Strategy = new
//                     {
//                         None = grouping
//                             .Select(trade => trade.Strategy)
//                             .ToList(),
//                         StdDev3 = grouping
//                             .Where(trade => trade.IsOutlierStdDev3 == false)
//                             .Select(trade => trade.Strategy)
//                             .ToList(),
//                         StdDev4 = grouping
//                             .Where(trade => trade.IsOutlierStdDev4 == false)
//                             .Select(trade => trade.Strategy)
//                             .ToList()
//                     }
//                 });
//         return Ok(allTradesByResolution);
//     }
// }


[Route("api/[controller]")]
[ApiController]
public class GetAllResultsBySymbolBoxPlotController : BaseController
{
    public GetAllResultsBySymbolBoxPlotController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetAllResultsBySymbolBoxPlot([FromQuery] string symbols)
    {
        string[] symbolsList = symbols.Split(',');

        IQueryable<Domain.Results> allResults = await _databaseService.GetAllResults();
        if (symbols != null && symbols.Length != 0 && symbols != "null")
        {
            allResults = allResults.Where(r => symbolsList.Contains(r.Symbol));
        }

        var allResultsByResolution = allResults
            .GroupBy(result => result.Resolution)
            // .OrderBy(grouping => grouping.Key)
            .ToDictionary(
                grouping => grouping.Key, // The resolution is the key
                grouping => 
                    {
                        var orderedResults = grouping.OrderBy(result => result.Symbol);
                        var allTrades = orderedResults.Where(result => result.OutlierRemovalMethod == OutlierRemovalMethods.None);
                        var noOutliersStdDev3 = orderedResults.Where(result => result.OutlierRemovalMethod == OutlierRemovalMethods.StdDev3);
                        var noOutliersStdDev4 = orderedResults.Where(result => result.OutlierRemovalMethod == OutlierRemovalMethods.StdDev4);
                        return new {
                                Performance = new
                                {
                                    None = allTrades.Select(result => result.Performance).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.Performance).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.Performance).ToList(),
                                },
                                WinRate = new
                                {
                                    None = allTrades.Select(result => result.WinRate).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.WinRate).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.WinRate).ToList(),
                                },
                                Strategy = new
                                {
                                    None = allTrades.Select(result => result.Strategy).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.Strategy).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.Strategy).ToList()
                                },
                                Symbol = new
                                {
                                    None = allTrades.Select(result => result.Symbol).ToList(),
                                    StdDev3 = noOutliersStdDev3.Select(result => result.Symbol).ToList(),
                                    StdDev4 = noOutliersStdDev4.Select(result => result.Symbol).ToList()
                                }
                        };
                    }
                );

        return Ok(allResultsByResolution);
    }
}

[Route("api/[controller]")]
[ApiController]
public class CompareOutliersController : BaseController
{
    public CompareOutliersController(DatabaseService databaseService)
        : base(databaseService)
    {
    }

    [HttpGet]
    public async Task<ActionResult<List<ReturnChartTradingDetails>>> CompareOutliers()
    {
        // ** THIS IS TO RUN ON THE DATABASE WHEN NO OUTLIERS HAVE BEEN REMOVED **
        // Once a satisfactory method is found, apply the data in Trader.cs to remove outliers while a strategy is being backtested. 

        // GOAL:
        // Plot all individual trades profits for a given chart/strategy 
        // and compare them to the same data with outliers removed.
        // To do this, 2 series are created: one with all trades and one with outliers removed.
        // The series are overlaid in Plotly to see if too much/little outliers are removed.
        // This is done for all charts (~500) to get a better idea of the outliser-removal method performance across different chart types.


        var resultsAsQueryable = await _databaseService.GetAllResults();
        var results = resultsAsQueryable
            .Where(r => r.OutlierRemovalMethod == "None")
            .Select(r => new
            {
                r.Id,
                r.Strategy,
                r.Symbol,
                r.Resolution
            })
            .ToList();

        var tradesAsQueryable = await _databaseService.GetAllTrades();
        var test = tradesAsQueryable.ToList();
        List<List<Trade>> tradesByResults = tradesAsQueryable
            // .Where(t => t.Resolution == "720" && t.Symbol == "ETHBTC" && t.Strategy == 1) // For testing, only check a certain resolution
            .GroupBy(t => t.ResultsId)
            .Take(200) // For testing purposes, only take the first few results
            .Select(r => r.OrderBy(t => t.Profit).ToList())
            .ToList();

        List<ReturnTradeDistribution> returnTradeDistributions = tradesByResults.Select((tradesByResults, i) =>
        {
            var resultId = tradesByResults[0].ResultsId;

            return new ReturnTradeDistribution
            {
                Profits = tradesByResults.Select(c => c.Profit).ToList(),
                OutliersStdDev3 = tradesByResults.Select(c => c.IsOutlierStdDev3? c.Profit : double.NaN).ToList(),
                OutliersStdDev4 = tradesByResults.Select(c => c.IsOutlierStdDev4? c.Profit : double.NaN).ToList(),
                // Look for a results object with the same Id as resultId and get the symbol, resolution, and strategy
                Symbol = results.FirstOrDefault(c => c.Id == resultId)?.Symbol,
                Resolution = results.FirstOrDefault(c => c.Id == resultId)?.Resolution,
                Strategy = results.FirstOrDefault(c => c.Id == resultId)?.Strategy,
            };
        }).ToList();
        returnTradeDistributions = returnTradeDistributions.OrderBy(r => r.Symbol).ThenBy(r => r.Resolution).ThenBy(r => r.Strategy).ToList();
        // return Ok(returnTradeDistributions);
        return Ok(returnTradeDistributions.ToList());
    }
}