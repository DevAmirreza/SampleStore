using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AYadollahibastani_C50_A03.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Product Name")]
        public String ProductName { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }

        public enum category { Beverages, Bread }
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



        protected void LoadShoppingCart() {


        }


        protected void SaveShoppingCart()
        {


        }


        public void AddProduct(Product product) {
            cartList.Add(product);
        }

        public void RemoveProduct(int productId) {
            var product = cartList.Where(s => s.Id == productId).FirstOrDefault();

            cartList.Remove(product);
        }

        public void UpdateProduct(Product product) {
            int productIndex = cartList.FindIndex(c => c.Id== product.Id);
            cartList[productIndex] = product; 
        }

        public static ShoppingCartList Instance // my singleton of a student list
        {
            get
            {

                if (instance == null)
                {
                    instance = new ShoppingCartList();
                    // populate with dummy data the first time this is referenced
                    XElement xelement = null;

                    try
                    {
                         xelement = XElement.Load(System.Web.HttpContext.Current.Server.MapPath("/App_Data/ShoppingCartList.xml"));
                    }
                    catch {

                       //file not found
                    }


                    try
                    {
                        IEnumerable<XElement> CartList = xelement.Elements();

                        instance.cartList = new List<Product>();
                        // Read the entire XML
                        foreach (var Product in CartList)
                        {
                            instance.cartList.Add(new Product()
                            {
                                //Id = 1
                                //author = book.Element("author").Value,
                                //title = book.Element("title").Value,
                                //price = Convert.ToDecimal(book.Element("price").Value),
                                //year = Convert.ToInt16(book.Element("year").Value),
                                //category = book.Attribute("category").Value,
                                //language = (Models.Book.lan)Enum.Parse(typeof(Models.Book.lan), book.Element("title").Attribute("lang").Value, true)


                            });
                        }
                    }
                    catch {
                        //Null Exception
                    }

         
                    
                      
                
                

                    instance.cartList = new List<Product>{
                            new Product() { Id = 1, ProductName = "XBOX" , Quantity = 2 , Price = 22 ,ProductCategory = Product.category.Beverages  } ,
                             new Product() { Id = 2, ProductName = "XBOX" , Quantity = 1 , Price = 22 ,ProductCategory = Product.category.Beverages  } ,
                             new Product() { Id = 3, ProductName = "XBOX" , Quantity = 1 , Price = 22 ,ProductCategory = Product.category.Beverages  }
                        };
                    // Get the students from the database in the real application 

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