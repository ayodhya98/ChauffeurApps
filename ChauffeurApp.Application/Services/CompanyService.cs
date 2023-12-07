using AutoMapper;
using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Application.Services.IServices;
using ChauffeurApp.Core.Entities;
using ChauffeurApp.Core.Enums;
using ChauffeurApp.DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChauffeurApp.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result<CompanyDTO>> CreateCompany(CreateCompanyDTO createCompanyDTO)
        {
            if (createCompanyDTO == null) return Result<CompanyDTO>.Failure("Invalid input");

            Company company = _mapper.Map<Company>(createCompanyDTO);

            await _unitOfWork.Companys.AddAsync(company);
            await _unitOfWork.SaveAsync();

            //need to create application user acc

            var email = await _userManager.FindByEmailAsync(createCompanyDTO.Email);
            if (email != null)
            {
                return Result<CompanyDTO>.Failure("This email already exists!");
            }

            var phoneNumber = await _userManager.Users.Where(x => x.PhoneNumber == createCompanyDTO.PhoneNumber).FirstOrDefaultAsync();
            if (phoneNumber != null)
            {
                return Result<CompanyDTO>.Failure("A user with this phone number already exists");
            };

            var identityUser = new ApplicationUser()
            {
                FullName = createCompanyDTO.CompanyName,
                PhoneNumber = createCompanyDTO.PhoneNumber,
                UserName = createCompanyDTO.Email,
                Email = createCompanyDTO.Email
            };

            var result = await _userManager.CreateAsync(identityUser, createCompanyDTO.Password);
            if (!result.Succeeded)
            {
                return Result<CompanyDTO>.Failure("Error in creating the user");
            }

            var role = await _userManager.AddToRoleAsync(identityUser, Roles.Company.ToString());
            if (!role.Succeeded)
            {
                return Result<CompanyDTO>.Failure("Error in assigning role to user");
            }

            CompanyDTO companyDTO = _mapper.Map<CompanyDTO>(company);
            return Result<CompanyDTO>.Success(companyDTO);

        }

        public async Task<Result<bool>> DeleteCompany(long id)
        {
            var company = await _unitOfWork.Companys.GetByIdAsync(id);

            if(company != null)
            {
                _unitOfWork.Companys.Remove(company);
                await _unitOfWork.SaveAsync();
                return Result<bool>.Success(true);

            }
            return Result<bool>.Failure("Company Does not exist");
        }

        public async Task<Result<CompanyDTO>> UpdateCompany(CreateCompanyDTO updateCompanyDTO, long ID)
        {
            var company = await _unitOfWork.Companys.GetByIdAsync(ID);
            if(company != null) 
            {
            company.CompanyName = updateCompanyDTO.CompanyName;
            company.ContactNumber = updateCompanyDTO.PhoneNumber;
            company.ContactEmail = updateCompanyDTO.Email;

            _unitOfWork.Companys.Update(company);
            await _unitOfWork.SaveAsync();

            CompanyDTO mappedCompany = _mapper.Map<CompanyDTO>(company);
            return Result<CompanyDTO>.Success(mappedCompany);
            }

            return Result<CompanyDTO>.Failure("Cannot Update Company");
        }

        public async Task<Result<List<CompanyDTO>>> ViewAllCompanies()
        {
            var listOfCompanies = await _unitOfWork.Companys.GetAllAsync();

            if(listOfCompanies == null)
            {
                Result<List<CompanyDTO>>.Failure("Company Not Found");
            }
            List<CompanyDTO> companies = _mapper.Map<List<CompanyDTO>>(listOfCompanies);
            return Result<List<CompanyDTO>>.Success(companies);
        }

        public async Task<Result<CompanyDTO>> ViewCompanyById(long id)
        {
            var Company = await _unitOfWork.Companys.GetByIdAsync(id);

            if(Company == null)
            {
                Result<CompanyDTO>.Failure("Company Does Not Exist");
            }

            CompanyDTO companyDTO = _mapper.Map<CompanyDTO>(Company);
            return Result<CompanyDTO>.Success(companyDTO);
        }
    }
}
