using System.Diagnostics;
using System.Text;
using Strategies.Domain.Presentation;

namespace Strategies.Domain;
public class Trader
{
    public string ResultsAllProfitsId { get; set; }
    public string ResultsFilteredProfitsIdStdDev3 { get; set; }
    public string ResultsFilteredProfitsIdStdDev4 { get; set; }
    public string Symbol { get; set; }
    public string Exchange { get; set; }
    public string Resolution { get; set; }
    public string TimeFrame { get; set; }
    public double Fee { get; set; }
    public int Strategy { get; set; }
    private double _buyPrice = 0;
    private DateTime _buyDate;
    private double _sellPrice = 0;
    private int _barIndexAtBuy = 0;
    private readonly List<double> _barsPerTrade = [];
    private readonly List<Trade> _trades = [];
    private bool _idleOnStopLoss = false;
    private double _buyAndHoldProfit = 0;

    public Trader(string symbol, string exchange, string resolution, string timeFrame, double fee, int strategy, double firstOpenPrice, double lastOpenPrice)
    {
        Symbol = symbol;
        Exchange = exchange;
        Resolution = resolution;
        TimeFrame = timeFrame;
        Fee = fee;
        Strategy = strategy;
        ResultsAllProfitsId = Utilities.GenerateResultsId(symbol, resolution, strategy, OutlierRemovalMethods.None);
        ResultsFilteredProfitsIdStdDev3 = Utilities.GenerateResultsId(symbol, resolution, strategy, OutlierRemovalMethods.StdDev3);
        ResultsFilteredProfitsIdStdDev4 = Utilities.GenerateResultsId(symbol, resolution, strategy, OutlierRemovalMethods.StdDev4);
        _buyAndHoldProfit = lastOpenPrice / firstOpenPrice;
    }

    public void Trade(double openPrice, double lowPrice, int barIndex, bool buyCondition, bool sellCondition, DateTime date, double? stopLoss = null)
    {
        if (!_idleOnStopLoss && buyCondition && _buyPrice == 0)
        {
            _buyPrice = openPrice * (1 + Fee);
            _barIndexAtBuy = barIndex;
            _buyDate = date;
            return;
        }
        else if (!sellCondition && _buyPrice > 0 && stopLoss > 0 && lowPrice < _buyPrice * stopLoss)
        {
            _sellPrice = _buyPrice * (double)stopLoss * (1 - Fee);
            Sell(_sellPrice);
            _idleOnStopLoss = true;
            return;
        }
        else if (sellCondition)
        {
            _idleOnStopLoss = false;
            if (_buyPrice > 0)
            {
                _sellPrice = openPrice * (1 - Fee);
                Sell(_sellPrice);
            }
            return;
        }

        void Sell(double sellPrice)
        {
            double tradeProfit = sellPrice / _buyPrice;
            double barsHeld = barIndex - _barIndexAtBuy;
            _barsPerTrade.Add(barsHeld);
            _buyPrice = 0;

            Trade trade = new()
            {
                ResultsId = ResultsAllProfitsId,
                Symbol = Symbol,
                Resolution = Resolution,
                Strategy = Strategy,
                Start = _buyDate,
                End = date,
                Profit = tradeProfit,
                DurationInHours = GetDurationFromResolution(Resolution, barsHeld)
            };

            _trades.Add(trade);
        }
    }

