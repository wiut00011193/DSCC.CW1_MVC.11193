using DSCC.CW1_MVC._11193.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DSCC.CW1_MVC._11193.Controllers
{
    public class EmployeeController : Controller
    {
        // The Definition of Base URL
        public const string baseUrl = "http://localhost:5116/";
        Uri ClientBaseAddress = new Uri(baseUrl);
        HttpClient clnt;

        // Constructor for initiating request to the given base URL publicly
        public EmployeeController()
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

        // GET: Employee
        public async Task<ActionResult> Index()
        {
            // Creating the list of new Employees list
            List<Employee> EmployeeInfo = new List<Employee>();

            HeaderClearing();

            // Sending Request to the find web api Rest service resources using HTTPClient
            HttpResponseMessage httpResponseMessage = await clnt.GetAsync("api/Employee");

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                EmployeeInfo = JsonConvert.DeserializeObject<List<Employee>>(responseMessage);
            }

            return View(EmployeeInfo);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            // Creating a Get Request to get single Employee
            Employee EmployeeDetails = new Employee();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results
            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Employee/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                EmployeeDetails = JsonConvert.DeserializeObject<Employee>(detailsInfo);
            }

            EmployeeDetails.EmployeeDepartment = GetDepartment(EmployeeDetails.EmployeeDepartmentId);

            return View(EmployeeDetails);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.Departments = GetDepartmentsSelectList();
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            employee.EmployeeDepartment = GetDepartment(employee.EmployeeDepartmentId);

            // serializing employee object into json format to send
            /*string jsonObject = "{"+employee."}"*/
            string createEmployeeInfo = JsonConvert.SerializeObject(employee);

            // creating string content to pass as Http content later
            StringContent stringContentInfo = new StringContent(createEmployeeInfo, Encoding.UTF8, "application/json");

            // Making a Post request
            HttpResponseMessage createHttpResponseMessage = clnt.PostAsync(clnt.BaseAddress + "api/Employee/", stringContentInfo).Result;
            Console.WriteLine(createHttpResponseMessage);
            if (createHttpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            // Creating a Get Request to get single Employee
            Employee EmployeeDetails = new Employee();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results
            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Employee/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                EmployeeDetails = JsonConvert.DeserializeObject<Employee>(detailsInfo);
            }

            ViewBag.Departments = GetDepartmentsSelectList();
            return View(EmployeeDetails);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            employee.EmployeeDepartment = GetDepartment(employee.EmployeeDepartmentId);
            // serializing employee object into json format to send
            /*string jsonObject = "{"+employee."}"*/
            string createEmployeeInfo = JsonConvert.SerializeObject(employee);

            // creating string content to pass as Http content later
            StringContent stringContentInfo = new StringContent(createEmployeeInfo, Encoding.UTF8, "application/json");
            // Making a Post request
            HttpResponseMessage createHttpResponseMessage = clnt.PutAsync(clnt.BaseAddress + "api/Employee/" + employee.ID, stringContentInfo).Result;
            if (createHttpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            // Creating a Get Request to get single Employee
            Employee EmployeeDetails = new Employee();

            HeaderClearing();

            // Creating a get request after preparation of get URL and assignin the results
            HttpResponseMessage httpResponseMessageDetails = clnt.GetAsync(clnt.BaseAddress + "api/Employee/" + id).Result;

            // Checking for response state
            if (httpResponseMessageDetails.IsSuccessStatusCode)
            {
                // storing the response details received from web api 
                string detailsInfo = httpResponseMessageDetails.Content.ReadAsStringAsync().Result;

                // deserializing the response
                EmployeeDetails = JsonConvert.DeserializeObject<Employee>(detailsInfo);
            }

            EmployeeDetails.EmployeeDepartment = GetDepartment(EmployeeDetails.EmployeeDepartmentId);

            return View(EmployeeDetails);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Employee employee)
        {
            employee.EmployeeDepartment = GetDepartment(employee.EmployeeDepartmentId);

            // serializing employee object into json format to send
            /*string jsonObject = "{"+employee."}"*/
            string createEmployeeInfo = JsonConvert.SerializeObject(employee);

            // creating string content to pass as Http content later
            StringContent stringContentInfo = new StringContent(createEmployeeInfo, Encoding.UTF8, "application/json");
            // Making a Post request
            HttpResponseMessage createHttpResponseMessage = clnt.DeleteAsync(clnt.BaseAddress + "api/Employee/" + employee.ID).Result;
            if (createHttpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(employee);
        }

        private List<SelectListItem> GetDepartmentsSelectList()
        {
            List<Department> DepartmentInfo = new List<Department>();

            HeaderClearing();

            HttpResponseMessage httpResponseMessage = clnt.GetAsync("api/Department").Result;

            // If the request is success
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // storing the web api data into model that was predefined prior
                var responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;

                DepartmentInfo = JsonConvert.DeserializeObject<List<Department>>(responseMessage);
            }

            var listDepartment = new List<SelectListItem>();

            listDepartment = DepartmentInfo.Select(d => new SelectListItem()
            {
                Value = d.ID.ToString(),
                Text = d.Name
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "--Select Department--"
            };

            listDepartment.Insert(0, defItem);

            return listDepartment;
        }

        private Department GetDepartment(int id)
        {
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

            return DepartmentDetails;
        }
    }
}
