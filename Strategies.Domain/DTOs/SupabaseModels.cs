// using Supabase.Postgrest.Attributes;
// using Supabase.Postgrest.Models;

// using Microsoft.EntityFrameworkCore;
using Postgrest.Attributes;
using Postgrest.Models;
// using Supabase.Postgrest.Models;

namespace Strategies.Domain;



[Table("colors")]
public class Colors : BaseModel
{
    [PrimaryKey("color")] 
    public string Color { get; set; } = string.Empty; 
    [Column("number")]
    public int Number { get; set; } = 11;
}

// Define models to be used with Supabase
[Table("Candles")]
public class SupabaseCandle : BaseModel
{
    [PrimaryKey("symbol", false)]
    public string Symbol { get; set; } = string.Empty;
    [PrimaryKey("exchange", false)]
    public string Exchange { get; set; } = string.Empty;
    [PrimaryKey("resolution", false)]
    public string Resolution { get; set; } = string.Empty;
    [PrimaryKey("datedownloaded", false)]
    public DateTime DateDownloaded { get; set; }
    [PrimaryKey("date", false)]
    public DateTime Date { get; set; }
    [Column("open")]
    public double Open { get; set; }
    [Column("high")]
    public double High { get; set; }
    [Column("low")]
    public double Low { get; set; }
    [Column("close")]
    public double Close { get; set; }
    [Column("ichimoku_conversionline")]
    public double? Ichimoku_ConversionLine { get; set; }
    [Column("ichimoku_baseline")]
    public double? Ichimoku_BaseLine { get; set; }
    [Column("ichimoku_laggingspan")]
    public double? Ichimoku_LaggingSpan { get; set; }
    [Column("ichimoku_leadingspana")]
    public double? Ichimoku_LeadingSpanA { get; set; }
    [Column("ichimoku_leadingspanb")]
    public double? Ichimoku_LeadingSpanB { get; set; }
    [Column("ma_20")]
    public double? MA_20 { get; set; }
    [Column("ma_50")]
    public double? MA_50 { get; set; }
    [Column("ma_100")]
    public double? MA_100 { get; set; }
    [Column("ma_200")]
    public double? MA_200 { get; set; }
    [Column("bb_basis")]
    public double? BB_Basis { get; set; }
    [Column("bb_upper")]
    public double? BB_Upper { get; set; }
    [Column("bb_lower")]
    public double? BB_Lower { get; set; }
    [Column("volume")]
    public double Volume { get; set; }
    [Column("macd_histogram")]
    public double? MACD_Histogram { get; set; }
    [Column("macd")]
    public double? MACD { get; set; }
    [Column("macd_signal")]
    public double? MACD_Signal { get; set; }
    [Column("rsi")]
    public double? RSI { get; set; }
    [Column("rsi_basedma")]
    public double? RSI_BasedMA { get; set; }
    [Column("adx")]
    public double? ADX { get; set; }
    [Column("mf")]
    public double? MF { get; set; }
    [Column("obv")]
    public double? OBV { get; set; }
    [Column("atr")]
    public double? ATR { get; set; }

    public static explicit operator Candle(SupabaseCandle v)
    {
        return new Candle
        {
            Symbol = v.Symbol,
            Exchange = v.Exchange,
            Resolution = v.Resolution,
            DateDownloaded = v.DateDownloaded,
            Date = v.Date,
            Open = v.Open,
            High = v.High,
            Low = v.Low,
            Close = v.Close,
            Ichimoku_ConversionLine = v.Ichimoku_ConversionLine,
            Ichimoku_BaseLine = v.Ichimoku_BaseLine,
            Ichimoku_LaggingSpan = v.Ichimoku_LaggingSpan,
            Ichimoku_LeadingSpanA = v.Ichimoku_LeadingSpanA,
            Ichimoku_LeadingSpanB = v.Ichimoku_LeadingSpanB,
            MA_20 = v.MA_20,
            MA_50 = v.MA_50,
            MA_100 = v.MA_100,
            MA_200 = v.MA_200,
            BB_Basis = v.BB_Basis,
            BB_Upper = v.BB_Upper,
            BB_Lower = v.BB_Lower,
            Volume = v.Volume,
            MACD_Histogram = v.MACD_Histogram,
            MACD = v.MACD,
            MACD_Signal = v.MACD_Signal,
            RSI = v.RSI,
            RSI_BasedMA = v.RSI_BasedMA,
            ADX = v.ADX,
            MF = v.MF,
            OBV = v.OBV,
            ATR = v.ATR
        };
    }
}

