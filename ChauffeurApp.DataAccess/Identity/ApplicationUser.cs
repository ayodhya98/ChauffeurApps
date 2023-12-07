using ChauffeurApp.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace ChauffeurApp.Core.Entities
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public ActiveStatus ActiveStatus { get; set; } = ActiveStatus.Active;
    }
}
