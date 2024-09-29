namespace Strategies.Domain;

public class Trade
{
    public string ResultsId { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Resolution { get; set; } = string.Empty;
    public int Strategy { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double Profit { get; set; }
    public double DurationInHours { get; set; }
    public bool IsOutlierStdDev3 { get; set; } = false;
    public bool IsOutlierStdDev4 { get; set; } = false;

    public bool Equals(Trade other)
    {
        if (other == null) return false;
        return ResultsId == other.ResultsId &&
            Symbol == other.Symbol &&
            Resolution == other.Resolution &&
            Strategy == other.Strategy &&
            Start == other.Start &&
            End == other.End &&
            Profit == other.Profit &&
            DurationInHours == other.DurationInHours &&
            IsOutlierStdDev3 == other.IsOutlierStdDev3 &&
            IsOutlierStdDev4 == other.IsOutlierStdDev4;    
        }

    public override bool Equals(object? obj)
    {
        if (obj is Trade other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        // Implement suitable hashing based on fields that are part of equality check
        return HashCode.Combine(ResultsId, Symbol, Resolution, Strategy);
    }
}