using Project.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Business
{
    public class PageBus
    {
        public static int GetRowUser()
        {
            return new PageDto().GetRowUserTable();
        }
    }
}
