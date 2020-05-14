using Project.Models.Dto;
using Project.Models.ViewModels;
using System.Collections.Generic;

namespace Project.Models.Business
{
    public class CategoryBus
    {
        public List<CategoryView> GetData(int page) => new CategoryDto().GetData(page);

        public CategoryView GetById(int id) => new CategoryDto().GetById(id);

        public List<CategoryView> SearchByName(string textsearch) => new CategoryDto().SearchByName(textsearch);

        public int Create(CategoryView categoryView) => new CategoryDto().Create(categoryView);

        public bool Modify(CategoryView categoryView) => new CategoryDto().Modify(categoryView);

        public bool Remove(int id) => new CategoryDto().SetStatus(id);
    }
}
