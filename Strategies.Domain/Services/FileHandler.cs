using System.Globalization;
using System.Diagnostics;
using CsvHelper;
using Strategies.Domain.Presentation;
using Strategies.Domain;

namespace Strategies.Domain;

internal class FileHandler(Cryptomator cryptomator, IMessageService messageService) : IFileHandler
{
    public void CheckCryptomatorAvailability()
    {
        messageService.Write("[[Background]] Checking Cryptomator availability...");
        if (Directory.Exists($"{cryptomator.PathToCryptomatorVolume}") == false)
            throw new Exception("The Cryptomator volume is not mounted/available.");

        messageService.WriteLine("   ...[green3]availabile ✔[/]");
    }
}