using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
namespace ProductsData
{
    public class SqlProductData : IProductsData
    {

        private ApplicationDbContext _ProductsContext;
        public SqlProductData(ApplicationDbContext ProductsContext)
        {
            _ProductsContext = ProductsContext;

        }
        public Products AddProducts(Products Products)
        {
            Products.ProdId = Guid.NewGuid();
            _ProductsContext.products.Add(Products);
            _ProductsContext.SaveChanges();
            return Products;
        }

        public void DeleteProducts(Products Products)
        {
            _ProductsContext.products.Remove(Products);
            _ProductsContext.SaveChanges();
        }

        public Products EditProducts(Products Products)
        {
            var existing_Products = _ProductsContext.products.Find(Products.ProdId);
            if (existing_Products != null)
            {
                existing_Products.ProdName = Products.ProdName;
                existing_Products.Category = Products.Category;
                existing_Products.Discription = Products.Discription;
                existing_Products.isAvailable = Products.isAvailable;
                existing_Products.UnitPrice = Products.UnitPrice;
                existing_Products.StockQty = Products.StockQty;
                _ProductsContext.products.Update(existing_Products);
                _ProductsContext.SaveChanges();
            }
            return Products;

        }

        public Products GetProducts(Guid id)
        {
            var Products = _ProductsContext.products.Find(id);
            return Products;
        }

        public List<Products> Getproducts()
        {
            return _ProductsContext.products.ToList();
        }

        List<Products> IProductsData.GetProducts()
        {
            return _ProductsContext.products.ToList();
        }

        // public List<Products> GetProducts()
        // {
        //     throw new NotImplementedException();
        // }
    }
}