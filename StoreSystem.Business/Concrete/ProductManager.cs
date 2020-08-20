using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using StoreSystem.Business.Abstract;
using StoreSystem.Business.ValidationRules;
using StoreSystem.Business.ValidationRules.FluentValidation;
using StoreSystem.DataAcccesLayer.Abstract;
using StoreSystem.DataAcccesLayer.Concrete.EntityFramework;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IValidator<Product> _productValidator;
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal, IValidator<Product> productValidator)
        {
            _productDal =productDal;  //NHİBERNATE ,DAPPER VAR 
            _productValidator = productValidator;
        }

        public string Add(Product product)
        {
            var validateResult = _productValidator.Validate(product);
            if (validateResult.IsValid)
            {
                _productDal.Add(product);
            }

            return validateResult.IsValid ? "Success" : validateResult.Errors.First().ErrorMessage;
            ;
            // throw new ValidationException(validateResult.Errors);

        }


        public void Delete(int productId)
        {
            _productDal.Delete(new Product { ProductId = productId });
        }

        public List<Product> GetAll(string productName)
        {
            return _productDal.GetList(p => p.ProductName == productName || productName == null);
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _productDal.GetList(filter: p => p.CategoryId == categoryId || categoryId == 0);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId); 
        }
        public List<Product> GetByName(string productName)
        {
            return _productDal.GetList(p => p.ProductName == productName || productName == null);
        }


        public string Update(Product product)
        {
            var validateResult = _productValidator.Validate(product);
            if (validateResult.IsValid)
            {
                _productDal.Update(product);
            }
            return validateResult.IsValid ? "Success" : validateResult.Errors.First().ErrorMessage;
        }
    }
}