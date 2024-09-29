namespace Strategies.Domain.Presentation;

/// <summary>
/// Interface for writing messages to the console. Decouples the domain from the presentation layer.
/// </summary>
public interface IMessageService
{
    void WriteLine(string message);
    void Write(string message);
}