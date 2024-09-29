namespace Strategies.Domain;

/// <summary>
/// The IHostBuilder registeres this class as a singleton and uses it to store the path to Cryptomator read from the appsettings.json file.
/// </summary>
public class Cryptomator
{
    public string? PathToCryptomatorVolume { get; set; }
}