namespace Strategies.ConsoleApp;

public static class Utilities
{
    // Based on pattern matching, get the number inside the brackets within actionSelection ("(12) ...")
    public static int ExtractNumberFromSelection(string optionSelection)
    {
        var selectedNumber = int.Parse(optionSelection.Split(")")[0].TrimStart('('));
        return selectedNumber;
    }
}