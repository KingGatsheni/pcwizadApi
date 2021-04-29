
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProductsData
{
    public interface  IProductsData
    {
        List<Models.Products> GetProducts();

        Models.Products GetProducts(Guid id);

        Models.Products AddProducts(Models.Products Products);

        void DeleteProducts(Models.Products Products);

        Models.Products EditProducts(Models.Products products);      
    }
}