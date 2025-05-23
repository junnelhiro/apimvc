using System.Text.RegularExpressions;

public class LexicalParser
{
    public void Parse()
    {
        string input = "int x = 2000.00;;";

        // Define regex for basic tokens
        string pattern = @"\b(int|float|string)\b|\b[a-zA-Z_]\w*\b|\d+|[=;]";

        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(input);
        Console.WriteLine("Tokens:");
        string prev = string.Empty;
        foreach (Match match in matches)
        {
            if (prev == match.Value) continue;
            Console.WriteLine(match.Value);
            prev = match.Value;
        }
    }
}