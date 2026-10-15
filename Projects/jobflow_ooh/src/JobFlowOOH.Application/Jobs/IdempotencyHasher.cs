using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace JobFlowOOH.Application.Jobs;

public static class IdempotencyHasher
{
    public static string Hash(CreateJobCommand command, string username, string callTakenBy, int jobType)
    {
        ArgumentNullException.ThrowIfNull(command);
        var payload = string.Join(
            '\n',
            command.ClientCode.Trim(),
            command.SiteCode.Trim(),
            command.Description.Trim(),
            command.OrderNumber.Trim(),
            command.ReportedBy.Trim(),
            command.SiteContactName.Trim(),
            command.ReceivedAt.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture),
            command.ResponseCategoryCode.ToString(CultureInfo.InvariantCulture),
            command.SectionCode.Trim(),
            command.SubsectionCode.Trim(),
            command.RequestedEngineerEmployeeId.ToString(CultureInfo.InvariantCulture),
            username.Trim(),
            callTakenBy.Trim(),
            jobType.ToString(CultureInfo.InvariantCulture));
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(payload)));
    }
}
