using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;


// I tested the indicators provided by the Skender.Stock.Indicators library.
// Some indicators generated results different from the TradingView data.
// In those cases, I tried to modify the indicators to match the TradingView data.

public static class IchimokuModified
{
    public static IEnumerable<IchimokuResult> GetIchimokuModified<TQuote>(this IEnumerable<TQuote> quotes) where TQuote : IQuote
        // !!! Failed to match TradingView data !!!
    {
        // Example of Ichimoku
        IEnumerable<IchimokuResult> ichimokuResults = quotes.GetIchimoku();

        List<IchimokuResult> newIchimokuResults = [];
        for (int i = 0; i < ichimokuResults.Count(); i++)
        {
            newIchimokuResults.Add(new IchimokuResult(ichimokuResults.ElementAt(i).Date)
            {
                TenkanSen = ichimokuResults.ElementAt(i).TenkanSen,
                KijunSen = ichimokuResults.ElementAt(i).KijunSen,
                SenkouSpanA = i < ichimokuResults.Count() - 1 ? ichimokuResults.ElementAt(i + 1).SenkouSpanA : ichimokuResults.ElementAt(i).SenkouSpanA,
                SenkouSpanB = i < ichimokuResults.Count() - 1 ? ichimokuResults.ElementAt(i + 1).SenkouSpanB : ichimokuResults.ElementAt(i).SenkouSpanB,
                ChikouSpan = i > 0 ? ichimokuResults.ElementAt(i - 1).ChikouSpan : ichimokuResults.ElementAt(i).ChikouSpan
            });
        }

        // convert newIchimokuResults to a IEnumerable<IchimokuResult>
        IEnumerable<IchimokuResult> newIchimokuResultsEnumerable = newIchimokuResults;

        return newIchimokuResultsEnumerable;
    }
}