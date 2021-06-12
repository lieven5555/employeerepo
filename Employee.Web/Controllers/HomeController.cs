
using Employee.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Employee.Web.Controllers
{
    public class HomeController : Controller
    {
        string baseUrl = "https://localhost:44319/api/employees/";
        public ActionResult Index()
        {
            // api call to get list of employess
            List<EmployeeDetails> employeesList = new List<EmployeeDetails>();
            string endpoint = "getAllEmployee";
            //since certificates used are not from trusted authority I am using ServicePointManager to bypass certificate.
            ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            List<EmployeeDetails> empList = new List<EmployeeDetails>();
            //Api call 
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, endpoint))
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var results = response.Result.Content.ReadAsStringAsync();
                        employeesList = JsonConvert.DeserializeObject<List<EmployeeDetails>>(results.Result);
                    }
                }
            }
            return View(employeesList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDetails employeeDetails)
        {
            if (ModelState.IsValid)
            {
                string endpoint = "CreateEmployee";
                //since certificates used are not from trusted authority I am using ServicePointManager to bypass certificate.
                ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                // API call http post
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, endpoint))
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var strObj = JsonConvert.SerializeObject(employeeDetails);
                    var content = new StringContent(strObj, Encoding.UTF8, "application/json");
                    using (var response = client.PostAsync(endpoint, content))
                    {
                        if (response.Result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return View(employeeDetails);
        }
    }
}