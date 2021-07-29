using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models;
using Student.Services;

namespace Student.Repositories
{
    public class ProductImplementation:ProductService
    {
        

        private readonly IDataContext _context;
        
        public ProductImplementation(IDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllItems()
        {
            var allItemRecord = await _context.Products.ToListAsync();
            return allItemRecord;
        }

        public async Task<Product> GetAnItem(int id)
        {
            var itemRecord = await _context.Products.FindAsync(id);
            return itemRecord;
        }

        public async Task UpdateProduct(Product product)
        {
            var itemToUpdate = await _context.Products.FindAsync(product.ProductId);
            if (itemToUpdate == null)
                throw new NullReferenceException();
            itemToUpdate.Name = product.Name;
            itemToUpdate.Price = product.Price; 
            await _context.SaveChangesAsync();
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var itemToDelete = await _context.Products.FindAsync(id);
            if (itemToDelete == null)
                throw new NullReferenceException();
            _context.Products.Remove(itemToDelete);
            await _context.SaveChangesAsync();
        }
    }
}