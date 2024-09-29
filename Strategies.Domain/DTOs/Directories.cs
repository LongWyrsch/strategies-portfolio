namespace Strategies.Domain;

/// <summary>
/// The IHostBuilder registeres this class as a singleton and uses it to store the directories read from the appsettings.json file.
/// </summary>
public class Directories
{
    public string CryptodatadownloadHourData { get; set; } = string.Empty;
    public string TradingViewMarch2024Data { get; set; } = string.Empty;
    public string TradingViewJune2022Data { get; set; } = string.Empty;
}