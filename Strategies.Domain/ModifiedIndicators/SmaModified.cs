
using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;


// I tested the indicators provided by the Skender.Stock.Indicators library.
// Some indicators generated results different from the TradingView data.
// In those cases, I tried to modify the indicators to match the TradingView data.

public static class SmaModified
{
    public static IEnumerable<SmaResult> GetSmaModified(this List<Quote> quotes, int period)
    {
        List<SmaResult> SMA = [];
        for (int i = 0; i < quotes.Count; i++)
        {
            if (i < period - 1)
                SMA.Add(new SmaResult(quotes.ElementAt(i).Date){Sma = null});
            else
            {
                double? sum = 0;
                for (int j = 0; j < period; j++)
                    sum += ((double?)quotes.ElementAt(i - j).Close ?? 0) / period;

                SMA.Add(new SmaResult(quotes.ElementAt(i).Date){Sma = sum});
            }
        }
        return SMA;
    }
    public static IEnumerable<(DateTime, double?)> GetSmaModified(this List<(DateTime, double?)> data, int period)
    {
        List<(DateTime, double?)> SMA = [];
        for (int i = 0; i < data.Count; i++)
        {
            if (i < period)
            {
                SMA.Add(new(data.ElementAt(i).Item1, null));
            }
            else
            {
                double? sum = 0;
                for (int j = 0; j < period; j++)
                {
                    sum += (data.ElementAt(i - j).Item2 ?? 0) / period;
                }
                SMA.Add(new(data.ElementAt(i).Item1, sum));
            }
        }
        return SMA;
    }
}