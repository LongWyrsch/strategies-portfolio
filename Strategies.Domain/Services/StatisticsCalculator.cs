using System;
using System.Collections.Generic;
using System.Linq;

namespace Strategies.Domain;

public class StatisticsCalculator
{
    public static double CalculateMean(List<double> data)
    {
        return data.Average();
    }

    public static double CalculateStandardDeviation(List<double> data)
    {
        double mean = CalculateMean(data);
        double sumOfSquaresOfDifferences = data.Sum(val => (val - mean) * (val - mean));
        return Math.Sqrt(sumOfSquaresOfDifferences / data.Count);
    }

    public static double? CalculateSkewness(List<double> data)
    {
        double mean = CalculateMean(data);
        double standardDeviation = CalculateStandardDeviation(data);
        int n = data.Count;

        double sumCubedDifferences = data.Sum(val => Math.Pow(val - mean, 3));
        double skewness = (n / ((n - 1.0) * (n - 2.0))) * (sumCubedDifferences / Math.Pow(standardDeviation, 3));

        // checked if skewness is NaN
        if (double.IsNaN(skewness) || double.IsInfinity(skewness))
            return null;

        return skewness;
    }

    public static double? CalculateExcessKurtosis(List<double> data)
    {
        double mean = CalculateMean(data);
        double standardDeviation = CalculateStandardDeviation(data);
        int n = data.Count;

        double sumQuarticDifferences = data.Sum(val => Math.Pow(val - mean, 4));
        double kurtosis = (n * (n + 1) / ((n - 1.0) * (n - 2) * (n - 3))) * (sumQuarticDifferences / Math.Pow(standardDeviation, 4)) - (3 * Math.Pow(n - 1, 2) / ((n - 2) * (n - 3)));

        // Adjust for excess kurtosis
        double excessKurtosis = kurtosis - 3;

        if (double.IsNaN(excessKurtosis) || double.IsInfinity(excessKurtosis))
            return null;

        return excessKurtosis;
    }

    public static double? NormalDistributioncorrelationCoefficient(List<double> sample)
    {
        double InverseStandardNormal(double p)
        {
            // Inverse CDF for the standard normal distribution (approximation)
            // Using an approximation method for simplicity
            if (p <= 0 || p >= 1) throw new ArgumentOutOfRangeException(nameof(p), "Argument must be between 0 and 1.");

            if (p < 0.5)
                return -RationalApproximation(Math.Sqrt(-2.0 * Math.Log(p)));
            else
                return RationalApproximation(Math.Sqrt(-2.0 * Math.Log(1 - p)));
        }

        double RationalApproximation(double t)
        {
            // Coefficients in rational approximations
            double[] c = { 2.515517, 0.802853, 0.010328 };
            double[] d = { 1.432788, 0.189269, 0.001308 };

            return t - ((c[2] * t + c[1]) * t + c[0]) / (((d[2] * t + d[1]) * t + d[0]) * t + 1.0);
        }

        sample.Sort();

        int n = sample.Count;
        double[] theoreticalQuantiles = new double[n];
        double mean = sample.Average();
        double stdDev = Math.Sqrt(sample.Select(val => Math.Pow(val - mean, 2)).Average());

        // Console.WriteLine("Sample Quantiles:\tTheoretical Quantiles:");

        for (int i = 1; i <= n; i++)
        {
            double p = (i - 0.5) / n;
            theoreticalQuantiles[i - 1] = mean + stdDev * InverseStandardNormal(p);
            // Console.WriteLine($"{sample[i - 1]}\t{theoreticalQuantiles[i - 1]}");
        }

        // Calculate linearity using Pearson correlation coefficient
        double sampleMean = sample.Average();
        double qqMean = theoreticalQuantiles.Average();

        double numerator = 0.0;
        double denominatorX = 0.0;
        double denominatorY = 0.0;

        for (int i = 0; i < n; i++)
        {
            numerator += (sample[i] - sampleMean) * (theoreticalQuantiles[i] - qqMean);
            denominatorX += Math.Pow(sample[i] - sampleMean, 2);
            denominatorY += Math.Pow(theoreticalQuantiles[i] - qqMean, 2);
        }

        double correlationCoefficient = Math.Abs(numerator / Math.Sqrt(denominatorX * denominatorY));

        // Check for high correlation (r close to 1 indicates normality)
        // bool isNormal = Math.Abs(correlationCoefficient) > 0.98; // Threshold to determine similarity
        // Console.WriteLine(isNormal ? "Sample is normally distributed." : "Sample is not normally distributed.");

        // checked if correlationCoefficient is NaN
        if (double.IsNaN(correlationCoefficient) || double.IsInfinity(correlationCoefficient))
            return null;

        return correlationCoefficient;
    }

