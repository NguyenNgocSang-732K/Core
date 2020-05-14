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
    public class SubCateroryDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public SubCateroryDto()
        {
            db = new DBContext();
        }

        public List<SubCateView> GetData(int cateId)
        {
            return db.SubCategory.AsNoTracking().Where(s => s.Cateid == cateId && s.Status).Select(s => new SubCateView
            {
                CateId = (int)s.Cateid,
                Id = s.Id,
                Name = s.Name,
            }).ToList();
        }

        public List<SubCateView> SearchByName(string textsearch)
        {
            return db.SubCategory.FromSqlRaw($"SELECT * FROM sub_category WHERE name {search} like '%{textsearch}%'").AsNoTracking().Where(s=>s.Status).Select(s => new SubCateView
            {
                CateId = (int)s.Cateid,
                Id = s.Id,
                Name = s.Name,
            }).ToList();
        }

        public int Create(SubCateView subCateView)
        {
            try
            {
                SubCategory subCategory = new SubCategory
                {
                    Cateid = subCateView.CateId,
                    Name = subCateView.Name,
                    Status = subCateView.Status,
                };
                db.SubCategory.Add(subCategory);
                db.SaveChanges();
                return subCategory.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public bool Modify(SubCateView subCateView)
        {
            try
            {
                SubCategory subCategory = db.SubCategory.Find(subCateView.Id);
                subCategory.Name = subCateView.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool SetStatus(int subId)
        {
            try
            {
                SubCategory subCategory = db.SubCategory.Find(subId);
                subCategory.Status = !subCategory.Status;
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
