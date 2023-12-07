using ChauffeurApp.Core.Common;

namespace ChauffeurApp.Core.Entities
{
    public class Company : BaseEntity,IAuditedEntity
    {
        public string CompanyName { get; set; }
        public string ContactEmail { get; set;}
        public string ContactNumber { get; set;}
        public DateTime RegisteredDate { get; set;}
        public DateTime? CreatedAt { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? DeletedById { get; set; }
    }
}