    public static (double, double, double) GetQuantileMetrics(List<Results> results, double threshold = 1.5)
    {
        List<double> performances = new();
        foreach (var result in results)
        {
            if (result.Performance == null)
                continue;

            performances.Add((double)result.Performance);
        }

        var sortedList = performances.OrderBy(x => x).ToList();

        double Q1 = GetQuantile(sortedList, 0.25);
        double Q3 = GetQuantile(sortedList, 0.75);
        double median = GetQuantile(sortedList, 0.5);

        double IQR = Q3 - Q1;

        double min = Q1 - threshold * IQR;
        double max = Q3 + threshold * IQR;


        return (min, Q1, median);
    }

    public static (List<double>, List<double>, List<double>, List<int>) RemoveTopOutliersIQR(List<double> input, double threshold = 1.5)
    {
        if (input.Count == 0)
            return ([], [], [], []);

        // Step 1: Sort the list
        var sortedList = input.OrderBy(x => x).ToList();

        // Step 2: Calculate Q1 and Q3
        double Q1 = GetQuantile(sortedList, 0.25);
        double Q3 = GetQuantile(sortedList, 0.75);

        // Step 3: Calculate IQR
        double IQR = Q3 - Q1;

        // Step 4: Calculate the minimum and maximum values to determine outliers
        double min = Q1 - threshold * IQR;
        double max = Q3 + threshold * IQR;

        // Step 5: Return the list without outliers
        // return sortedList.Where(x => x >= min && x <= max).ToList();
        // To be conservative, only the upper outliers (exceptionaly good results) are removed.
        var filteredList = input.Where(x => x <= max).ToList();

        // Create a list of the same size as the input list, where outliers are replaced with null
        var filteredListSameSize = input.Select(x => x > max ? double.NaN : x).ToList();

        // create a list of the outliers
        var outliers = input.Where(x => x > max).ToList();

        // create a list of the indexes of the removed elements
        var removedIndexes = filteredListSameSize.Select((value, index) => new { value, index })
            .Where(pair => double.IsNaN(pair.value))
            .Select(pair => pair.index)
            .ToList() ?? [];

        return (filteredList, filteredListSameSize, outliers, removedIndexes);
    }

    private static double GetQuantile(List<double> sortedList, double quantile)
    {
        double index = (sortedList.Count - 1) * quantile;
        int lowerIndex = (int)Math.Floor(index);
        int upperIndex = (int)Math.Ceiling(index);
        if (lowerIndex == upperIndex)
        {
            return sortedList[lowerIndex];
        }
        double fraction = index - lowerIndex;

        try
        {
            var sdf = sortedList[lowerIndex] * (1 - fraction) + sortedList[upperIndex] * fraction;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return sortedList[lowerIndex] * (1 - fraction) + sortedList[upperIndex] * fraction;
    }

    public static (List<double>, List<double>, List<double>, List<int>) RemoveTopOutliersStdDev(List<double> input, double threshold = 3)
    {
        if (input.Count == 0)
            return ([], [], [], []);

        double mean = input.Average();
        double stdDev = CalculateStandardDeviation(input);

        bool isWithinThreshold(double x) => (mean - threshold * stdDev) <= x && x <= (mean + threshold * stdDev);

        var filteredList = input.Where(x => isWithinThreshold(x)).ToList();
        var filteredListSameSize = input.Select(x => isWithinThreshold(x) ? x : double.NaN).ToList();
        var outliers = input.Where(x => !isWithinThreshold(x)).ToList();
        var removedIndexes = filteredListSameSize.Select((value, index) => new { value, index })
            .Where(pair => double.IsNaN(pair.value))
            .Select(pair => pair.index)
            .ToList();

        return (filteredList, filteredListSameSize, outliers, removedIndexes);
    }

}
