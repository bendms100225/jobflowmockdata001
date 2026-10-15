using System.Text.RegularExpressions;

namespace JobFlowOOH.UnitTests.Safety;

public class JobFlowSqlSafetyScanTests
{
    private static readonly string[] MutationPatterns =
    [
        @"\bINSERT\b",
        @"\bUPDATE\b",
        @"\bDELETE\b",
        @"\bMERGE\b",
        @"\bTRUNCATE\b",
        @"\bALTER\b",
        @"\bDROP\b",
        @"\bCREATE\s+TABLE\b",
        @"\bEXEC\b",
        @"\bEXECUTE\b",
        @"\bSELECT\s+INTO\b"
    ];

    private static readonly string[] SensitiveColumns =
    [
        "password",
        "epass",
        "uemailpass",
        "trpassword",
        "nino"
    ];

    [Fact]
    public void JobFlowProject_ContainsNoSqlMutationsOutsideComments()
    {
        var root = FindRepositoryRoot();
        var project = Path.Combine(root, "src", "JobFlowOOH.JobFlow");
        var hits = new List<string>();
        foreach (var file in Directory.EnumerateFiles(project, "*.cs", SearchOption.AllDirectories))
        {
            if (file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)
                || file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var source = StripComments(File.ReadAllText(file));
            var relative = Path.GetRelativePath(project, file);
            var experimentalWriter = relative.StartsWith(
                Path.Combine("Writing", "Experimental") + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase);
            foreach (var pattern in MutationPatterns)
            {
                if (experimentalWriter && pattern == @"\bINSERT\b")
                {
                    continue;
                }

                if (Regex.IsMatch(source, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                {
                    hits.Add($"{relative} matches {pattern}");
                }
            }

            foreach (var column in SensitiveColumns)
            {
                if (Regex.IsMatch(source, $@"\b{column}\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                {
                    hits.Add($"{relative} reads {column}");
                }
            }
        }

        Assert.True(hits.Count == 0, string.Join(Environment.NewLine, hits));
    }

    [Fact]
    public void Scanner_DetectsAMutationThatIsNotCommentedOut()
    {
        const string source = "var sql = \"UPDATE dbo.orders SET jobno = jobno\";";

        Assert.Matches(new Regex(@"\bUPDATE\b", RegexOptions.IgnoreCase), StripComments(source));
        Assert.DoesNotMatch(new Regex(@"\bUPDATE\b", RegexOptions.IgnoreCase), StripComments("// UPDATE dbo.orders"));
    }

    [Fact]
    public void DomainProject_DoesNotReferenceSqlAspNetOrEntra()
    {
        var csproj = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "JobFlowOOH.Domain", "JobFlowOOH.Domain.csproj"));
        Assert.DoesNotContain("PackageReference", csproj, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Microsoft.Data.SqlClient", csproj, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Microsoft.AspNetCore", csproj, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Microsoft.Identity", csproj, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Dapper", csproj, StringComparison.OrdinalIgnoreCase);
    }

    private static string StripComments(string source)
    {
        var withoutBlock = Regex.Replace(source, @"/\*.*?\*/", " ", RegexOptions.Singleline);
        return Regex.Replace(withoutBlock, @"//.*?$", " ", RegexOptions.Multiline);
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "JobFlowOOH.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Could not find JobFlowOOH.sln.");
    }
}
