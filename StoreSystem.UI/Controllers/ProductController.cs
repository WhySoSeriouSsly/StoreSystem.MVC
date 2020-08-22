#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using StoreSystem.Business.Abstract;
using StoreSystem.Entities.Concrete;
using StoreSystem.UI.Models;
using StoreSystem.UI.Utilities.Messages;

namespace StoreSystem.UI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult List(string productName)
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetAll(productName)
            };
            return View(productListViewModel);
        }
        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {

            _productService.Add(product);
            TempData.Add("message", Messages.ProductAdded);
            return RedirectToAction("Add");//buraya return view(); dersek add viewini açmaya çalışacak
            //Böyle olunca category select listi dolu gelemeyecek o yüzden exception vericek 
            //category dolu gelmesi lazım o yüzden hata alırız.
        }

        public ActionResult add2()
        {
            _productService.Add(new Product{ProductName = "NAZIM",CategoryId = 2});
            return Ok();
        }

        //[HttpPost]
        //public ActionResult Add(Product product)
        //{

        //    _productService.Add(product);
        //    TempData.Add("message", Messages.ProductAdded);
        //    return RedirectToAction("Add");
        //    //buraya return view(); dersek add viewini açmaya çalışacak
        //    //Böyle olunca category select listi dolu gelemeyecek o yüzden exception vericek 
        //    //category dolu gelmesi lazım o yüzden hata alırız.

        //}


        public ActionResult Update(int productId)
        {
            var model = new ProductUpdateViewModel
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll()
            };

            return View(model);

        }

        [HttpPost]
        public ActionResult Update(Product product)
        {
            _productService.Update(product); 
            TempData.Add("message", Messages.ProductUpdated);
            return RedirectToAction("Update");
        }

        public ActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            //TempData.Add("message", "Product was successfully deleted");
            return RedirectToAction("List");
        }

        public ActionResult Search(string? productName)
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetByName(productName)
            };
            return View(productListViewModel);

        }

        public IActionResult Transaction(Product product)
        {
            _productService.TransactionalOperations(new Product
            {
                ProductName = "Melike",
                CategoryId = 2,
                UnitPrice = 5
            });
            return Ok();
        }

    }
}
