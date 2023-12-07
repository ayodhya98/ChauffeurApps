namespace ChauffeurApp.Application.DTOs
{
    public class CompanyDTO
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
