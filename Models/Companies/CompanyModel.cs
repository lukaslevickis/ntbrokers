namespace NTBrokers.Models.Companies
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Address { get; set; }
        public string TableName { get; set; } = "Company";
    }
}
