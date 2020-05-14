using Project.Models.Dto;
using Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Business
{
    public class SubCateBus
    {
        public List<SubCateView> GetData(int cateId) => new SubCateroryDto().GetData(cateId);

        public List<SubCateView> SearchByName(string textsearch) => new SubCateroryDto().SearchByName(textsearch);

        public int Create(SubCateView subCateView) => new SubCateroryDto().Create(subCateView);

        public bool Modify(SubCateView subCateView) => new SubCateroryDto().Modify(subCateView);

        public bool SetStatus(int subId) => new SubCateroryDto().SetStatus(subId);

    }
}
