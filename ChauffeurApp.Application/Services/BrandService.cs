﻿using AutoMapper;
using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Application.Services.IServices;
using ChauffeurApp.Core.Entities;
using ChauffeurApp.DataAccess.Repositories.IRepositories;

namespace ChauffeurApp.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<BrandDTO>> CreateBrand(BrandCreateDTO createBrandDTO)
        {
            if (createBrandDTO == null) return Result<BrandDTO>.Failure("Invalid input");

            Brand brand = _mapper.Map<Brand>(createBrandDTO);

            // need to do some validations
            await _unitOfWork.Brands.AddAsync(brand);
            await _unitOfWork.SaveAsync();

            BrandDTO brandDTO = _mapper.Map<BrandDTO>(brand);

            return Result<BrandDTO>.Success(brandDTO);
        }

        public async Task<Result<bool>> DeleteBrand(long id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);

            if (brand != null)
            {
                _unitOfWork.Brands.Remove(brand);
                await _unitOfWork.SaveAsync();
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Brand does not exist");

        }

        public async Task<Result<BrandDTO>> UpdateBrand(BrandCreateDTO updatedDTO, long ID)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(ID);
            if (brand != null)
            {
                brand.Name = updatedDTO.Name;
                brand.Features = updatedDTO.Features;

                _unitOfWork.Brands.Update(brand);
                await _unitOfWork.SaveAsync();

                BrandDTO mappedBrand = _mapper.Map<BrandDTO>(brand);
                return Result<BrandDTO>.Success(mappedBrand);
            }
            return Result<BrandDTO>.Failure("Cannot update brand");
        }

        public async Task<Result<List<BrandDTO>>> ViewAllBrands()
        {
            var listOfBrands = await _unitOfWork.Brands.GetAllAsync();

            if (listOfBrands == null)
            {
                Result<List<BrandDTO>>.Failure("Brands not found");
            }
            List<BrandDTO> brands = _mapper.Map<List<BrandDTO>>(listOfBrands);
            return Result<List<BrandDTO>>.Success(brands);
        }

        public async Task<Result<BrandDTO>> ViewBrandByID(long id)
        {
            var brand = await _unitOfWork.Brands.GetByIdAsync(id);

            if (brand == null)
            {
                Result<BrandDTO>.Failure("Brand does not exist");
            }
            BrandDTO brandDTO = _mapper.Map<BrandDTO>(brand);
            return Result<BrandDTO>.Success(brandDTO);
        }


    }
}
