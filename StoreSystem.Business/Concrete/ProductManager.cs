﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using StoreSystem.Business.Abstract;
using StoreSystem.Business.ValidationRules;
using StoreSystem.Business.ValidationRules.FluentValidation;
using StoreSystem.Core.Aspects.Autofac.Transaction;
using StoreSystem.Core.Aspects.Autofac.Validation;
using StoreSystem.Core.CrossCuttingConcerns.Validation;
using StoreSystem.DataAcccesLayer.Abstract;
using StoreSystem.DataAcccesLayer.Concrete.EntityFramework;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.Concrete
{
    public class ProductManager : IProductService
    {
        //private readonly IValidator<Product> _productValidator;
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal, IValidator<Product> productValidator)
        {
            _productDal = productDal;  //NHİBERNATE ,DAPPER VAR 
        }

        [ValidationAspect(typeof(ProductValidator))]
        public void Add(Product product)
        {
            _productDal.Add(product);

        }

        //public void Add(Product product)
        //{

        //    ValidationTool.Validate(new ProductValidator(), product);
        //    _productDal.Add(product);
        //    ;
        //    // throw new ValidationException(validateResult.Errors);

        //}


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
         [TransactionScopeAspects]
        public void TransactionalOperations(Product product)
        {
            _productDal.Add(product);
            _productDal.Update(product);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId);
        }
        public List<Product> GetByName(string productName)
        {
            return _productDal.GetList(p => p.ProductName.Contains(productName) || productName == null);
        }


        [ValidationAspect(typeof(ProductValidator))]
        public void Update(Product product)
        {
                _productDal.Update(product);
        }

        //[ValidationAspect(typeof(ProductValidator))]
        //public string Update(Product product)
        //{
        //    var validateResult = _productValidator.Validate(product);
        //    if (validateResult.IsValid)
        //    {
        //        _productDal.Update(product);
        //    }
        //    return validateResult.IsValid ? "Success" : validateResult.Errors.First().ErrorMessage;
        //}
    }
}