    public (List<Results>, List<Trade>) GenerateResults()
    {

        Results noTradeResults(string outlierType)
        {
            return new()
            {
                Id = ResultsAllProfitsId,
                Symbol = Symbol,
                Exchange = Exchange,
                Resolution = Resolution,
                Fee = Fee,
                Strategy = Strategy,
                OutlierRemovalMethod = outlierType,
                BuyAndHoldProfit = _buyAndHoldProfit,
                StrategyProfit = 1,
                Performance = 1 / _buyAndHoldProfit,
            };
        };

        // If there was no trades, return the results with default values
        if (_trades.Count == 0) return ([
            noTradeResults(OutlierRemovalMethods.None), 
            noTradeResults(OutlierRemovalMethods.StdDev3), 
            noTradeResults(OutlierRemovalMethods.StdDev4), 
        ], _trades);

        // The below logic assumes there was at least one trade

        List<double> allProfits = _trades.Select(t => t.Profit).ToList();
        var (filteredProfitsStdDev3, _, _, outlierIndexesStdDev3) = StatisticsCalculator.RemoveTopOutliersStdDev(allProfits, 3);
        var (filteredProfitsStdDev4, _, _, outlierIndexesStdDev4) = StatisticsCalculator.RemoveTopOutliersStdDev(allProfits, 4);
        outlierIndexesStdDev3.ForEach(outlierIndex => _trades[outlierIndex].IsOutlierStdDev3 = true);
        outlierIndexesStdDev4.ForEach(outlierIndex => _trades[outlierIndex].IsOutlierStdDev4 = true);

        // Remove all elements from _barsPerTrade that correspond to outliers
        var filteredBarsPerTradeStdDev3 = _barsPerTrade.Where((_, index) => !outlierIndexesStdDev3.Contains(index)).ToList();
        var filteredBarsPerTradeStdDev4 = _barsPerTrade.Where((_, index) => !outlierIndexesStdDev4.Contains(index)).ToList();

        Results fillInResults(string id, List<double> profits, List<double> barsPerTrade, string outlierType)
        {
            Results results = new();

            double strategyProfit = profits.Aggregate(1.0, (acc, x) => acc * x);

            results.Id = id;
            results.Symbol = Symbol;
            results.Exchange = Exchange;
            results.Resolution = Resolution;
            results.Fee = Fee;
            results.Strategy = Strategy;
            results.OutlierRemovalMethod = outlierType;
            results.BuyAndHoldProfit = _buyAndHoldProfit;
            results.StrategyProfit = strategyProfit;
            results.Performance = strategyProfit / _buyAndHoldProfit;
            results.TradeCount = profits.Count;
            results.TradeProfitAverage = profits.Average();
            results.TradeProfitStdDeviation = StatisticsCalculator.CalculateStandardDeviation(profits);
            results.TradeProfitNormality = StatisticsCalculator.NormalDistributioncorrelationCoefficient(profits);
            results.TradeProfitSkewness = StatisticsCalculator.CalculateSkewness(profits);
            results.TradeProfitKurtosis = StatisticsCalculator.CalculateExcessKurtosis(profits);
            results.WinRate = (double)profits.Where(p => p > 1).Count() / profits.Count;
            results.BarsPerTradeAverage = barsPerTrade.Average();
            results.AverageTimePerTradeInHours = GetDurationFromResolution(Resolution, barsPerTrade.Average());
            results.BarsPerTradeStdDeviation = StatisticsCalculator.CalculateStandardDeviation(barsPerTrade);
            results.BarsPerTradeSkewness = StatisticsCalculator.CalculateSkewness(barsPerTrade);
            results.BarsPerTradeKurtosis = StatisticsCalculator.CalculateExcessKurtosis(barsPerTrade);

            return results;
        }

        var resultsAllProfits = fillInResults(ResultsAllProfitsId, allProfits, _barsPerTrade, OutlierRemovalMethods.None);
        var resultsFilteredProfitsStdDev3 = fillInResults(ResultsFilteredProfitsIdStdDev3, filteredProfitsStdDev3, filteredBarsPerTradeStdDev3, OutlierRemovalMethods.StdDev3);
        var resultsFilteredProfitsStdDev4 = fillInResults(ResultsFilteredProfitsIdStdDev4, filteredProfitsStdDev4, filteredBarsPerTradeStdDev4, OutlierRemovalMethods.StdDev4);

        // List<Results> allResults = [resultsFilteredProfitsStdDev3];
        List<Results> allResults = [resultsAllProfits, resultsFilteredProfitsStdDev3, resultsFilteredProfitsStdDev4];

        return (allResults, _trades);
    }

    private int GetDurationFromResolution(string resolution, double numberOfBars)
    {
        int hours = resolution switch
        {
            "60" => 1,
            "180" => 3,
            "360" => 6,
            "720" => 12,
            "1D" => 24,
            // "2D" => 48,
            "3D" => 72,
            "1W" => 168,
            _ => throw new Exception("Invalid resolution")
        };

        int duration = (int)(hours * numberOfBars);

        return duration;
    }

}