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
    public class InvoiceDTDto
    {
        private DBContext db;
        public int size = ConstantCuaSang.size;
        public string search = ConstantCuaSang.search;
        //int start = size * (page - 1);

        public InvoiceDTDto()
        {
            db = new DBContext();
        }

        public List<InvoiceDTView> GetData(int page, int invoiceId)
        {
            int start = size * (page - 1);
            return db.InvoiceDetail.AsNoTracking().Where(s => s.Invoiceid == invoiceId).Skip(start).Take(size).Select(s => new InvoiceDTView
            {
                InvoiceId = s.Invoiceid,
                Price = s.Price,
                ProId = s.Proid,
                ProName = s.Pro.Name,
                Quantity = s.Quantity
            }).ToList();
        }

        public bool CreateInvoiceDT(List<InvoiceDTView> invoiceDTViews, int invoiceId)
        {
            try
            {
                List<InvoiceDetail> list = new List<InvoiceDetail>();
                invoiceDTViews.ForEach(s =>
                {
                    list.Add(new InvoiceDetail
                    {
                        Invoiceid = invoiceId,
                        Price = s.Price,
                        Proid = s.ProId,
                        Quantity = s.Quantity
                    });
                });
                db.AddRange(list);
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
