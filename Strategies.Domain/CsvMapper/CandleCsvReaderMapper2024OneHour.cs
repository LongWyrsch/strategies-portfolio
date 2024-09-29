using CsvHelper.Configuration;

namespace Strategies.Domain;

// This is used to read the CSV files with the candles data downloaded from cryptodatadownload/com in February 2024.
internal sealed class CandleCsvReaderMapper2024OneHour : ClassMap<Candle>
{
    internal CandleCsvReaderMapper2024OneHour()
    {
        this.Map(m => m.Date).Name("Date");
        this.Map(m => m.Open).Name("Open").TypeConverter<DoubleConverter>();
        this.Map(m => m.High).Name("High").TypeConverter<DoubleConverter>();
        this.Map(m => m.Low).Name("Low").TypeConverter<DoubleConverter>();
        this.Map(m => m.Close).Name("Close").TypeConverter<DoubleConverter>();
        this.Map(m => m.Volume).Index(0).TypeConverter<DoubleConverter>();
    }
}
