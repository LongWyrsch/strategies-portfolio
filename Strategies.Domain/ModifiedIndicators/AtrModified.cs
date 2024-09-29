using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;

public static class ATRModified
{
    public static IEnumerable<(DateTime, double?)> GetATRModified(this List<Quote> quotes, int period = 14)
    {
        var tr = quotes.GetModifiedTrueRange();
        var rma = tr.Select(atr => (atr.Date, atr.Atr)).ToList().GetRmaModified(period);
        return rma;
    }

}