using Newtonsoft.Json;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Models.Home;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class HomeController : Controller
    {

        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public static string method;
        dbMyOnlineShoppingEntities ctx = new dbMyOnlineShoppingEntities();
        public ActionResult Index(string search,int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search,8, page));
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }

        public List<SelectListItem> GetCountry()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Country>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CountryId.ToString(), Text = item.CountryName });
            }
            return list;
        }
        public List<SelectListItem> GetGovernorates()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Governorate>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CountryId.ToString(), Text = item.GovernorateName });
            }
            return list;
        }
        public ActionResult ShippingDetails()
        {
            ViewBag.CountryList = GetCountry();
            ViewBag.GovernorateList = GetGovernorates();
            return View();
        }
        [HttpPost]
        public ActionResult ShippingDetails(Tbl_ShippingDetails tbl, ShippingDetail model)
        {
            tbl.MemberId = model.MemberId;
            tbl.Adress = model.Adress;
            tbl.City = model.City;
            tbl.State = model.State;
            tbl.Country = model.Country;
            tbl.ZipCode = model.ZipCode;
            _unitOfWork.GetRepositoryInstance<Tbl_ShippingDetails>().Add(tbl);

            return View(model);
        }


        public ActionResult CheckoutDetails()
        {
            ViewBag.name = method;
            return View();
        }
        //Not Working
        public string PaymentMethod(FormCollection form)
        {
            string selectedMethod = form["payment_method"].ToString();

            if (selectedMethod.Equals("1"))
                method = "cash";
            //return "cash";
            else
                method = "PaymentWithPapal";

                return method;
        }
        //=========================================================
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        public ActionResult AddToCart(int productId,string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(productId);
                for (int i = 0; i < count;i++ )
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect(url);
        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult OrderSummary()
        {
            return View();
        }
    }
}