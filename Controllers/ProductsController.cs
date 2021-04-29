using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace backendApi.Controllers
{

    [ApiController]
    [Authorize]

    public class ProductsController : ControllerBase
    {
        private ProductsData.IProductsData _ProductsData;
        private IWebHostEnvironment _webhost;
        public ProductsController(ProductsData.IProductsData ProductsDta, IWebHostEnvironment webhost)
        {
            _ProductsData = ProductsDta;
            _webhost = webhost;
        }
        [HttpGet]
        [Route("/api/[controller]")]

        public IActionResult GetProductss()
        {
            return Ok(_ProductsData.GetProducts());

        }

        [HttpGet]
        [Route("/api/[controller]/{id}")]

        public IActionResult GetProducts(Guid id)
        {
            var Products = _ProductsData.GetProducts(id);
            if (Products != null)
            {
                return Ok(Products);
            }

            return NotFound($"Product with id :{id} not found");

        }


        [HttpPost]
        [Route("/api/[controller]")]

        public async Task<IActionResult> GetProductsAsync(Products Products)
        {
            // Products.ImageName = await Post();
            _ProductsData.AddProducts(Products);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + Products.ProdId, Products);
        }

        [HttpDelete]
        [Route("/api/[controller]/{id}")]

        public IActionResult DeleteProducts(Guid id)
        {
            var Products = _ProductsData.GetProducts(id);
            if (Products != null)
            {
                _ProductsData.DeleteProducts(Products);
                return Ok("Deleted sucessfully");
            }

            return NotFound($"Product with id :{id} not found");



        }

        [HttpPut]
        [Route("/api/[controller]/{id}")]

        public IActionResult EditProducts(Guid id, Models.Products Products)
        {
            var existing_Products = _ProductsData.GetProducts(id);

            if (existing_Products != null)
            {
                Products.ProdId = existing_Products.ProdId;
                _ProductsData.EditProducts(existing_Products);
            }

            return Ok(Products);




        }
        [NonAction]
        public async Task<string> SaveImage([FromForm] IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetFileNameWithoutExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webhost.ContentRootPath + "Uploads" + imageName);
            using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [HttpPost]
        [Route("api/fileUpload")]
      
        public async Task<string> Post([FromForm]  FileUpload  objectFile)
        {

            try
            {
                if (objectFile.ImageFile.Length > 0)
                {
                    string path = _webhost.WebRootPath + "//Uploads//";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = System.IO.File.Create(path + objectFile.ImageFile.FileName))
                    {
                       await objectFile.ImageFile.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return "//Uploads//" + objectFile.ImageFile.FileName;
                    }
                }
                else
                {
                    return "Not Uploaded";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}