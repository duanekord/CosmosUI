using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using cosmosui.Data;
using cosmosui.Models;

namespace cosmosui.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IEnumerable<FieldMasterInfo> items;
        private Dictionary<string, IEnumerable<object>> _dropdowns;
        private PropertyInfo[] properties;

        public async Task<ActionResult> Index()
        {
            items = await DocumentDBRepository<FieldMasterInfo>.GetAll(d => d.TenantId != null);

            //populate dropdowns
            return View(SetUp(items));
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
                            var expr = GenerateFieldExpression<FieldMasterInfo>(property.Name, value);

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


        //Populates dropdowns based on DropDown attribute above class property
        public FieldMasterInfo SetUp(IEnumerable<FieldMasterInfo> allItems)
        {
            FieldMasterInfo fmi = new FieldMasterInfo();
            this.properties = fmi.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if(Attribute.IsDefined(property, typeof(DropdownAttribute)))
                {
                    fmi.populate_dd.Add(property.Name, allItems.Select(m => m.GetType().GetProperty(property.Name).GetValue(m, null)).Distinct());
                }
            }
            return fmi;
        }

        public Expression<Func<T, bool>> GenerateFieldExpression<T>(string fieldName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "m");
            // m
            var fieldAccess = Expression.PropertyOrField(parameter, fieldName);
            // m.[fieldName]
            var nullValue = Expression.Constant(value);
            // null
            var body = Expression.Equal(fieldAccess, nullValue);
            // m.[fieldName] == value
            var expr = Expression.Lambda<Func<T, bool>>(body, parameter);
            // m => m.[fieldName] == value
            return expr;
        }
    }
}