using Application.Services.Interfaces;

namespace Application.Services.Services;

public class BlackListReportService : IIsBlackListed
{
    public bool IsBlacklisted(string dni)
    {
        return dni == "000000Z";
    }
}
