using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Project.Models.Dto;
using Project.Models.Entities;
using Project.Models.ViewModels;
using Supports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Business
{
    public class ProductBus
    {
        public List<ProductView> GetData(int page) => new ProductDto().GetData(page);

        public ProductView GetById(int id)
        {
            ProductView product = new ProductDto().GetById(id);
            product.EditerName = new UserDto().GetDataById(product.EditerId).Name;
            product.ListPhoto = new ProductPhotoDto().GetDataByProductId(product.Id).Select(s => s.Photo).ToList();
            return product;
        }

        public static int Create(ProductView productView)
        {
            try
            {
                int proid = new ProductDto().Create(productView);
                if (proid == 0) return proid;
                //Photo product
                List<ProductPhotoView> photos = new List<ProductPhotoView>();
                productView.ListPhoto.ForEach(s =>
                {
                    photos.Add(new ProductPhotoView
                    {
                        Photo = s,
                        ProId = proid,
                        Main = false
                    });
                });
                photos[0].Main = true;
                //End photos
                bool checkPhotos = new ProductPhotoDto().Create(photos);
                if (!checkPhotos)
                {
                    return -2; //Insert ảnh bị lỗi
                }
                return proid;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0; // insert product bị lỗi
            }
        }

        public bool Modify(ProductView productView)
        {
            try
            {
                List<ProductPhotoView> photos = new List<ProductPhotoView>();
                productView.ListPhoto.ForEach(s =>
                {
                    photos.Add(new ProductPhotoView
                    {
                        Photo = s,
                        ProId = productView.Id,
                        Main = false
                    });
                });
                bool checkPro = new ProductDto().Modify(productView);
                if (checkPro) return false;
                //xxxxx còn hình ảnh chưa modify được
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool UploadPhotos(List<int> listIdPhotoCurrent, List<string> listPhotoNameNew, int productId)
        {
            List<int> listIdPhotoOld = new ProductPhotoDto().GetDataByProductId(productId).Select(s => s.Id).ToList();
            List<int> listIdPhotoRemove = listIdPhotoOld.Except(listIdPhotoCurrent).ToList();
            listIdPhotoRemove.ForEach(s =>
            {
                new ProductPhotoDto().Remove(s);
            });
            List<ProductPhotoView> listProPhotos = new List<ProductPhotoView>();
            listPhotoNameNew.ForEach(s =>
            {
                listProPhotos.Add(new ProductPhotoView
                {
                    Photo = s,
                    ProId = productId
                });
            });
            return new ProductPhotoDto().Create(listProPhotos);
        }

        public bool Active(int id) => new ProductDto().Active(id);

        public bool SetStatus(int id) => new ProductDto().SetStatus(id);

        public List<ProductView> SearchByName(int page, string textsearch) => new ProductDto().SearchByName(page, textsearch);

        public int GetRowCountSearchByName(string textsearch) => new ProductDto().GetRowCountSearchByName(textsearch);
    }
}
