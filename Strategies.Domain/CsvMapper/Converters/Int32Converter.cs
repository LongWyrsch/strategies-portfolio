using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Strategies.Domain;

/// <summary>
/// A custom type converter for the CsvHelper library. It converts a string to an integer.
/// </summary>
public class Int32Converter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }
        else if (int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
        {
            return result;
        }
        else
        {
            // Throw an exception if the value cannot be parsed as an integer
            throw new CsvHelperException(row.Context, $"Invalid integer value '{text}' in column '{memberMapData.Member.Name}'.");
        }
    }
}
