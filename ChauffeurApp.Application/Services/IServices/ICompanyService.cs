using ChauffeurApp.Application.DTOs;

namespace ChauffeurApp.Application.Services.IServices
{
    public interface ICompanyService
    {
        Task<Result<CompanyDTO>> CreateCompany(CreateCompanyDTO createCompanyDTO);
        Task<Result<List<CompanyDTO>>> ViewAllCompanies();
        Task<Result<CompanyDTO>> ViewCompanyById(long id);
        Task<Result<bool>> DeleteCompany(long id);
        Task<Result<CompanyDTO>> UpdateCompany(CreateCompanyDTO updateCompanyDTO, long ID);
    }
}
