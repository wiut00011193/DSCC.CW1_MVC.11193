using DSCC.CW1_MVC._11193.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DSCC.CW1_MVC._11193.Controllers
{
    public class DepartmentController : Controller
    {
        // The Definition of Base URL
        public const string baseUrl = "http://localhost:5116/";
        Uri ClientBaseAddress = new Uri(baseUrl);
        HttpClient clnt;

        // Constructor for initiating request to the given base URL publicly
        public DepartmentController()
        {
            clnt = new HttpClient();
            clnt.BaseAddress = ClientBaseAddress;
        }

        public void HeaderClearing()
        {
            // Clearing default headers
            clnt.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            clnt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Department
        public async Task<ActionResult> Index()
        {
            // Creating the list of new Departments list
            List<Department> DepartmentInfo = new List<Department>();

            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Department");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                DepartmentInfo = JsonConvert.DeserializeObject<List<Department>>(responseMessage);
            }

            return View(DepartmentInfo);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            // Creating a Get Request to get single Department
            Department DepartmentDetails = new Department();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results
            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Department/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                DepartmentDetails = JsonConvert.DeserializeObject<Department>(detailsInfo);
            }

            return View(DepartmentDetails);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                // serializing department object into json format to send
                /*string jsonObject = "{"+department."}"*/
                string createDepartmentInfo = JsonConvert.SerializeObject(department);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createDepartmentInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Department/", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            // Creating a Get Request to get single Department
            Department DepartmentDetails = new Department();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results
            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Department/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                DepartmentDetails = JsonConvert.DeserializeObject<Department>(detailsInfo);
            }

            return View(DepartmentDetails);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                // serializing department object into json format to send
                /*string jsonObject = "{"+department."}"*/
                string createDepartmentInfo = JsonConvert.SerializeObject(department);

                // creating string content to pass as Http content later
                StringContent stringContentInfo = new StringContent(createDepartmentInfo, Encoding.UTF8, "application/json");

                // Making a Post request
                HttpResponseMessage createHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + "api/Department/" + department.ID, stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            // Creating a Get Request to get single Department
            Department DepartmentDetails = new Department();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results
            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Department/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                DepartmentDetails = JsonConvert.DeserializeObject<Department>(detailsInfo);
            }

            return View(DepartmentDetails);
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Department department)
        {

            // serializing department object into json format to send
            /*string jsonObject = "{"+department."}"*/
            string createDepartmentInfo = JsonConvert.SerializeObject(department);

            // creating string content to pass as Http content later
            StringContent stringContentInfo = new StringContent(createDepartmentInfo, Encoding.UTF8, "application/json");

            // Making a Post request
            HttpResponseMessage createHttpResponseMessage = clnt.DeleteAsync(clnt.BaseAddress + "api/Department/" + department.ID).Result;
            if (createHttpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }
    }
}
