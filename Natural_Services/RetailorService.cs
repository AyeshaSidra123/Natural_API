﻿using Natural_Core.IServices;
using Natural_Core.Models;
using Natural_Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Natural_Core.IRepositories;
using Natural_Data.Repositories;

#nullable disable

namespace Natural_Services
{
    public class RetailorService : IRetailorService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RetailorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Retailor>> GetAllRetailors()
        {
            var result = await _unitOfWork.RetailorRepo.GetAllRetailorsAsync();
            return result;
        }
        public async Task<Retailor> GetRetailorDetailsById(string retailorId)
        {
            return await _unitOfWork.RetailorRepo.GetWithRetailorsByIdAsync(retailorId);
        }
        public async Task<Retailor> GetRetailorsById(string retailorId)
        {
            return await _unitOfWork.RetailorRepo.GetByIdAsync(retailorId);
        }

        public async Task<ResultResponse> CreateRetailorWithAssociationsAsync(Retailor retailor)
        {
            var response = new ResultResponse();

            try
            {
                retailor.Id = "NRET" + new Random().Next(10000, 99999).ToString();

                await _unitOfWork.RetailorRepo.AddAsync(retailor);

                var created = await _unitOfWork.CommitAsync();

                if (created != 0)
                {
                    response.Message = "Insertion Successful";
                    response.StatusCode = 200;
                }
            }
            catch (Exception)
            {

                response.Message = "Insertion Failed";
                response.StatusCode = 401;
            }

            return response;
        }
        public async Task<ResultResponse> UpdateRetailors(Retailor existingRetailor, Retailor retailor)
        {

            var response = new ResultResponse();

            try
            {
                await _unitOfWork.RetailorRepo.UpdateRetailorAsync(existingRetailor, retailor);

                await _unitOfWork.CommitAsync();


                response.Message = "Update Successfull";
                response.StatusCode = 200;
            }

            catch (Exception)
            {

                response.Message = "Update Failed";
                response.StatusCode = 400;

            }
            return response;

        }


        public async Task<ResultResponse> DeleteRetailor(string retailorId)
        {
            var response = new ResultResponse();

            try
            {
                var retailor = await _unitOfWork.RetailorRepo.GetByIdAsync(retailorId);

                if (retailor!= null)
                {
                    _unitOfWork.RetailorRepo.Remove(retailor);
                    await _unitOfWork.CommitAsync();
                    response.Message = "SUCCESSFULLY DELETED";
                    response.StatusCode = 200;
                }
                else
                {
                    response.Message = "RETAILER NOT FOUND";
                    response.StatusCode = 404;
                }
            }
            catch (Exception)
            {
                response.Message = "Internal Server Error";
            }

            return response;
        }

        public async Task<IEnumerable<Retailor>> SearcRetailors(SearchModel search)
        {
            var exec = await _unitOfWork.RetailorRepo.SearchRetailorAsync(search);
            return exec;
        }
    }
}
 
       
