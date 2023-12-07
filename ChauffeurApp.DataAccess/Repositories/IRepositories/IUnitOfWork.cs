namespace ChauffeurApp.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
       IBrandRepository Brands { get; }
       ICompanyRepository Companys { get; }
       Task SaveAsync();
    }
}
