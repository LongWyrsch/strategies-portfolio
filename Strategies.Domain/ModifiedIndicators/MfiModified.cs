using System.Diagnostics;
using Skender.Stock.Indicators;

namespace Strategies.Domain;

//  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
//  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
//  = = = = = = = = = = = = VALUES DON'T MATCH TRADINGVIEW! = = = = = = = = = = = =
//  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
//  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
// The stockindicators library MFI indicator is good enough.

public static class MfiModified //aka ADI
{
    public static List<MfiResult> GetMfiModified(this List<Quote> quotes, int length = 14)
    {
        List<double?> src = quotes.Select(q => (double?)(q.High + q.Low + q.Close)/3).ToList();


        List<double?> sumUp = [0];
        List<double?> sumLow = [0];
        for (int i = 1; i < quotes.Count; i++)
        {
            sumUp.Add((double?)quotes[i].Volume * ((src[i]-src[i-1]) <= 0 ? 0 : src[i]));
            sumLow.Add((double?)quotes[i].Volume * ((src[i]-src[i-1]) >= 0 ? 0 : src[i]));
        }

        IEnumerable<double?> upper = SlidingSum(sumUp, length);
        IEnumerable<double?> lower = SlidingSum(sumUp, length);
        
        List<MfiResult> mfi = [];
        for (int i = 0; i < quotes.Count; i++)
        {
            double? mfiValue = 100.0 - (100.0 / (1.0 + upper.ElementAt(i) / lower.ElementAt(i)));
            mfi.Add(new MfiResult(quotes.ElementAt(i).Date)
            {
                Mfi = mfiValue
            });
        }
        return mfi;
    }

    public static IEnumerable<double?> SlidingSum(List<double?> source, int length)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length), "Length must be positive.");

        for (int i = 0; i < source.Count; i++)
        {
            // Calculate start index for the sum calculation
            int startIndex = Math.Max(0, i - length + 1);
            // Calculate the sum of elements from startIndex to the current index i
            double? sum = source.Skip(startIndex).Take(length).Sum();
            yield return sum;
        }
    }
}