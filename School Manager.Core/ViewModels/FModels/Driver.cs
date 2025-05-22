using School_Manager.Domain.Entities.Catalog.Enums;

namespace School_Manager.Core.ViewModels.FModels
{
    
    public class DriverDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FutherName { get; set; }
        public DateTime BirthDate {  get; set; }
        public string CertificateId { get; set; }
        public string Education { get; set; }
        public string Descriptions { get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; }
        public string BankNumber { get; set; }
        public string NationCode { get; set; }
        public List<ChildInfo> Passanger { get; set; }
        public CarInfoDto Car { get; set; }
    }
}
