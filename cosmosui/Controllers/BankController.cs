using cosmosui.Data;
using cosmosui.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cosmosui.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private PropertyInfo[] properties;
        private IEnumerable<FieldMasterInfo> items;

        // GET: Bank
        public async Task<ActionResult> Index()
        {
            items = await DocumentDBRepository<FieldMasterInfo>.GetAll(d => d.TenantId != null);
            SetUp(items);
            return View(items);
        }

        //POST
        //[HttpPost]
        //public async Task<ActionResult> Index(FieldMasterInfo fminfo)
        //{
        //    //Set up
        //    IEnumerable<FieldMasterInfo> items;
        //    Type myType = fminfo.GetType();
        //    properties = fminfo.GetType().GetProperties();
        //    var predicate = PredicateBuilder.True<FieldMasterInfo>();

        //    foreach (var i in fminfo.store_dd)
        //    {
        //        if (i.Value != null && i.Value != "")
        //        {
        //            foreach (PropertyInfo property in properties)
        //            {
        //                //var localI = i;
        //                //FieldMasterInfo localFMI = fminfo;

        //                if (i.Key == property.Name)
        //                {
        //                    var value = fminfo.store_dd[i.Key];
        //                    var expr = GeneratePredicate.GenerateFieldExpression<FieldMasterInfo>(property.Name, value);

        //                    myType.GetProperty(property.Name).SetValue(fminfo, value);
        //                    predicate = predicate.And(expr);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    items = await DocumentDBRepository<FieldMasterInfo>.GetItemsAsync(predicate, fminfo.TenantId);

        //    return View("~/Views/Home/ResultsView.cshtml", items);
        //}

        //public ActionResult FilterMenuCustomization_FieldId()
        //{
        //    return Json(this.items.Select(e => e.FieldId).Distinct(), JsonRequestBehavior.AllowGet);
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request, FieldMasterInfo product)
        {
            if (product != null && ModelState.IsValid)
            {
            }

            //return Json(products.ToDataSourceResult(request, ModelState));
            return Json(product);

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

            ViewBag.FieldIdAP = allItems.Where(w => w.FieldId == "AP")
                                              .GroupBy(g => g.FieldId)                                   
                                              .Select(t => t.First())
                                              .Select(m =>
                                                   new SelectListItem()
                                                   {
                                                       Text = m.FieldId,
                                                       Value = m.FieldId

                                                   });

            ViewBag.FieldIdCI = allItems.Where(w => w.FieldId == "CI")
                                              .GroupBy(g => g.FieldId)
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