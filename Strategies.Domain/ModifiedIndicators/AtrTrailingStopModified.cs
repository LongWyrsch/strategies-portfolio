
using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;


// I tested the indicators provided by the Skender.Stock.Indicators library.
// Some indicators generated results different from the TradingView data.
// In those cases, I tried to modify the indicators to match the TradingView data.

public static class ATRTrailingStopModified
{
    public static IEnumerable<AtrStopResult> GetATRTrailingStopModified(this List<Quote> quotes, int period = 21, int nATRMultip = 3)
    {
        var closes = quotes.Select(q => (double)q.Close).ToList();
        
        var atr = quotes.GetATRModified(period);
        
        List<AtrStopResult> ATRTrailingStop = [];
        ATRTrailingStop.Add(new AtrStopResult(quotes.ElementAt(0).Date)
        {
            AtrStop = null
        });
        for (int i = 1; i < quotes.Count; i++)
        {
            var close = closes[i];
            var closePrev = closes[i - 1];
            var atrPrev = (double?)ATRTrailingStop[i - 1].AtrStop;
            double? atrTrailingStop = 0;

            if (i < period - 1)
            {
                atrTrailingStop = null;
            }
            else
            {
                double nLoss = (atr.ElementAt(i).Item2 * nATRMultip) ?? 0.0;
                double iff_1 = close > atrPrev ? close - nLoss : close + nLoss;
                double iff_2 = close < atrPrev && closePrev < atrPrev ? Math.Min(atrPrev ?? 0, close + nLoss) : iff_1;
                atrTrailingStop = close > atrPrev && closePrev > atrPrev ? Math.Max(atrPrev ?? 0, close - nLoss) : iff_2;
            }

            ATRTrailingStop.Add(new AtrStopResult(quotes.ElementAt(i).Date)
            {
                AtrStop = (decimal?)atrTrailingStop
            });
        }
        return ATRTrailingStop;
    }

}