using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;


// I tested the indicators provided by the Skender.Stock.Indicators library.
// Some indicators generated results different from the TradingView data.
// In those cases, I tried to modify the indicators to match the TradingView data.

public static class AdxModified //aka ADI
{
    public static List<AdxResult> GetAdxModified(this List<Quote> quotes, int dilen = 14, int adxlen = 14)
    {
        var trueRange = quotes.GetModifiedTrueRange().Select(t => (t.Date, t.Atr)).ToList();
        var atr = trueRange.GetRmaModified(dilen);
        
        List<double?> plusDM = [0];
        List<double?> minusDM = [0];
        for (int i = 1; i < quotes.Count; i++)
        {
            double? up = (double?)(quotes[i].High - quotes[i-1].High);
            double? down = (double?)(-1*(quotes[i].Low - quotes[i-1].Low));
            plusDM.Add(up == null ? null : up > down && up > 0 ? up : 0);
            minusDM.Add(down == null ? null : down > up && down > 0 ? down : 0);
        }

        var plusDMTuple = plusDM.Select((p, i) => (quotes.ElementAt(i).Date, (double?)p)).ToList();
        var minusDMTuple = minusDM.Select((p, i) => (quotes.ElementAt(i).Date, (double?)p)).ToList();
        var plusDMRma = plusDMTuple.GetRmaModified(dilen);
        var minusDMRma = minusDMTuple.GetRmaModified(dilen);

        List<double?> sum = [];
        for (int i = 0; i < quotes.Count; i++)
        {
            var plus = 100 * plusDMRma.ElementAt(i).Item2 / atr.ElementAt(i).Item2;
            if (plus == null) // equivalent of fixnan in Pine Script: For a given series replaces NaN values with previous nearest non-NaN value.
            {
                int j = i - 1;
                while (j >= 0 && plus == null)
                {
                    plus = plusDM[j];
                    j--;
                }
            }
            plusDM[i] = plus;
            var minus = 100 * minusDMRma.ElementAt(i).Item2 / atr.ElementAt(i).Item2;
            if (minus == null) // equivalent of fixnan in Pine Script: For a given series replaces NaN values with previous nearest non-NaN value.
            {
                int j = i - 1;
                while (j >= 0 && plus == null)
                {
                    minus = minusDM[j];
                    j--;
                }
            }
            minusDM[i] = minus;
            sum.Add(plus + minus);
        };

        var adx = sum.Select((sum, i) => (quotes[i].Date, Math.Abs((plusDM[i]??0) - (minusDM[i]??0)) / (sum??(double?)1))).ToList().GetRmaModified(adxlen);
        adx = adx.Select(a => (a.Item1, a.Item2*100)).ToList();
        return adx.Select((a, i) => new AdxResult(quotes[i].Date) { Adx = a.Item2 }).ToList();
    }
}