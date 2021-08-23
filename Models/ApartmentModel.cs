namespace NTBrokers.Models
{
    public class ApartmentModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Address { get; set; }
        public int FlatFloor { get; set; }
        public int BuildingFloors { get; set; }
        public int Area { get; set; }
        public int BrokerId { get; set; }
        public int CompanyId { get; set; }
    }
}
