using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;


namespace WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly string apiUrl = "https://localhost:44336/api/Employee/GetAllEmployee";
        private readonly string adding = "https://localhost:44336/api/Employee/AddEmployee";
        private readonly string updating = "https://localhost:44336/api/Employee/UpdateEmployee";

        public async Task<ActionResult> Index()
        {
            List<UserViewModel> employees = new List<UserViewModel>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(data);
                    employees = JsonConvert.DeserializeObject<List<UserViewModel>>(result.listEmployees.ToString());
                }
            }

            return View(employees);

        }

        

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel user)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(adding, content);
                if (response.IsSuccessStatusCode)
                {
                    // Employee added successfully, you can redirect or display a success message.
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error, display error message, etc.
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the employee.");
                }
            }

            return View(user);
        }

        public ActionResult Update()
        {
            return View("Update");
        }


        [HttpPost]
        public async Task<ActionResult> Update(UserViewModel user)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(updating, content);
                if (response.IsSuccessStatusCode)
                {
                    // Successfully updated, you can redirect to another page or take other actions
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error case, show error message or take necessary actions
                    ViewBag.ErrorMessage = "Error updating user.";
                    return View("Error"); // Create an "Error" view to display the error message
                }
            }
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                // Create an instance of HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Set the base address of your Web API
                    client.BaseAddress = new Uri("https://localhost:44336/");

                    // Send a DELETE request to your Web API endpoint
                    HttpResponseMessage response = client.DeleteAsync($"api/Employee/DeleteEmployee/{id}").Result;

                    // Check if the request was successful (HTTP status code 204 for No Content)
                    if (response.IsSuccessStatusCode)
                    {
                        
                    }
                    else
                    {
                        
                        return View("Error");
                    }
                }

                // After successful deletion or handling any errors, redirect to the Index action.
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the deletion process.
                // You can log the error, return an error view, or take other appropriate actions.
                return View("Error");
            }
        }

    }
}