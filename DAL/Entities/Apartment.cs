using System.ComponentModel.DataAnnotations;

namespace NTBrokers.DAL.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public int FlatFloor { get; set; }
        public int BuildingFloors { get; set; }
        public int Area { get; set; }
        public int? BrokerId { get; set; } = null;
        [Required]
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
