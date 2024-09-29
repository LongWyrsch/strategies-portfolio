using CsvHelper.Configuration;

namespace Strategies.Domain;

// This is used to read the CSV files with the candles data downloaded from TradingView in March 2024.
internal sealed class CandleCsvReaderMapper2024 : ClassMap<Candle>
{
    internal CandleCsvReaderMapper2024()
    {
        // CSV files downloaded in March 2024 have:
        //     - "yyyy-MM-dd" dates for with a weekly resolution
        //     - "yyyy-MM-dd'T'HH:mm:ss'Z'" dates for smaller resolutions.
        // UnixTimestampConverter handles both cases.
        this.Map(m => m.Date).Name("time").TypeConverter<MultiFormatDateTimeConverter>();
        this.Map(m => m.Open).Name("open").TypeConverter<DoubleConverter>();
        this.Map(m => m.High).Name("high").TypeConverter<DoubleConverter>();
        this.Map(m => m.Low).Name("low").TypeConverter<DoubleConverter>();
        this.Map(m => m.Close).Name("close").TypeConverter<DoubleConverter>();
        this.Map(m => m.Ichimoku_ConversionLine).Name("Conversion Line").TypeConverter<DoubleConverter>();
        this.Map(m => m.Ichimoku_BaseLine).Name("Base Line").TypeConverter<DoubleConverter>();
        this.Map(m => m.Ichimoku_LaggingSpan).Name("Lagging Span").TypeConverter<DoubleConverter>();
        this.Map(m => m.Ichimoku_LeadingSpanA).Name("Leading Span A").TypeConverter<DoubleConverter>();
        this.Map(m => m.Ichimoku_LeadingSpanB).Name("Leading Span B").TypeConverter<DoubleConverter>();
        this.Map(m => m.MA_20).Name("MA №1").TypeConverter<DoubleConverter>();
        this.Map(m => m.MA_50).Name("MA №2").TypeConverter<DoubleConverter>();
        this.Map(m => m.MA_100).Name("MA №3").TypeConverter<DoubleConverter>();
        this.Map(m => m.MA_200).Name("MA №4").TypeConverter<DoubleConverter>();
        this.Map(m => m.BB_Basis).Name("Basis").TypeConverter<DoubleConverter>();
        this.Map(m => m.BB_Upper).Name("Upper").TypeConverter<DoubleConverter>();
        this.Map(m => m.BB_Lower).Name("Lower").TypeConverter<DoubleConverter>();
        this.Map(m => m.Volume).Name("Volume").TypeConverter<DoubleConverter>();
        this.Map(m => m.MACD_Histogram).Name("Histogram").TypeConverter<DoubleConverter>();
        this.Map(m => m.MACD).Name("MACD").TypeConverter<DoubleConverter>();
        this.Map(m => m.MACD_Signal).Name("Signal").TypeConverter<DoubleConverter>();
        this.Map(m => m.RSI).Name("RSI").TypeConverter<DoubleConverter>();
        this.Map(m => m.RSI_BasedMA).Name("RSI-based MA").TypeConverter<DoubleConverter>();
        this.Map(m => m.ADX).Name("ADX").TypeConverter<DoubleConverter>();
        this.Map(m => m.MF).Name("MF").TypeConverter<DoubleConverter>();
        this.Map(m => m.OBV).Name("OnBalanceVolume").TypeConverter<DoubleConverter>();
        this.Map(m => m.ATR).Name("ATR").TypeConverter<DoubleConverter>();
    }
}