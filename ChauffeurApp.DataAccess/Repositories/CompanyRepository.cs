using ChauffeurApp.Core.Entities;
using ChauffeurApp.DataAccess.Persistence;
using ChauffeurApp.DataAccess.Repositories.IRepositories;

namespace ChauffeurApp.DataAccess.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
