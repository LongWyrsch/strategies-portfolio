using CsvHelper;
using CsvHelper.TypeConversion;
using CsvHelper.Configuration;
using System.Globalization;

namespace Strategies.Domain;

internal class UnixTimestampConverter : ITypeConverter
{
    // This converts a Unix timestamp to a DateTime
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (long.TryParse(text, out long unixTimestamp))
        {
            // Assuming the timestamp is in seconds and UTC
            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
            return dateTime;
        }
        else
        {
            // Throw an exception if the value cannot be parsed as an double
            throw new CsvHelperException(row.Context, $"Invalid unix value '{text}' in column '{memberMapData.Member.Name}'.");
        }
    }

    public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        // Implement this if you need to write back to CSV
        throw new NotImplementedException();
    }
}