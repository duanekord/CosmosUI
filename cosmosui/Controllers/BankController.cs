using cosmosui.Data;
using cosmosui.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
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
            //SetUp(items);
            return View(items);
        }

        //POST
        [HttpPost]
        public async Task<ActionResult> Index(FieldMasterInfo fminfo)
        {
            //Set up
            IEnumerable<FieldMasterInfo> items;
            Type myType = fminfo.GetType();
            properties = fminfo.GetType().GetProperties();
            var predicate = PredicateBuilder.True<FieldMasterInfo>();

            foreach (var i in fminfo.store_dd)
            {
                if (i.Value != null && i.Value != "")
                {
                    foreach (PropertyInfo property in properties)
                    {
                        //var localI = i;
                        //FieldMasterInfo localFMI = fminfo;

                        if (i.Key == property.Name)
                        {
                            var value = fminfo.store_dd[i.Key];
                            var expr = GeneratePredicate.GenerateFieldExpression<FieldMasterInfo>(property.Name, value);

                            myType.GetProperty(property.Name).SetValue(fminfo, value);
                            predicate = predicate.And(expr);
                            break;
                        }
                    }
                }
            }

            items = await DocumentDBRepository<FieldMasterInfo>.GetItemsAsync(predicate, fminfo.TenantId);

            return View("~/Views/Home/ResultsView.cshtml", items);
        }

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


        //public void SetUp(IEnumerable<FieldMasterInfo> allItems)
        //{
        //    FieldMasterInfo fmi = new FieldMasterInfo();
        //    this.properties = fmi.GetType().GetProperties();

        //    foreach (PropertyInfo property in properties)
        //    {
        //        if (Attribute.IsDefined(property, typeof(DropdownAttribute)))
        //        {
        //            fmi.populate_dd.Add(property.Name, allItems.Select(m => m.GetType().GetProperty(property.Name).GetValue(m, null)).Distinct());
        //        }
        //    }
        //    return fmi;
        //}


    }
}