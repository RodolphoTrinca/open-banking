using OpenBanking.Application.Entity;
using OpenBanking.Worker.DTO;

namespace OpenBanking.Worker.Helper
{
    public static class BankDataExtentions
    {
        internal static void UpdateData(this BankData bankData, CompanyData newData)
        {
            bankData.Name = newData.OrganisationName;
            bankData.Status = newData.Status;
            bankData.AutorizationServers = newData.AuthorisationServers
                .Select(a => a.ToAutorizationServer())
                .ToList();
        }
    }
}
