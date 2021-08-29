namespace NTBrokers.Models.Apartments
{
    public class ApartmentModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public int FlatFloor { get; set; }
        public int BuildingFloors { get; set; }
        public int Area { get; set; }
        public int? BrokerId { get; set; } = null;
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
    }
}
