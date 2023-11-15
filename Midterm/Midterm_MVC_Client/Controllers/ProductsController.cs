using Midterm_MVC_Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Midterm_MVC_Client.Controllers
{
    public class ProductsController : Controller
    {
        static HttpClient client = new HttpClient();
        static string BaseURL = ConfigurationManager.AppSettings["ShopService"] + "/products";

        // GET: Products
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{BaseURL}").Result;
                var ct = new List<ProductDTO>();
                if (response.IsSuccessStatusCode)
                {
                    var Result = JsonConvert.DeserializeObject<ProductsResponse>(response.Content.ReadAsStringAsync().Result);
                    if(Result.IsSuccess && Result.Products?.Count > 0)
                    {
                        ct = Result.Products;
                    }
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpResponseMessage response = client.GetAsync($"{BaseURL}/{id}").Result;
            ProductDTO ct = new ProductDTO();
            if (response.IsSuccessStatusCode)
            {
                var Result = JsonConvert.DeserializeObject<ProductResponse>(response.Content.ReadAsStringAsync().Result);
                if (Result.IsSuccess && Result.Product != null)
                {
                    ct = Result.Product;
                }
            }
            return View(ct);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, ProductDTO Product)
        {
            ViewBag.ErrorMessage = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "მონაცემები არავალიდურია!";
                    return View();
                }
                else
                {
                    string output = JsonConvert.SerializeObject(Product);
                    var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync($"{BaseURL}/add", stringContent).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "დამატების დროს მოხდა შეცდომა. ბოდიშს გიხდით";
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpResponseMessage response = client.GetAsync($"{BaseURL}/{id}").Result;

            ProductDTO ct = new ProductDTO();

            if (response.IsSuccessStatusCode)
            {
                var Result = JsonConvert.DeserializeObject<ProductResponse>(response.Content.ReadAsStringAsync().Result);
                if(Result.IsSuccess && Result.Product != null)
                {
                    ct = Result.Product;
                }
                return View(ct);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection, ProductDTO Product)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("მონაცემები არავალიდურია!");

                string output = JsonConvert.SerializeObject(Product);
                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(BaseURL + "/update", stringContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            if (id.Equals(""))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpResponseMessage response = client.GetAsync($"{BaseURL}/{id}").Result;
            ProductDTO ct = new ProductDTO();
            if (response.IsSuccessStatusCode)
            {
                var Result = JsonConvert.DeserializeObject<ProductResponse>(response.Content.ReadAsStringAsync().Result);
                if (Result.IsSuccess && Result.Product != null)
                {
                    ct = Result.Product;
                }
            }
            return View(ct);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"{BaseURL}/delete/{id}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
