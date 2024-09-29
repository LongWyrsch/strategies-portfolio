using System;
using System.Security.Cryptography;
using System.Text;
using Skender.Stock.Indicators;

namespace Strategies.Domain;

public static class Utilities
{
    public static string ToSha256Hash(this string input)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter
                .ToString(hash)
                .Replace("-", "")
                .ToLowerInvariant()
                .Substring(0, 16);
        }
    }

    // Convert Candle objects to Quote objects. 
    // This is required because the indicator library uses Quote objects.
    public static List<Quote> ConvertToQuotes(List<Candle> candles)
    {
        List<Quote> quotes = candles.Select(c => new Quote
        {
            Date = c.Date,
            Open = (decimal)c.Open,
            High = (decimal)c.High,
            Low = (decimal)c.Low,
            Close = (decimal)c.Close,
            Volume = (decimal)c.Volume
        })
        .OrderBy(q => q.Date)
        .ToList();

        return quotes;
    }

    public static string GenerateResultsId(string symbol, string resolution, int strategyNumber, string outlierType)
    {
        string input = $"{symbol}{resolution}{strategyNumber}{outlierType}";
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }   

    public static double? RoundDouble(double? value)
    {
        return value == null ? null : (double)Math.Round((decimal)value, 2);
    }
}