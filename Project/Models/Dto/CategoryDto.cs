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
    public class CategoryDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public CategoryDto()
        {
            db = new DBContext();
        }

        public List<CategoryView> GetData(int page)
        {
            int start = size * (page - 1);
            return db.Category.AsNoTracking().Where(s => s.Status).Skip(start).Take(size).Select(s => new CategoryView
            {
                Name = s.Name,
                Id = s.Id,
            }).ToList();
        }

        public CategoryView GetById(int id)
        {
            return db.Category.AsNoTracking().Where(s => s.Status && s.Id == id).Select(s => new CategoryView
            {
                Name = s.Name,
                Id = s.Id,
            }).SingleOrDefault();
        }

        public List<CategoryView> SearchByName(string textsearch)
        {
            return db.Category.FromSqlRaw($"SELECT * FROM category WHERE name {search} like '%{textsearch}%'").AsNoTracking().Where(s => s.Status).Select(s => new CategoryView
            {
                Name = s.Name,
                Id = s.Id,
            }).ToList();
        }

        public int Create(CategoryView categoryView)
        {
            try
            {
                Category category = new Category
                {
                    Name = categoryView.Name,
                    Status = categoryView.Status
                };
                db.Category.Add(category);
                db.SaveChanges();
                return category.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public bool Modify(CategoryView categoryView)
        {
            try
            {
                Category category = db.Category.Find(categoryView.Id);
                category.Name = categoryView.Name;
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
                Category category = db.Category.Find(id);
                category.Status = !category.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}
