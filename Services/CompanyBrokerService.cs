using System.Collections.Generic;
using System.Linq;

namespace NTBrokers.Services
{
    public class CompanyBrokerService
    {
        public CompanyBrokerService()
        {
        }

        public void GetCompanyBrokersID(int[] createFormSelectedBrokers, List<int> existingBrokers,
                                        out List<int> brokersToRemove, out List<int> brokersToAdd)
        {
            brokersToRemove = null;
            brokersToAdd = null;
            List<int> selectedBrokers = createFormSelectedBrokers?.ToList();
            if (selectedBrokers == null)
            {
                brokersToRemove = existingBrokers;
            }
            else
            {
                brokersToRemove = existingBrokers.Where(x => !selectedBrokers.Contains(x))?.ToList();
                brokersToAdd = selectedBrokers.Where(x => !existingBrokers.Contains(x)).ToList();
            }
        }
    }
}
