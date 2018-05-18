using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using cosmosui.Data;
using cosmosui.Models;

namespace cosmosui.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["tenants"] = GetTenants();



            return View();
        }

        [Authorize(Roles = "Observer")]
        public ActionResult Observer()
        {
            ViewBag.Message = "You can see this because you are an observer.";

            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "You can see this because you are an admin.";

            return View();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Index(FieldMasterInfo fminfo)
        {
            //Set up repository and pre-defined tenants
            ViewData["tenants"] = GetTenants();
            DocumentDBRepository<FieldMasterInfo>.Initialize();
            IEnumerable<FieldMasterInfo> items;

            if (string.IsNullOrEmpty(fminfo.InputType))
            {
                items = await DocumentDBRepository<FieldMasterInfo>.GetItemsAsync(d => d.FieldId.Equals(fminfo.FieldId), fminfo.TenantId);
                return View();
            }
            if (!string.IsNullOrEmpty(fminfo.InputType))
            {
                items = await DocumentDBRepository<FieldMasterInfo>.GetItemsAsyncMultiParams(d => d.FieldId.Equals(fminfo.FieldId), d => d.InputType.Equals(fminfo.InputType), fminfo.TenantId);
                return View();
            }

            return View();
        }

        public static SelectList GetTenants()
        {

            var tenants = new List<SelectListItem>();

            tenants.Add(new SelectListItem { Text = "CI", Value = "CI" });
            tenants.Add(new SelectListItem { Text = "AP", Value = "AP" });

            return new SelectList(tenants, "Value", "Text");
        }
    }
}