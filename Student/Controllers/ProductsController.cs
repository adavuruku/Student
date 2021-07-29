using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Student.Dto;
using Student.Models;
using Student.Services;

namespace Student.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            var products = await _productService.GetAllItems();
            return Ok(products);
        }

        //get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetAProduct(int id)
        {
            
            var product = await _productService.GetAnItem(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        [HttpPost]
        public async Task<ActionResult<Product>> CreateItems(ProductDto itemDto)
        {
            Product productY = new()
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                DateCreated = DateTime.Now
            };

            await _productService.AddProduct(productY);
            return Ok();
            //return CreatedAtAction(nameof(GetAProduct), new {id = productY.ProductId}, productY());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(int id, UpdateProductDto productDto)
        {
            var existingItem = await _productService.GetAnItem(id);
            if (existingItem is null)
            {
                return NotFound();
            }

            Product updatedItem = existingItem with
            {
                ProductId = existingItem.ProductId,
                Name = productDto.Name,
                Price = productDto.Price
            };
            
           await _productService.UpdateProduct(updatedItem);
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await  _productService.DeleteProduct(id);
            return Ok();
        }
    }
}