using ClassLibrary1.Interfaces;
using ClassLibrary1.Respositories;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using Tracker.WebAPIClient;
namespace RAD302Week3Lab12026WebAPIS00236888
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer<Customer> _repository;

        public CustomerController(ICustomer<Customer> repository)
        {
            _repository = repository;
        }

        // Must decorate for swagger
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName: "Ryan McClelland",
                activityName: "Rad302 Week 3 Lab 1",
                Task: "Testing Basic Controller Call");

            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public Customer GetCustomerbyID(int id)
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName:
                "Ryan McClelland", activityName: "Rad302 Week 3 Lab 1",
                Task: "Testing Get Customer By ID Call");

            return _repository.GetById(id);
        }
        // For swagger, we need to specify the route parameters in the HttpPost attribute
        //[HttpPost("CheckCredit/{ID}/{orderAmount}")]
        //public bool CheckCustomerCredit(int ID, decimal orderAmount)
        //{
        //    ActivityAPIClient.Track(StudentID: "S00236888", StudentName:
        //        "Ryan McClelland", activityName: "Rad302 Week 3 Lab 1",
        //        Task: "Testing Check Customer Credit Rating Call");

        //    return _repository.CheckCustomerCredit(ID, orderAmount);

        // for view model as json object
        [HttpPost("CheckCredit")]
        public bool CheckCustomerCredit([FromBody] CustomerCreditRequest request)
        {
            ActivityAPIClient.Track(StudentID: "S00236888", StudentName:
                "Ryan McClelland", activityName: "Rad302 Week 3 Lab 1",
                Task: "Testing Check Customer Credit Rating Call");
            return _repository.CheckCustomerCredit(request.CustomerID, request.OrderAmount);
        }
    }
}