using Project.Models.Dto;
using Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Business
{
    public class InvoiceDTBus
    {
        public List<InvoiceDTView> GetData(int page, int invoiceId) => new InvoiceDTDto().GetData(page, invoiceId);

        public bool CreateInvoiceDT(List<InvoiceDTView> invoiceDTViews, int invoiceId) => new InvoiceDTDto().CreateInvoiceDT(invoiceDTViews, invoiceId);
    }
}
