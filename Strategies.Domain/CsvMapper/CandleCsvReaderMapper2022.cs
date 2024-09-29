using CsvHelper.Configuration;

namespace Strategies.Domain;

// This is used to read the CSV files with the candles data downloaded from TradingView in June 2022.
internal sealed class CandleCsvReaderMapper2022 : ClassMap<Candle>
{
    internal CandleCsvReaderMapper2022()
    {
        this.Map(m => m.Date).Name("time").TypeConverter<UnixTimestampConverter>();
        this.Map(m => m.Open).Name("open").TypeConverter<DoubleConverter>();
        this.Map(m => m.High).Name("high").TypeConverter<DoubleConverter>();
        this.Map(m => m.Low).Name("low").TypeConverter<DoubleConverter>();
        this.Map(m => m.Close).Name("close").TypeConverter<DoubleConverter>();
        this.Map(m => m.BB_Basis).Name("Basis").TypeConverter<DoubleConverter>();
        this.Map(m => m.BB_Upper).Name("Upper").TypeConverter<DoubleConverter>();
        this.Map(m => m.BB_Lower).Name("Lower").TypeConverter<DoubleConverter>();
        this.Map(m => m.Volume).Name("Volume").TypeConverter<DoubleConverter>();
        this.Map(m => m.RSI).Name("RSI").TypeConverter<DoubleConverter>();
        this.Map(m => m.RSI_BasedMA).Name("RSI-based MA").TypeConverter<DoubleConverter>();
        this.Map(m => m.MACD_Histogram).Name("Histogram").TypeConverter<DoubleConverter>();
        this.Map(m => m.MACD).Name("MACD").TypeConverter<DoubleConverter>();
        this.Map(m => m.MACD_Signal).Name("Signal").TypeConverter<DoubleConverter>();
    }
}
