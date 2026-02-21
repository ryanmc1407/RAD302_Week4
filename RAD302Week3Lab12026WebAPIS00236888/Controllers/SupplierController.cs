using ClassLibrary1.Respositories;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Tracker.WebAPIClient;

namespace RAD302Week3Lab12026WebAPIS00236888.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _repo;

        // The constructor injects the repository we registered in Program.cs
        public SupplierController(ISupplierRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Requirement 1: Keep your tracking info
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland",
                activityName: "Rad302 Week 4 Lab 1", Task: "completing Extra time task");

            // Change this line to return the actual data from the CSV
            var suppliers = _repo.GetAll();
            return Ok(suppliers);
        }
    }
}