using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;


// I tested the indicators provided by the Skender.Stock.Indicators library.
// Some indicators generated results different from the TradingView data.
// In those cases, I tried to modify the indicators to match the TradingView data.

public static class ModifiedTrueRange
{
    public static IEnumerable<AtrResult> GetModifiedTrueRange(this List<Quote> quotes)
    {
        List<AtrResult> trueRangeList = [];
        trueRangeList.Add(new AtrResult(quotes.ElementAt(0).Date)
        {
            Atr = (double)(quotes.ElementAt(0).High - quotes.ElementAt(0).Low)
        });
        for (int i = 1; i < quotes.Count; i++)
        {
            var quote = quotes.ElementAt(i);
            var quotePrev = quotes.ElementAt(i - 1);
            decimal? trueRange;
            trueRange = Math.Max(Math.Max(quote.High - quote.Low, Math.Abs(quote.High - quotePrev.Close)), Math.Abs(quote.Low - quotePrev.Close));
            trueRangeList.Add(new AtrResult(quote.Date)
            {
                Atr = (double)trueRange
            });
        }
        return trueRangeList;
    }
}