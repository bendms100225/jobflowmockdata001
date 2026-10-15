using System.Text.RegularExpressions;

namespace JobFlowOOH.JobFlow.Data;

internal static partial class SelectOnlySql
{
    public static void Ensure(string sql)
    {
        if (string.IsNullOrWhiteSpace(sql))
        {
            throw new InvalidOperationException("JobFlow SQL is empty.");
        }

        var statement = StripComments(sql).Trim().TrimEnd(';').Trim();
        if (statement.Contains(';', StringComparison.Ordinal))
        {
            throw new InvalidOperationException("JobFlow SQL must be a single statement.");
        }

        if (!statement.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("JobFlow SQL must be a single SELECT.");
        }

        if (IntoPattern().IsMatch(statement))
        {
            throw new InvalidOperationException("JobFlow SQL must read existing tables only.");
        }
    }

    internal static string StripComments(string sql)
    {
        var withoutBlock = BlockCommentPattern().Replace(sql, " ");
        return LineCommentPattern().Replace(withoutBlock, " ");
    }

    [GeneratedRegex(@"/\*.*?\*/", RegexOptions.Singleline)]
    private static partial Regex BlockCommentPattern();

    [GeneratedRegex(@"--.*?$", RegexOptions.Multiline)]
    private static partial Regex LineCommentPattern();

    [GeneratedRegex(@"\bINTO\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex IntoPattern();
}
