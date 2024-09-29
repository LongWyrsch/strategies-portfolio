using Strategies.Domain.Presentation;




/// <summary>
/// Represents a service that can write messages to the console.
/// It implements the IMessageService interface defined in the Domain layer.
/// By doing so, it decouples the Domain layer from the ConsoleApp layer.
/// </summary>
public class ConsoleMessageService : IMessageService
{
    public void WriteLine(string message)
    {
        
    }
    public void Write(string message)
    {
        
    }
}