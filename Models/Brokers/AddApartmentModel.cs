using System.Collections.Generic;
using NTBrokers.Models.Apartments;

namespace NTBrokers.Models.Brokers
{
    public class AddApartmentModel
    {
        public int BrokerId { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
    }
}
