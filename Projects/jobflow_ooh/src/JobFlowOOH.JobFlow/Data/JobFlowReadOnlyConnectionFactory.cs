using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JobFlowOOH.JobFlow.Data;

internal sealed class JobFlowReadOnlyConnectionFactory
{
    private readonly string _connectionString;

    public JobFlowReadOnlyConnectionFactory(IConfiguration configuration)
        : this(configuration.GetConnectionString("JobFlowReadOnly"))
    {
    }

    internal JobFlowReadOnlyConnectionFactory(string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:JobFlowReadOnly is not configured.");
        }

        var builder = new SqlConnectionStringBuilder(connectionString)
        {
            ApplicationIntent = ApplicationIntent.ReadOnly
        };
        _connectionString = builder.ConnectionString;
    }

    public SqlConnection Open()
    {
        return new SqlConnection(_connectionString);
    }
}
