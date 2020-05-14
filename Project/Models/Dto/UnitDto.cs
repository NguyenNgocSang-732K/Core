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
    public class UnitDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public UnitDto()
        {
            db = new DBContext();
        }

        public List<UnitView> GetData()
        {
            return db.Unit.AsNoTracking().Where(s => s.Status).Select(s => new UnitView
            {
                id = s.Id,
                Name = s.Name,
                NameConvert = s.Nameconvert,
                Quantity = s.Quantity,
            }).ToList();
        }

        public UnitView GetById(int id)
        {
            return db.Unit.AsNoTracking().Where(s => s.Id == id).Select(s => new UnitView
            {
                id = s.Id,
                Name = s.Name,
                NameConvert = s.Nameconvert,
                Quantity = s.Quantity
            }).SingleOrDefault();
        }

        public int Create(UnitView unitView)
        {
            try
            {
                Unit unit = new Unit
                {
                    Id = unitView.id,
                    Name = unitView.Name,
                    Nameconvert = unitView.NameConvert,
                    Quantity = unitView.Quantity
                };
                db.Unit.Add(unit);
                db.SaveChanges();
                return unit.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public bool Modify(UnitView unitView)
        {
            try
            {
                Unit unit = db.Unit.Find(unitView.id);
                unit.Name = unitView.Name;
                unit.Nameconvert = unitView.NameConvert;
                unit.Quantity = unitView.Quantity;
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
                Unit unit = db.Unit.Find(id);
                unit.Status = !unit.Status;
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public List<UnitView> GetDataRemoved(int page)
        {
            return db.Unit.AsNoTracking().Where(s => !s.Status).Select(s => new UnitView
            {
                id = s.Id,
                Name = s.Name,
                NameConvert = s.Nameconvert,
                Quantity = s.Quantity,
            }).ToList();
        }
    }
}
