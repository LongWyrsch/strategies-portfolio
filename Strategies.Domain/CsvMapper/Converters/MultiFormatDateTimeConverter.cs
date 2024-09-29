using CsvHelper;
using CsvHelper.TypeConversion;
using CsvHelper.Configuration;
using System.Globalization;

namespace Strategies.Domain;

// CSV files downloaded in March 2024 have:
//     - "yyyy-MM-dd" dates for with a weekly resolution
//     - "yyyy-MM-dd'T'HH:mm:ss'Z'" dates for smaller resolutions.
// UnixTimestampConverter handles both cases.
public class MultiFormatDateTimeConverter : ITypeConverter
{
    private readonly string[] formats = new[] { "yyyy-MM-dd", "yyyy-MM-dd'T'HH:mm:ss'Z'" };

    public object? ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (DateTime.TryParseExact(text, formats, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            return date;
        }

        // If parsing fails, you can choose to return a default value or throw an exception
        throw new CsvHelper.TypeConversion.TypeConverterException(this, memberMapData, text, row.Context);
    }

    public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString(formats[0]); // Choose your preferred format for writing
        }

        return string.Empty;
    }
}
