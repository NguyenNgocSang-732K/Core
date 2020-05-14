using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;
using System.Linq;

namespace Project.Models.Dto
{
    public class PageDto
    {
        public int GetRowUserTable()
        {
            DBContext db = new DBContext();
            return db.User.AsNoTracking().Count(s => (bool)s.Status);
        }
    }
}
