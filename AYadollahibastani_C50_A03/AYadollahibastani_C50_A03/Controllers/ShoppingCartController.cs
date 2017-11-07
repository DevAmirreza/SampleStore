using AYadollahibastani_C50_A03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AYadollahibastani_C50_A03.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View(Models.ShoppingCartList.Instance.GetList());
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details(int id)
        {
            var product = Models.ShoppingCartList.Instance.GetList().Where(s => s.Id == id).FirstOrDefault();

            return View(product);
        }

        // GET: ShoppingCart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.Product product = new Models.Product();
                product.Id = Models.ShoppingCartList.Instance.GetList().Count + 2 ;
                product.Price = Convert.ToDouble(collection.GetValue("Price").AttemptedValue); 
                product.Quantity = Convert.ToInt16(collection.GetValue("Quantity").AttemptedValue);
                product.ProductName = collection.GetValue("ProductName").AttemptedValue;

                Models.ShoppingCartList.Instance.AddProduct(product);


                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Exception exp = ex; 
                return View();
            }
        }

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int id)
        {
            var product = Models.ShoppingCartList.Instance.GetList().Where(s => s.Id == id).FirstOrDefault();

            return View(product);
        }

        // POST: ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var product = new Product();
                product.Id = id; 
                product.Price= Convert.ToDouble(collection.GetValue("Price").AttemptedValue);
                product.ProductName  = collection.GetValue("ProductName").AttemptedValue;
                product.Quantity = Convert.ToInt16(collection.GetValue("Quantity").AttemptedValue);
             

                Models.ShoppingCartList.Instance.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Exception exc = e;
                return View();
            }
        }

        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(int id)
        {
            var product = Models.ShoppingCartList.Instance.GetList().Where(s => s.Id == id).FirstOrDefault();

            //return View(product);
            return PartialView("_Delete");    
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Models.ShoppingCartList.Instance.RemoveProduct(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        #region Custom ActionResults

        [HttpPost]
        public ActionResult IsDuplicate(String name)
        {
           
                if(Models.ShoppingCartList.Instance.IsDuplicate(name)) 
                    return Json(true, JsonRequestBehavior.AllowGet);

                return Json(false, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult CreateButton() {

            return PartialView("CreateProduct");
        }

        [HttpPost]
        public ActionResult ProfileJson() {
            return Json(Models.ShoppingCartList.Instance.GetList()); 
        }


        #endregion



    }
}
