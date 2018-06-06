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
        private static IEnumerable<FieldMasterInfo> items;

        // GET: Bank
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReadItems([DataSourceRequest] DataSourceRequest request, string tenant)
        {
            items = DocumentDBRepository<FieldMasterInfo>.GetAll(GeneratePredicate.GenerateEqualFieldExpression<FieldMasterInfo>("TenantId", tenant), tenant);
            return Json(items.ToDataSourceResult(request));
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

        public ActionResult FilterMenuCustomization_FieldId()
        {
           var e =  items.GroupBy(g => g.FieldId)
                                          .Select(t => t.First())
                                          .Select(m =>
                                               new SelectListItem()
                                               {
                                                   Text = m.FieldId,
                                                   Value = m.FieldId

                                               });


            e.Distinct();
            return Json(e, JsonRequestBehavior.AllowGet);
        }
    }
}