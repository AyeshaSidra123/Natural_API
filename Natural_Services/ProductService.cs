﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Natural_Core;
using Natural_Core.IServices;
using Natural_Core.Models;
using Natural_Core.S3Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace Natural_Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;
        private readonly S3Config _s3Config;
        private readonly ICategoryService _categoryService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<S3Config> s3Config, ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
            _s3Config = s3Config.Value;
            _categoryService = categoryService;
        }

        // insert product data to database//
        public async Task<ProductResponse> CreateProduct(Product product)
        {
            {
                var response = new ProductResponse();

                try
                {

                    product.Id = "PROD" + new Random().Next(10000, 99999).ToString();

                    await _unitOfWork.ProductRepository.AddAsync(product);

                    var created = await _unitOfWork.CommitAsync();

                    if (created != 0)
                    {
                        response.Message = "Insertion Successful";
                        response.StatusCode = 200;
                        await _unitOfWork.ProductRepository.AddAsync(product);
                        response.Id = product.Id;
                    }
                }
                catch (Exception)
                {

                    response.Message = "Insertion Failed";
                    response.StatusCode = 401;
                }

                return response;
            }
        }


        //get bucket names//
        public async Task<IEnumerable<string>> GetAllBucketAsync()
        {
            var bucketlist = await _unitOfWork.ProductRepository.GetAllBucketAsync();
            return bucketlist;
        }


        //get all files //all images with presignedurl
        public async Task<IEnumerable<S3Config>> GetAllFilesAsync(string bucketName, string prefix)
        {
            var Allfilename = await _unitOfWork.ProductRepository.GetAllFilesAsync(bucketName, prefix);
            return Allfilename;
        }


        //get products with category name
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var result = await _unitOfWork.ProductRepository.GetProducttAsync();
            //var PresentinCategory = result.Where(d => d.IsDeleted != true).ToList();
            return result;
        }


        //get products with category name and presignred url//
        public async Task<IEnumerable<GetProduct>> GetAllPrtoductDetails(string prefix)
        {
            var productresoursze = await GetAllProduct();

            
            string bucketName = _s3Config.BucketName;
            var PresignedUrl = await GetAllFilesAsync(bucketName, prefix);

            var leftJoinQuery = from Produc in productresoursze
                                join Presigned in PresignedUrl
                                on Produc.Image equals Presigned.Image into newurl
                                from sub in newurl.DefaultIfEmpty()
                                select new GetProduct()
                                {
                                    Id = Produc.Id,
                                    Category = Produc.Category,
                                    ProductName = Produc.ProductName,
                                    Price = Produc.Price,
                                    Quantity = Produc.Quantity,
                                    Weight = Produc.Weight,
                                    PresignedUrl = sub?.PresignedUrl,

                                };
            return leftJoinQuery;
        }


        //get product by id -category name -presigned url
        public async Task<GetProduct> GetProductDetailsByIdAsync(string ProductId)
        {
            var productResult = await _unitOfWork.ProductRepository.GetProductByIdAsync(ProductId);
           
            if (string.IsNullOrEmpty(productResult.Image))
            {
                var productresoursze1 = _Mapper.Map<Product, GetProduct>(productResult);

                return productresoursze1;

            }
            else
            {
                string bucketName = _s3Config.BucketName;
                string prefix = productResult.Image;
                var PresignedUrl = await GetAllFilesAsync(bucketName, prefix);
                if (PresignedUrl.Any())
                {
                    var isd = PresignedUrl.FirstOrDefault();
                    var productresoursze1 = _Mapper.Map<Product, GetProduct>(productResult);
                    productresoursze1.PresignedUrl = isd.PresignedUrl;

                    return productresoursze1;
                }
                else
                {
                    var productresoursze1 = _Mapper.Map<Product, GetProduct>(productResult);

                    return productresoursze1;
                }

            }

        }


        public async Task<GetProduct> GetProductpresignedurlByIdAsync(string ProductId)
        {


            var productResult = await _unitOfWork.ProductRepository.GetByIdAsync(ProductId);
           
          

            if (string.IsNullOrEmpty(productResult.Image))
            {
                var productresoursze1 = _Mapper.Map<Product, GetProduct>(productResult);

                return productresoursze1;

            }
            else
            {
                string bucketName = _s3Config.BucketName;
                string prefix = productResult.Image;
                var PresignedUrl = await GetAllFilesAsync(bucketName, prefix);

                if (PresignedUrl.Any())
                {
                    var isd = PresignedUrl.FirstOrDefault();
                    var productresoursze1 = _Mapper.Map<Product, GetProduct>(productResult);
                    productresoursze1.PresignedUrl = isd.PresignedUrl;

                    return productresoursze1;
                }
                else
                {
                    var productresoursze1 = _Mapper.Map<Product, GetProduct>(productResult);

                    return productresoursze1;

                }
            }
        }
        //get product by id as in tabel 

        public async Task<Product> GetProductByIdAsync(string ProductId)
        {

            //var productResult = await _unitOfWork.ProductRepository.GetByIdAsync(ProductId);

            //return productResult;

            var productResult = await _unitOfWork.ProductRepository.GetByIdAsync(ProductId);
            if (productResult != null && productResult.IsDeleted != true)
            {
                return productResult;
            }
            else
            {
                
                return null;
            }


        }


        //updating product data to db
        public async Task<ProductResponse> UpadateProduct(Product product)
        {
            var response = new ProductResponse();
            try
            {
                _unitOfWork.ProductRepository.Update(product);
                var updated = await _unitOfWork.CommitAsync();
                if (updated != 0)
                {

                    response.Message = "updatesuceesfull";
                    response.StatusCode = 200;
                }
            }
            catch (Exception)
            {
                response.Message = "Failed";
                response.StatusCode = 500;
            }

            return (response);
        }


        //upload images to s3 bucket
        public async Task<UploadResult> UploadFileAsync(IFormFile file, string prefix)
        {
           
            string bucketName = _s3Config.BucketName;
            var metadata = await _unitOfWork.ProductRepository.UploadFileAsync(file, bucketName, prefix);
            return metadata;

        }


        //if while updating i want to delete image
        public async Task<ProductResponse> DeleteImage(string Id)
        {
            var product1 = await GetProductByIdAsync(Id);
            var response = new ProductResponse();
            if (product1 == null)
            {
                response.Message = "product does not exit";
            }
            string key = product1.Image;
           
            string bucketName = _s3Config.BucketName;
            await _unitOfWork.ProductRepository.DeleteImageAsync(bucketName, key);
            response.Message = "Image deleted";
          
            return response;
        }


        //if you want to delete Product complete from db including image
        //public async Task<ProductResponse> DeleteProduct(string ProductId)
        //{

        //    var response = new ProductResponse();
        //    try
        //    {
        //        var product1 = await GetProductByIdAsync(ProductId);

        //        if (product1 != null)
        //        {
                   
        //            string bucketName = _s3Config.BucketName;
        //            string key = product1.Image;
        //            bool imageDeletionResult = await _unitOfWork.ProductRepository.DeleteImageAsync(bucketName, key);
                    
        //            _unitOfWork.ProductRepository.Remove(product1);
        //            await _unitOfWork.CommitAsync();
        //            response.Message = "SUCCESSFULLY DELETED";
        //            response.StatusCode = 200;
        //        }
                
        //        else
        //        {
        //            response.Message = "Product NOT FOUND";
        //            response.StatusCode = 404;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        response.Message = "Internal Server Error";
        //    }

        //    return response;
        //}



        public async Task<ProductResponse> DeleteProduct(string ProductId)
        {

            var response = new ProductResponse();
            try
            {
                var product1 = await GetProductByIdAsync(ProductId);

                if (product1 != null)
                {
                    product1.IsDeleted = true;
                    string bucketName = _s3Config.BucketName;
                    string key = product1.Image;
                    bool imageDeletionResult = await _unitOfWork.ProductRepository.DeleteImageAsync(bucketName, key);

                    _unitOfWork.ProductRepository.Update(product1);
                    await _unitOfWork.CommitAsync();
                    response.Message = "SUCCESSFULLY DELETED";
                    response.StatusCode = 200;
                }

                else
                {
                    response.Message = "Product NOT FOUND";
                    response.StatusCode = 404;
                }
            }
            catch (Exception)
            {
                response.Message = "Internal Server Error";
            }

            return response;
        }


        //search product based on category and product
        public async Task<IEnumerable<GetProduct>> SearchProduct(SearchProduct search)
        {
            string prefix = "";
            var getProduct = await GetAllPrtoductDetails(prefix);


            List<GetProduct> exec = new List<GetProduct>();
            exec = getProduct
             .Where(c =>
                    (string.IsNullOrEmpty(search.Category) || c.Category.StartsWith(search.Category)) &&
                    (string.IsNullOrEmpty(search.ProductName) || c.ProductName.StartsWith(search.ProductName, StringComparison.OrdinalIgnoreCase))
                    && c.IsDeleted != true
                )
                .Select(c => new GetProduct
                {
                    Id = c.Id,
                    Category = c.Category,
                    ProductName = c.ProductName,
                    Price = c.Price,
                    Quantity = c.Quantity,
                    Weight = c.Weight,
                    PresignedUrl = c.PresignedUrl
                })
                .ToList();

            return exec;
        }

    }
}

