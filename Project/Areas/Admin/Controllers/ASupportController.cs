using Microsoft.AspNetCore.Mvc;
using Supports;

namespace Project.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/support")]
    public class SupportController : Controller
    {
        [HttpGet("changeQuantityRows/{quantity}")]
        public IActionResult ChangeQuantityRows(int quantity)
        {
            ConstantCuaSang.size = (quantity < 1 || quantity > 20) ? ConstantCuaSang.size : quantity;
            return Json(quantity);
        }
    }
}