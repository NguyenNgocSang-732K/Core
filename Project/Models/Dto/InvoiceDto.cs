using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;
using Project.Models.ViewModels;
using Supports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace Project.Models.Dto
{
    public class InvoiceDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public InvoiceDto()
        {
            db = new DBContext();
        }

        public List<InvoiceView> GetData(int page)
        {
            int start = size * (page - 1);
            return db.Invoice.AsNoTracking().OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new InvoiceView
            {
                Id = s.Id,
                Code = s.Code,
                CusId = s.Cusid,
                CusName = s.Cus.Name,
                DayCreate = s.Daycreate,
                EmpId = s.Empid,
                EmpName = s.Emp.Name,
                Status = s.Status,
                Total = s.Total
            }).ToList();
        }

        public int GetRowsAll()
        {
            return db.Invoice.AsNoTracking().Count();
        }

        public InvoiceView GetById(int id)
        {
            return db.Invoice.AsNoTracking().Where(s => s.Id == id).Select(s => new InvoiceView
            {
                Id = s.Id,
                Code = s.Code,
                CusId = s.Cusid,
                CusName = s.Cus.Name,
                DayCreate = s.Daycreate,
                EmpId = s.Empid,
                EmpName = s.Emp.Name,
                Status = s.Status,
                Total = s.Total
            }).SingleOrDefault();
        }

        public int Create(InvoiceView invoiceView)
        {
            try
            {
                Invoice invoice = new Invoice
                {
                    Id = invoiceView.Id,
                    Code = invoiceView.Code,
                    Cusid = invoiceView.CusId,
                    Empid = invoiceView.EmpId,
                    Daycreate = invoiceView.DayCreate,
                    Status = invoiceView.Status,
                    Total = invoiceView.Total
                };
                db.Invoice.Add(invoice);
                db.SaveChanges();
                return invoice.Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public bool SetStatus(int id)
        {
            try
            {
                Invoice invoice = db.Invoice.Find(id);
                invoice.Status = !invoice.Status;
                db.Invoice.Add(invoice);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public List<InvoiceView> SearchByDate(int page, DateTime dateStart, DateTime dateEnd)
        {
            int start = size * (page - 1);
            return db.Invoice.FromSqlRaw($"SELECT * FROM invoice WHERE invoice.daycreate BETWEEN { dateStart} AND { dateEnd}").AsNoTracking().OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new InvoiceView
            {
                Id = s.Id,
                Code = s.Code,
                CusId = s.Cusid,
                CusName = s.Cus.Name,
                DayCreate = s.Daycreate,
                EmpId = s.Empid,
                EmpName = s.Emp.Name,
                Status = s.Status,
                Total = s.Total
            }).ToList();
        }

        public int GetRowCountSearchByDate(DateTime dateStart, DateTime dateEnd)
        {
            return db.Invoice.FromSqlRaw($"SELECT * FROM invoice WHERE invoice.daycreate BETWEEN { dateStart} AND { dateEnd}").AsNoTracking().Count();
        }

        public List<InvoiceView> SearchByEmpName(int page, string textsearch)
        {
            int start = size * (page - 1);
            return db.Invoice.FromSqlRaw($"SELECT inv.* FROM invoice inv, [user] emp, [user] cus WHERE inv.empid = emp.id AND cus.id=inv.cusid AND emp.name {search} like '%{textsearch}%'").AsNoTracking().OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new InvoiceView
            {
                Id = s.Id,
                Code = s.Code,
                CusId = s.Cusid,
                CusName = s.Cus.Name,
                DayCreate = s.Daycreate,
                EmpId = s.Empid,
                EmpName = s.Emp.Name,
                Status = s.Status,
                Total = s.Total
            }).ToList();
        }

        public int GetRowCountSearchByEmpName(string textsearch)
        {
            return db.Invoice.FromSqlRaw($"SELECT inv.* FROM invoice inv, [user] emp, [user] cus WHERE inv.empid = emp.id AND cus.id=inv.cusid AND emp.name {search} like '%{textsearch}%'").AsNoTracking().Count();
        }

        public List<InvoiceView> SearchByCusName(int page, string textsearch)
        {
            int start = size * (page - 1);
            return db.Invoice.FromSqlRaw($"SELECT inv.* FROM invoice inv, [user] emp, [user] cus WHERE inv.empid = emp.id AND cus.id=inv.cusid AND cus.name {search} like '%{textsearch}%'").AsNoTracking().OrderByDescending(s => s.Id).Skip(start).Take(size).Select(s => new InvoiceView
            {
                Id = s.Id,
                Code = s.Code,
                CusId = s.Cusid,
                CusName = s.Cus.Name,
                DayCreate = s.Daycreate,
                EmpId = s.Empid,
                EmpName = s.Emp.Name,
                Status = s.Status,
                Total = s.Total
            }).ToList();
        }

        public int GetRowCountSearchByCusName(string textsearch)
        {
            return db.Invoice.FromSqlRaw($"SELECT inv.* FROM invoice inv, [user] emp, [user] cus WHERE inv.empid = emp.id AND cus.id=inv.cusid AND cus.name {search} like '%{textsearch}%'").AsNoTracking().Count();
        }
    }
}
