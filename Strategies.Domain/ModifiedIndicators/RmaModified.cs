
using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;


// I tested the indicators provided by the Skender.Stock.Indicators library.
// Some indicators generated results different from the TradingView data.
// In those cases, I tried to modify the indicators to match the TradingView data.

public static class RmaModified
{
    public static IEnumerable<(DateTime, double?)> GetRmaModified(this List<(DateTime, double?)> data, int period)
    {
        var sma = data.GetSmaModified(period);

        List<(DateTime, double?)> rma = [];
        for (int i = 0; i < data.Count; i++)
        {
            if (i == 0)
            {
                rma.Add(new(data.ElementAt(i).Item1, sma.ElementAt(i).Item2));
                continue;
            }
            double? alpha = 1.0 / period;
            double? sum = rma.ElementAt(i - 1).Item2 == null ? sma.ElementAt(i).Item2 : alpha * data.ElementAt(i).Item2 + (1 - alpha) * rma.ElementAt(i - 1).Item2;
            rma.Add(new(data.ElementAt(i).Item1, sum));
        }
        return rma;
    }
}