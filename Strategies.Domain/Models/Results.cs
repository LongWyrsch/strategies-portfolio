namespace Strategies.Domain;

public class Results
{
    public string Id { get; set; } = string.Empty;    
    public string Symbol { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string Resolution { get; set; } = string.Empty;
    public double Fee { get; set; }
    public int Strategy { get; set; }
    public string OutlierRemovalMethod { get; set; } = "None";
    public double? BuyAndHoldProfit { get; set; } // ratio, ex: 2 means 2x
    public double? StrategyProfit { get; set; } // ratio, ex: 2 means 2x
    public double? Performance { get; set; } // StrategyProfit / BuyAndHoldProfit
    public int TradeCount { get; set; } = 0;
    public double? TradeProfitAverage { get; set; }
    public double? TradeProfitStdDeviation { get; set; }
    public double? TradeProfitNormality { get; set; }
    public double? TradeProfitSkewness { get; set; }
    public double? TradeProfitKurtosis { get; set; }
    public double? WinRate { get; set; }
    public double? BarsPerTradeAverage { get; set; }
    public int AverageTimePerTradeInHours { get; set; } = 0;
    public DateTime ChartStartTime { get; set; }
    public DateTime ChartEndTime { get; set; }
    public double ChartTotalDays { get; set; }
    public double RatioTimeTrading { get; set; }
    public double? BarsPerTradeStdDeviation { get; set; }
    public double? BarsPerTradeSkewness { get; set; }
    public double? BarsPerTradeKurtosis { get; set; }

    public bool Equals(Results other)
    {
        if (other == null) return false;
        return Id == other.Id &&
            Symbol == other.Symbol &&
            Exchange == other.Exchange &&
            Resolution == other.Resolution &&
            Fee == other.Fee &&
            Strategy == other.Strategy &&
            OutlierRemovalMethod == other.OutlierRemovalMethod &&
            BuyAndHoldProfit == other.BuyAndHoldProfit &&
            StrategyProfit == other.StrategyProfit &&
            Performance == other.Performance &&
            TradeCount == other.TradeCount &&
            TradeProfitAverage == other.TradeProfitAverage &&
            TradeProfitStdDeviation == other.TradeProfitStdDeviation &&
            TradeProfitNormality == other.TradeProfitNormality &&
            TradeProfitSkewness == other.TradeProfitSkewness &&
            TradeProfitKurtosis == other.TradeProfitKurtosis &&
            WinRate == other.WinRate &&
            BarsPerTradeAverage == other.BarsPerTradeAverage &&
            AverageTimePerTradeInHours == other.AverageTimePerTradeInHours &&
            BarsPerTradeStdDeviation == other.BarsPerTradeStdDeviation &&
            BarsPerTradeSkewness == other.BarsPerTradeSkewness &&
            BarsPerTradeKurtosis == other.BarsPerTradeKurtosis;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Results other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Implement suitable hashing based on fields that are part of equality check
        return HashCode.Combine(Id, Symbol, Exchange, Resolution, Strategy, OutlierRemovalMethod);
    }
}