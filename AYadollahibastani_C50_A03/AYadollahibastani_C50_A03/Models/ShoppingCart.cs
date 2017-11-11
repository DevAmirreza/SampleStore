using AYadollahibastani_C50_A03.Models.Validations;
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
        [ValidateProductName(ErrorMessage = "Your Product Name is Already taken , please choose another one ")]
        [Required]
        public String ProductName { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }

        public enum category { Other, Beverages, Bakery, Canned, Dairy, Dry, Frozen, Meat, Produce, Cleaners, Paper, Personal }
        [Display(Name = "Category")]
        public category ProductCategory { get; set; }

    }

    public class ShoppingCartList
    {
        // singleton pattern, not thread safe but good enough for this demo (single server solution, single threaded)
        // this is all just to fake out not having a database for now
        List<Product> cartList;
        private static ShoppingCartList instance = null;


        private ShoppingCartList()
        {
            cartList = null; // empty
        }

        public List<Product> GetList()
        {
            return cartList;
        }



        static protected void LoadShoppingCart()
        {
            XElement xelement = XElement.Load(System.Web.HttpContext.Current.Server.MapPath("/App_Data/ShoppingCartList.xml"));
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



        }


        protected void SaveShoppingCart()
        {

            XDocument xdoc = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("/App_Data/ShoppingCartList.xml"));
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
                //hererere
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

        public Boolean IsDuplicate(String productName)
        {
            var product = cartList.Where(s => s.ProductName.Trim().Equals(productName.Trim())).FirstOrDefault();

            if (product != null)
                return true;

            return false;

        }


        public static ShoppingCartList Instance // my singleton of a student list
        {
            //get
            //{

            //    if (instance == null)
            //    {
            //        instance = new ShoppingCartList();
            //        // populate with dummy data the first time this is referenced




            //        //LoadShoppingCart();


            //        try
            //        {
            //            XElement xelement = XElement.Load(System.Web.HttpContext.Current.Server.MapPath("/App_Data/ShoppingCartList.xml"));
            //            IEnumerable<XElement> products = xelement.Elements();

            //            instance.cartList = new List<Product>();
            //            // Read the entire XML
            //            foreach (var item in products)
            //            {

            //                instance.cartList.Add(new Product()
            //                {
            //                    Id = Convert.ToInt16(item.Element("Id").Value),
            //                    ProductName = item.Element("ProductName").Value,
            //                    Price = Convert.ToDouble(item.Element("Price").Value),
            //                    Quantity = Convert.ToInt16(item.Element("Quantity").Value)

            //                    //language = (Models.Book.lan)Enum.Parse(typeof(Models.Book.lan), book.Element("title").Attribute("lang").Value, true)


            //                });
            //            }

            //        }
            //        catch (Exception exp)
            //        {
            //            Exception ex = exp;

            //            XElement root = new XElement("ShoppingCart", "");
            //            root.Save(System.Web.HttpContext.Current.Server.MapPath("/App_Data/ShoppingCartList.xml"));

            //            //file not found
            //        }




            //        //instance.cartList = new List<Product>{
            //        //        new Product() { Id = 1, ProductName = "XBOX" , Quantity = 2 , Price = 22 ,ProductCategory = Product.category.Beverages  } ,
            //        //         new Product() { Id = 2, ProductName = "XBOX" , Quantity = 1 , Price = 22 ,ProductCategory = Product.category.Beverages  } ,
            //        //         new Product() { Id = 3, ProductName = "XBOX" , Quantity = 1 , Price = 22 ,ProductCategory = Product.category.Beverages  }
            //        //    };
            //        // Get the students from the database in the real application 

            //    } // end if first time

            //    return instance;
            //}

            get
            {

                if (instance == null)
                {
                    instance = new ShoppingCartList();
                    // populate with dummy data the first time this is referenced

                    LoadShoppingCart();

                } // end if first time

                return instance;
            }


        }


        //public void updateList(Book book)
        //{
        //    int bookIndex = myList_mv.FindIndex(c => c.bookId == book.bookId);
        //    myList_mv[bookIndex] = book;
        //    //SetList(Instance.myList_mv);

        //    XDocument xdoc = XDocument.Load(FILEPATH);
        //    xdoc.Root.RemoveAll();
        //    xdoc.Save(FILEPATH);


        //    foreach (var item in myList_mv)
        //    {
        //        addBook(item);
        //    }


        //}

        //public void deleteBook(Book book)
        //{

        //    myList_mv.Remove(book);
        //    XDocument xdoc = XDocument.Load(FILEPATH);
        //    xdoc.Root.RemoveAll();
        //    xdoc.Save(FILEPATH);


        //    foreach (var item in myList_mv)
        //    {
        //        addBook(item);
        //    }

        //}

        //public void addToBookList(Book book)
        //{
        //    myList_mv.Add(book);
        //}


        //public void addBook(Book book)
        //{

        //    XDocument xdoc = XDocument.Load(FILEPATH);
        //    XElement rootElement = xdoc.Root;
        //    rootElement.Add(new XComment("Amirreza Yadollahi - Lab 07 b&c - November first "));
        //    XElement bookElenent = new XElement("book", new XElement("id", book.bookId));
        //    bookElenent.Add(new XAttribute("category", book.category));
        //    rootElement.Add(bookElenent);
        //    XElement titleElement = new XElement("title", book.title);
        //    titleElement.Add(new XAttribute("lang", book.language));
        //    bookElenent.Add(titleElement);
        //    bookElenent.Add(new XElement("author", book.author));
        //    bookElenent.Add(new XElement("year", book.year));
        //    bookElenent.Add(new XElement("price", book.price));

        //    xdoc.Save(FILEPATH);
        //}


    } // end StudentList


}