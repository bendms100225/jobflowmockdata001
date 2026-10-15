using JobFlowOOH.Application.Exceptions;
using JobFlowOOH.Application.Jobs;
using JobFlowOOH.Application.Options;
using Microsoft.Data.SqlClient;

namespace JobFlowOOH.JobFlow.Writing.Experimental;

internal static class ExperimentalWriteGuard
{
    public static void EnsureRegistered(bool isDevelopment, JobFlowWritesOptions options, string? connectionString)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (!IsExperimentalMode(options.Mode))
        {
            return;
        }

        if (!isDevelopment)
        {
            throw new JobFlowExperimentalWriteException("Experimental JobFlow writes are refused outside Development.");
        }

        if (!options.AllowDevelopmentDatabaseMutation)
        {
            throw new JobFlowExperimentalWriteException("Experimental JobFlow writes require JobFlowWrites:AllowDevelopmentDatabaseMutation.");
        }

        EnsureConnection(connectionString);
        if (string.IsNullOrWhiteSpace(options.DevelopmentUsername) || string.IsNullOrWhiteSpace(options.DevelopmentCallTakenBy))
        {
            throw new JobFlowExperimentalWriteException("Configure DevelopmentUsername and DevelopmentCallTakenBy from existing JobFlow login rows.");
        }

        if (options.DevelopmentJobType is null)
        {
            throw new JobFlowExperimentalWriteException("Configure JobFlowWrites:DevelopmentJobType from an existing orders.jobtype value.");
        }
    }

    public static void EnsureConnection(string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new JobFlowExperimentalWriteException("ConnectionStrings:JobFlowExperimental is not configured.");
        }

        var builder = new SqlConnectionStringBuilder(connectionString);
        if (!string.Equals(builder.InitialCatalog, PocMarkers.JobFlowDatabaseName, StringComparison.Ordinal))
        {
            throw new JobFlowExperimentalWriteException("Experimental writes are refused unless the database name is JobFlowDemo.");
        }

        if (builder.ApplicationIntent != ApplicationIntent.ReadWrite)
        {
            throw new JobFlowExperimentalWriteException("The experimental JobFlow connection must not be read-only.");
        }
    }

    public static bool IsExperimentalMode(string? mode)
    {
        return string.Equals(mode, JobFlowWriteModes.ExperimentalDirectSql, StringComparison.OrdinalIgnoreCase);
    }
}
