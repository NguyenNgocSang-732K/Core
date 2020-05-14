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
    public class ProductPhotoDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public ProductPhotoDto()
        {
            db = new DBContext();
        }

        public List<ProductPhotoView> GetDataByProductId(int proId)
        {
            return db.ProductPhoto.AsNoTracking().Select(s => new ProductPhotoView
            {
                Id = s.Id,
                Main = s.Main,
                Photo = s.Photo,
                ProId = s.Proid
            }).ToList();
        }

        public bool Create(List<ProductPhotoView> productPhotoViews)
        {
            try
            {
                List<ProductPhoto> list = new List<ProductPhoto>();
                productPhotoViews.ForEach(s =>
                {
                    list.Add(new ProductPhoto
                    {
                        Id = s.Id,
                        Main = s.Main,
                        Photo = s.Photo,
                        Proid = s.ProId
                    });
                });
                db.ProductPhoto.AddRange(list);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                db.ProductPhoto.Remove(db.ProductPhoto.Find(id));
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