[Table("Trades")]
public class SupabaseTrade : BaseModel
{
    [PrimaryKey("resultsid", false)]
    public string ResultsId { get; set; } = string.Empty;
    [Column("symbol")]
    public string Symbol { get; set; } = string.Empty;
    [Column("resolution")]
    public string Resolution { get; set; } = string.Empty;
    [Column("strategy")]
    public int Strategy { get; set; }
    [PrimaryKey("start", false)]
    public DateTime Start { get; set; }
    [Column("end")]
    public DateTime End { get; set; }
    [Column("profit")]
    public double Profit { get; set; }
    [Column("durationinhours")]
    public double DurationInHours { get; set; }
    [Column("isoutlierstddev3")]
    public bool IsOutlierStdDev3 { get; set; } = false;
    [Column("isoutlierstddev4")]
    public bool IsOutlierStdDev4 { get; set; } = false;

    public static explicit operator Trade(SupabaseTrade v)
    {
        return new Trade
        {
            ResultsId = v.ResultsId,
            Symbol = v.Symbol,
            Resolution = v.Resolution,
            Strategy = v.Strategy,
            Start = v.Start,
            End = v.End,
            Profit = v.Profit,
            DurationInHours = v.DurationInHours,
            IsOutlierStdDev3 = v.IsOutlierStdDev3,
            IsOutlierStdDev4 = v.IsOutlierStdDev4
        };
    }
}

[Table("Results")]
public class SupabaseResults : BaseModel
{
    [PrimaryKey("id", false)]
    public string Id { get; set; } = string.Empty;    
    [Column("symbol")]
    public string Symbol { get; set; } = string.Empty;
    [Column("exchange")]
    public string Exchange { get; set; } = string.Empty;
    [Column("resolution")]
    public string Resolution { get; set; } = string.Empty;
    [Column("fee")]
    public double Fee { get; set; }
    [Column("strategy")]
    public int Strategy { get; set; }
    [Column("outlierremovalmethod")]
    public string OutlierRemovalMethod { get; set; } = "None";
    [Column("buyandholdprofit")]
    public double? BuyAndHoldProfit { get; set; } // ratio, ex: 2 means 2x
    [Column("strategyprofit")]
    public double? StrategyProfit { get; set; } // ratio, ex: 2 means 2x
    [Column("performance")]
    public double? Performance { get; set; } // StrategyProfit / BuyAndHoldProfit
    [Column("tradecount")]
    public int TradeCount { get; set; } = 0;
    [Column("tradeprofitaverage")]
    public double? TradeProfitAverage { get; set; }
    [Column("tradeprofitstddeviation")]
    public double? TradeProfitStdDeviation { get; set; }
    [Column("tradeprofitnormality")]
    public double? TradeProfitNormality { get; set; }
    [Column("tradeprofitskewness")]
    public double? TradeProfitSkewness { get; set; }
    [Column("tradeprofitkurtosis")]
    public double? TradeProfitKurtosis { get; set; }
    [Column("winrate")]
    public double? WinRate { get; set; }
    [Column("barspertradeaverage")]
    public double? BarsPerTradeAverage { get; set; }
    [Column("averagetimepertradeinhours")]
    public int AverageTimePerTradeInHours { get; set; } = 0;
    [Column("chartstarttime")]
    public DateTime ChartStartTime { get; set; }
    [Column("chartendtime")]
    public DateTime ChartEndTime { get; set; }
    [Column("charttotaldays")]
    public double ChartTotalDays { get; set; }
    [Column("ratiotimetrading")]
    public double RatioTimeTrading { get; set; }
    [Column("barspertradestddeviation")]
    public double? BarsPerTradeStdDeviation { get; set; }
    [Column("barspertradeskewness")]
    public double? BarsPerTradeSkewness { get; set; }
    [Column("barspertradekurtosis")]
    public double? BarsPerTradeKurtosis { get; set; }

    public static explicit operator Results(SupabaseResults v)
    {
        return new Results
        {
            Id = v.Id,
            Symbol = v.Symbol,
            Exchange = v.Exchange,
            Resolution = v.Resolution,
            Fee = v.Fee,
            Strategy = v.Strategy,
            OutlierRemovalMethod = v.OutlierRemovalMethod,
            BuyAndHoldProfit = v.BuyAndHoldProfit,
            StrategyProfit = v.StrategyProfit,
            Performance = v.Performance,
            TradeCount = v.TradeCount,
            TradeProfitAverage = v.TradeProfitAverage,
            TradeProfitStdDeviation = v.TradeProfitStdDeviation,
            TradeProfitNormality = v.TradeProfitNormality,
            TradeProfitSkewness = v.TradeProfitSkewness,
            TradeProfitKurtosis = v.TradeProfitKurtosis,
            WinRate = v.WinRate,
            BarsPerTradeAverage = v.BarsPerTradeAverage,
            AverageTimePerTradeInHours = v.AverageTimePerTradeInHours,
            ChartStartTime = v.ChartStartTime,
            ChartEndTime = v.ChartEndTime,
            ChartTotalDays = v.ChartTotalDays,
            RatioTimeTrading = v.RatioTimeTrading,
            BarsPerTradeStdDeviation = v.BarsPerTradeStdDeviation,
            BarsPerTradeSkewness = v.BarsPerTradeSkewness,
            BarsPerTradeKurtosis = v.BarsPerTradeKurtosis
        };
    }
}