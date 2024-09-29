namespace Strategies.Server.DTO;

public class ReturnResultsByStrategy
{
    public List<StrategyResults>? AllResultsMixed { get; set; }
    public List<StrategyResults>? Results60 { get; set; }
    public List<StrategyResults>? Results180 { get; set; }
    public List<StrategyResults>? Results360 { get; set; }
    public List<StrategyResults>? Results720 { get; set; }
    public List<StrategyResults>? Results1D { get; set; }
    public List<StrategyResults>? Results3D { get; set; }
    public List<StrategyResults>? Results1W { get; set; }
}

public class StrategyResults
{
    public int Strategy { get; set; }
    public string? Resolution { get; set; }
    public double? AverageTradeCount { get; set; }
    public double? AverageWinRate { get; set; }
    public double? AveragePerformance { get; set; }
    public double? AverageWinRateNoOutlierResults { get; set; }
    public double? AveragePerformanceNoOutlierResults { get; set; }
}