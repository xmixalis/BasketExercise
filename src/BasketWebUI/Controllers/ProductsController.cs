﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketWebUI.Interfaces;
using BasketWebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;

        // GET: Products
        [HttpGet]
        public ActionResult Index()
        {
            ProductsIndexViewModel p = _productService.GetProductItems();
            return View(p);
        }
    }
}