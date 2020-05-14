using Project.Models.Dto;
using Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Business
{
    public class InvoiceBus
    {
        public List<InvoiceView> GetData(int page) => new InvoiceDto().GetData(page);

        public int GetRowsAll() => new InvoiceDto().GetRowsAll();

        public InvoiceView GetById(int id) => new InvoiceDto().GetById(id);

        public int Create(InvoiceView invoiceView) => new InvoiceDto().Create(invoiceView);

        public bool SetStatus(int id) => new InvoiceDto().SetStatus(id);

        public List<InvoiceView> SearchByDate(int page, DateTime dateStart, DateTime dateEnd) => new InvoiceDto().SearchByDate(page, dateStart, dateEnd);

        public int GetRowCountSearchByDate(DateTime dateStart, DateTime dateEnd) => new InvoiceDto().GetRowCountSearchByDate(dateStart, dateEnd);

        public List<InvoiceView> SearchByEmpName(int page, string textsearch) => new InvoiceDto().SearchByEmpName(page, textsearch);

        public int GetRowCountSearchByEmpName(string textsearch) => new InvoiceDto().GetRowCountSearchByEmpName(textsearch);

        public List<InvoiceView> SearchByCusName(int page, string textsearch) => new InvoiceDto().SearchByCusName(page, textsearch);

        public int GetRowCountSearchByCusName(string textsearch) => new InvoiceDto().GetRowCountSearchByCusName(textsearch);
    }
}
