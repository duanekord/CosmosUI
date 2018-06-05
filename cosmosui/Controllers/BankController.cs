using cosmosui.Data;
using cosmosui.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cosmosui.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private IEnumerable<FieldMasterInfo> items;

        // GET: Bank
        public async Task<ActionResult> Index()
        {
            items = await DocumentDBRepository<FieldMasterInfo>.GetAll(d => d.TenantId != null);
            SetUp(items);
            return View(items);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> EditingCustom_Update([DataSourceRequest] DataSourceRequest request, 
                FieldMasterInfo product)
        {
            
            if (product != null && ModelState.IsValid)
            {
                await DocumentDBRepository<FieldMasterInfo>.UpdateSingleDoc(product);
            }
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));

        }

        public ActionResult FilterMenuCustomization_Tenant()
        {
            List<string> tenantDistinct = new List<string>(){ "AP", "CI"};
            
            tenantDistinct = tenantDistinct.Distinct().ToList();
            return Json(tenantDistinct, JsonRequestBehavior.AllowGet);
        }


        public void SetUp(IEnumerable<FieldMasterInfo> allItems)
        {
            ViewBag.FieldIdNoFilter = allItems.GroupBy(g => g.FieldId)
                                              .Select(t => t.First())
                                              .Select(m =>
                                                   new SelectListItem()
                                                   {
                                                       Text = m.FieldId,
                                                       Value = m.FieldId

                                                   });
        }
    }
}