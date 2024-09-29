using Strategies.Domain;

namespace Strategies.Server.DTO;

public class ReturnTradeDistribution
{
    public List<double> Profits { get; set; }
    public List<double> OutliersStdDev3 { get; set; }
    public List<double> OutliersStdDev4 { get; set; }
    public string Symbol { get; set; }
    public string Resolution { get; set; }
    public int? Strategy { get; set; }
}