// using CsvHelper;
// using CsvHelper.Configuration;
// using CsvHelper.TypeConversion;

// public class DataValidator : DefaultTypeConverter
// {
//     private List<string> _validValues;

//     public DataValidator(List<string> validValues)
//     {
//         _validValues = validValues;
//     }

//     public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
//     {
//         if (_validValues.Contains(text))
//         {
//             return text;
//         }
//         else
//         {
//             // Handle invalid value (e.g., throw exception, log, or use a default value)
//             throw new ArgumentException($"Value '{text}' is not in the list of valid values.");
//         }
//     }
// }