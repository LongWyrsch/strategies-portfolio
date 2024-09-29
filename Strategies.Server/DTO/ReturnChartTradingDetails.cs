namespace Strategies.Server.DTO;

public class ReturnChartTradingDetails
{
    public List<TraceData> TraceData { get; set; } = new();
    public Domain.Results Results { get; set; } = new();
    public List<double> ProfitList { get; set; } = new();
}

public class TraceData
{
    public double Close { get; set; }
    public double Trades { get; set; }
    public string TradeData { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}


