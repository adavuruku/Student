using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Student.Models;

namespace Student.Services
{
    public interface ProductService
    {
        Task<IEnumerable<Product>> GetAllItems();
        Task<Product> GetAnItem(int id);
        Task UpdateProduct(Product product);
        
        Task AddProduct(Product product);
        
        Task DeleteProduct(int id);
    }
}