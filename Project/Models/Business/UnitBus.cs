using Project.Models.Dto;
using Project.Models.Entities;
using Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Business
{
    public class UnitBus
    {
        public List<UnitView> GetData() => new UnitDto().GetData();

        public UnitView GetById(int id) => new UnitDto().GetById(id);

        public int Create(UnitView unitView) => new UnitDto().Create(unitView);

        public bool Modify(UnitView unitView) => new UnitDto().Modify(unitView);

        public bool SetStatus(int id) => new UnitDto().SetStatus(id);

        public List<UnitView> GetDataRemoved(int page) => new UnitDto().GetDataRemoved(page);
    }
}
