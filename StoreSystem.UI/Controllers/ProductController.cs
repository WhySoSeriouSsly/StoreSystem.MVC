#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using StoreSystem.Business.Abstract;
using StoreSystem.Entities.Concrete;
using StoreSystem.UI.Models;

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
        public IActionResult List(string? productName)
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

            var result = _productService.Add(product);

            TempData.Add("message", "Product was successfully added");


            return RedirectToAction("Add");//buraya return view(); dersek add viewini açmaya çalışacak
            //Böyle olunca category select listi dolu gelemeyecek o yüzden exception vericek 
            //category dolu gelmesi lazım o yüzden hata alırız.
        }


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
            if (ModelState.IsValid)
            {
                _productService.Update(product);
                TempData.Add("message", "Product was successfully updated");
            }

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

        //public ActionResult deneme()
        //{
        //     var resultmessage= _productService.Add2(new Product{CategoryId = 2});
        //     return Ok(resultmessage);
        //}

    }
}
