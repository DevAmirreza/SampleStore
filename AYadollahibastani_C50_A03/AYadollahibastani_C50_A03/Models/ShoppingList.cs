using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;
using static AYadollahibastani_C50_A03.Models.Product;

namespace AYadollahibastani_C50_A03.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        public String ProductName { get; set; }
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public enum category { Other, Beverages, Bakery, Canned, Dairy, Dry, Frozen, Meat, Produce, Cleaners, Paper, Personal }
        [Display(Name = "Category")]
        public category ProductCategory { get; set; }

        public const String FILEPATH = "/App_Data/ShoppingCartList.xml";

    }

    public class ShoppingCartList
    {
        #region Declaration
        List<Product> cartList;
        private static ShoppingCartList instance = null;
        #endregion

        #region Methods 
        private ShoppingCartList()
        {
            cartList = null; // empty
        }
        public List<Product> GetList()
        {
            return cartList;
        }
        #endregion

        #region XML File Processing
        static protected void LoadShoppingCart()
        {
            XElement xelement = XElement.Load(System.Web.HttpContext.Current.Server.MapPath(FILEPATH));
            IEnumerable<XElement> products = xelement.Elements();

            instance.cartList = new List<Product>();
            if (IsValidXml())
            {
                // Read the entire XML
                foreach (var productItem in products)
                {
                    instance.cartList.Add(new Product()
                    {
                        Id = Convert.ToInt16(productItem.Element("id").Value),
                        ProductName = productItem.Element("Product").Element("Name").Value,
                        Price = Convert.ToDouble(productItem.Element("Product").Element("Price").Value),
                        Quantity = Convert.ToInt16(productItem.Element("Quantity").Value),
                        ProductCategory = (category)Enum.Parse(typeof(category), productItem.Attribute("category").Value)

                    });
                }
            }
            else
            {
                //error message goes here
                instance.cartList.Add(new Product());
            }

        }//End of LoadShopping 

        protected void SaveShoppingCart()
        {

            XDocument xdoc = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath(FILEPATH));
            XElement rootElement = xdoc.Root;
            rootElement.RemoveAll();

            foreach (var item in cartList)
            {
                XElement ShoppingEntry = new XElement("ShoppingEntry", new XElement("id", item.Id));
                ShoppingEntry.Add(new XAttribute("category", item.ProductCategory));

                XElement productElement = new XElement("Product");
                XElement nameElement = new XElement("Name", item.ProductName);
                XElement priceElement = new XElement("Price", item.Price);

                productElement.Add(nameElement);
                productElement.Add(priceElement);

                ShoppingEntry.Add(productElement);
                ShoppingEntry.Add(new XElement("Quantity", item.Quantity));
                rootElement.Add(ShoppingEntry);
            }

            if (IsValidXml())
            {
                xdoc.Save(System.Web.HttpContext.Current.Server.MapPath("/App_Data/ShoppingCartList.xml"));
            }
            else
            {
                //xml not valid
            }
        }

        public static bool IsValidXml()
        {
            var xdoc = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("/App_Data/ShoppingCartList.xml"));
            var schemas = new XmlSchemaSet();
            schemas.Add(null, System.Web.HttpContext.Current.Server.MapPath("/Schema/ShoppingList.xsd"));

            try
            {
                xdoc.Validate(schemas, null);
            }
            catch (XmlSchemaValidationException)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region CRUD Operations
        public void AddProduct(Product product)
        {
            cartList.Add(product);
            SaveShoppingCart();
        }

        public void RemoveProduct(int productId)
        {
            var product = cartList.Where(s => s.Id == productId).FirstOrDefault();

            cartList.Remove(product);
            SaveShoppingCart();
        }

        public void UpdateProduct(Product product)
        {
            int productIndex = cartList.FindIndex(c => c.Id == product.Id);
            cartList[productIndex] = product;

            SaveShoppingCart();
        }

        #endregion

        #region Validation
        public Boolean IsDuplicate(String productName)
        {
            var product = cartList.Where(s => s.ProductName.Trim().Equals(productName.Trim())).FirstOrDefault();

            if (product != null)
                return true;

            return false;

        }

        #endregion


        //Creat and instance of Shopping Cart List 
        public static ShoppingCartList Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCartList();
                    //Load products information at the start up 
                    LoadShoppingCart();

                } // end if first time

                return instance;
            }
        }

    } // end of ShoppingList
}