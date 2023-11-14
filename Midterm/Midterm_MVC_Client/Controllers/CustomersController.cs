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
    public class CustomersController : Controller
    {
        static HttpClient client = new HttpClient();
        string BaseURL = ConfigurationManager.AppSettings["ShopService"] + "/customers";

        // GET: Customers
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{BaseURL}").Result;
                var ct = new List<CustomerDTO>();
                if (response.IsSuccessStatusCode)
                {
                    var Result = JsonConvert.DeserializeObject<CustomersResponse>(response.Content.ReadAsStringAsync().Result);
                    if(Result.IsSuccess && Result.Customers?.Count > 0)
                    {
                        ct = Result.Customers;
                    }
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpResponseMessage response = client.GetAsync(BaseURL + "?id=" + id).Result;
            CustomerDTO ct = new CustomerDTO();
            if (response.IsSuccessStatusCode)
            {
                var Result = JsonConvert.DeserializeObject<CustomerResponse>(response.Content.ReadAsStringAsync().Result);
                if (Result.IsSuccess && Result.Customer != null)
                {
                    ct = Result.Customer;
                }
            }
            return View(ct);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, CustomerDTO customer)
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
                    string output = JsonConvert.SerializeObject(customer);
                    var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync(BaseURL, stringContent).Result;

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

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpResponseMessage response = client.GetAsync(BaseURL + "?id=" + id).Result;

            CustomerDTO ct;

            if (response.IsSuccessStatusCode)
            {
                ct = JsonConvert.DeserializeObject<CustomerDTO>(response.Content.ReadAsStringAsync().Result);
                return View(ct);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, FormCollection collection, CustomerDTO customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("მონაცემები არავალიდურია!");

                string output = JsonConvert.SerializeObject(customer);
                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(BaseURL + "?PN=" + id, stringContent).Result;

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

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            if (id.Equals(""))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpResponseMessage response = client.GetAsync(BaseURL + "?id=" + id).Result;
            CustomerDTO ct = new CustomerDTO();
            if (response.IsSuccessStatusCode)
            {
                ct = JsonConvert.DeserializeObject<CustomerDTO>(response.Content.ReadAsStringAsync().Result);
            }
            return View(ct);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(BaseURL + "?id=" + id).Result;

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
