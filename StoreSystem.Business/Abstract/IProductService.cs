using System;
using System.Collections.Generic;
using System.Text;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll(string productName);
        List<Product> GetByCategory(int categoryId);
        string Add(Product product);

        void Delete(int productId);
        string Update(Product product);
        Product GetById(int productId);
         List<Product> GetByName(string productName);
    }
}
