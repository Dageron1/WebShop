using WebShop.DataAccess.Repository.IRepository;
using WebShop.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace WebShop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IProductRepository Product { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }  
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IProductImageRepository ProductImage { get; private set; }
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UnitOfWork(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            ProductImage = new ProductImageRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);   
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db); 
            _hostingEnvironment = hostEnvironment;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public async void UploadImage(IFormFile file)
        {
            long totalBytes = file.Length;
            string fileName = file.FileName.Trim('"');
            fileName = EnsureFileName(fileName);
            byte[] buffer = new byte[16 * 1024];
            using (FileStream output = System.IO.File.Create(GetpathAndFileName(fileName)))
            {
                using (Stream input = file.OpenReadStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, readBytes);
                        totalBytes += readBytes;
                    }
                }
            }
        }
        private string GetpathAndFileName(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\uploads\\";
            if(!Directory.Exists(path)) 
            { 
                Directory.CreateDirectory(path);
                
            }
            return path + fileName;
        }
        private string EnsureFileName(string fileName)
        {
            if (fileName.Contains("\\"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("\\") +1);
            }
            return fileName;
        }
    }
}
