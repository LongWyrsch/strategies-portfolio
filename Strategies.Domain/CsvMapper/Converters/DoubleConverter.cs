using CsvHelper;
using CsvHelper.TypeConversion;
using CsvHelper.Configuration;
using System.Globalization;

namespace Strategies.Domain;

internal class DoubleConverter : ITypeConverter
{
    // This converts a string to a double
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Equals("NaN", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }
        else if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }
        else
        {
            // Throw an exception if the value cannot be parsed as an double
            throw new CsvHelperException(row.Context, $"Invalid double value '{text}' in column '{memberMapData.Member.Name}'.");
        }
    }

    public string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        // Implement this if you need to write back to CSV
        throw new NotImplementedException();
    }
}