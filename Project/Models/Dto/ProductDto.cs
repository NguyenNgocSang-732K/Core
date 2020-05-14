using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;
using Project.Models.ViewModels;
using Supports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dto
{
    public class ProductDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public ProductDto()
        {
            db = new DBContext();
        }

        public List<ProductView> GetData(int page)
        {
            int start = size * (page - 1);
            return db.Product.AsNoTracking().Where(s => s.Status).OrderByDescending(s => s.Id).Skip(start).Skip(size).Select(s => new ProductView
            {
                Id = s.Id,
                Active = s.Active,
                Code = s.Code,
                DayCreate = s.Daycreate,
                DayEdited = s.Dayedited,
                Description = s.Description,
                Name = s.Name,
                Price = s.Price,
                Quantity = s.Quantity,
                Photo = s.ProductPhoto.SingleOrDefault(s => s.Main).Photo,
                Colors = s.Color.Split(new char[] { ',' }).ToList()
            }).ToList();
        }

        public ProductView GetById(int id)
        {
            return db.Product.AsNoTracking().Where(s => s.Status && s.Id == id).Select(s => new ProductView
            {
                Id = s.Id,
                Active = s.Active,
                Code = s.Code,
                DayCreate = s.Daycreate,
                DayEdited = s.Dayedited,
                Description = s.Description,
                Name = s.Name,
                Price = s.Price,
                Quantity = s.Quantity,
                Photo = s.ProductPhoto.SingleOrDefault(s => s.Main).Photo,
                EditerId = (int)s.EditerId,
                Colors = s.Color.Split(new char[] { ',' }).ToList()
            }).SingleOrDefault();
        }

        public int Create(ProductView productView)
        {
            try
            {
                string colors = string.Join(",", productView.Colors);
                if (CheckExists(productView)) return -1;
                Product product = new Product
                {
                    Active = productView.Active,
                    Code = productView.Code,
                    Daycreate = productView.DayCreate,
                    Dayedited = productView.DayEdited,
                    Description = productView.Description,
                    Name = productView.Name,
                    Price = productView.Price,
                    Id = productView.Id,
                    Quantity = productView.Quantity,
                    Status = productView.Status,
                    Subcateid = productView.SubCategory.Id,
                    Unitid = productView.Unit.id,
                    Color = colors
                };
                db.Product.Add(product);
                db.SaveChanges();
                return product.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public bool Modify(ProductView productView)
        {
            try
            {
                string colors = string.Join(",", productView.Colors);
                if (CheckExists(productView)) return false;
                Product product = db.Product.Find(productView.Id);
                product.Name = productView.Name;
                product.Code = productView.Code;
                product.Dayedited = DateTime.Now;
                product.Price = productView.Price;
                product.Quantity = productView.Quantity;
                product.Subcateid = productView.SubCategory.Id;
                product.Unitid = productView.Unit.id;
                product.Description = product.Description;
                product.Color = colors;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool Active(int id)
        {
            try
            {
                Product product = db.Product.Find(id);
                product.Active = !product.Active;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool SetStatus(int id)
        {
            try
            {
                Product product = db.Product.Find(id);
                product.Status = !product.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public List<ProductView> SearchByName(int page, string textsearch)
        {
            int start = size * (page - 1);
            return db.Product.FromSqlRaw($"SELECT * FROM product WHERE product.name {search} like '%{textsearch}%'").AsNoTracking().Where(s => s.Status).Skip(start).Take(size).Select(s => new ProductView
            {
                Id = s.Id,
                Active = s.Active,
                Code = s.Code,
                DayCreate = s.Daycreate,
                DayEdited = s.Dayedited,
                Description = s.Description,
                Name = s.Name,
                Price = s.Price,
                Quantity = s.Quantity,
                Photo = s.ProductPhoto.SingleOrDefault(s => s.Main).Photo
            }).ToList();
        }

        public int GetRowCountSearchByName(string textsearch)
        {
            return db.Product.FromSqlRaw($"SELECT * FROM product WHERE product.name {search} like '%{textsearch}%'").AsNoTracking().Where(s => s.Status).Count();
        }

        private bool CheckExists(ProductView productView)
        {
            Product product = db.Product.AsNoTracking().SingleOrDefault(s => s.Name.ToLower().Trim() == productView.Name.ToLower().Trim() ||
            s.Code.ToLower().Trim() == productView.Code.ToLower().Trim());
            return product == null ? true : false;
        }
    }
}
