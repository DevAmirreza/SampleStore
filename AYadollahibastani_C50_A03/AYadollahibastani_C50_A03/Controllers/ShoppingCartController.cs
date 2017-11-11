using AYadollahibastani_C50_A03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AYadollahibastani_C50_A03.Models.Product;

namespace AYadollahibastani_C50_A03.Controllers
{
    public class ShoppingCartController : Controller
    {
        #region Page ActionResults 

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
            String productName = collection.GetValue("ProductName").AttemptedValue;
            if (!Models.ShoppingCartList.Instance.IsDuplicate(productName))
            {

                try
                {
                    //Intialize Model 
                    Models.Product product = new Models.Product();
                    product.Id = Models.ShoppingCartList.Instance.GetList().Count + 2;
                    product.Price = Convert.ToDouble(collection.GetValue("Price").AttemptedValue);
                    product.Quantity = Convert.ToInt16(collection.GetValue("Quantity").AttemptedValue);
                    product.ProductName = productName;
                    product.ProductCategory = (category)Enum.Parse(typeof(category), collection.GetValue("ProductCategory").AttemptedValue);

                    //add product to shopping cart 
                    Models.ShoppingCartList.Instance.AddProduct(product);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Exception exp = ex;
                    return View();
                }
            }
            else
                return View("_Error");
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
                //Intialize Business Object
                var product = new Product();
                product.Id = id;
                product.Price = Convert.ToDouble(collection.GetValue("Price").AttemptedValue);
                product.ProductName = collection.GetValue("ProductName").AttemptedValue;
                product.Quantity = Convert.ToInt16(collection.GetValue("Quantity").AttemptedValue);
                product.ProductCategory = (category)Enum.Parse(typeof(category), collection.GetValue("ProductCategory").AttemptedValue);

                //Update product 
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

            //Partial View
            return PartialView("_Delete", product);
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
        #endregion

        #region Custom ActionResults

        [HttpPost]
        public ActionResult IsDuplicate(String name)
        {

            if (Models.ShoppingCartList.Instance.IsDuplicate(name))
                return Json(true);

            return Json(false);

        }//Validation Called on Ajax

        public ActionResult CreateButton()
        {
            return PartialView("CreateProduct");
        }//return partial View 

        [HttpPost]
        public ActionResult ProfileJson()
        {
            //return list of products 
            return Json(Models.ShoppingCartList.Instance.GetList());
        }//called on Ajax

        #endregion

    }//end of controller 
}
