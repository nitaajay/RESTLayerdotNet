using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using BusinessLayer.Models;
using DataAccessLayer;
using Newtonsoft.Json;

namespace WebApiDataBaseConnectivity.Controllers
{
    public class EmployeeACATransactionsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getEmployeeACATransactions()
        {
            Console.WriteLine("in get");
            string sSQL = "select * from [ACA].[xferTransaction] where TransactionSSN = '614140684'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeACATransactions>>(json);
            return Ok(listData);
        }

        [Route("api/EmployeeACATransactions/{id}")]
        public IHttpActionResult getEmployeeACATransactions(string id)
        {
            Console.WriteLine(id);
            string sSQL = "select * from [ACA].[xferTransaction] where TransactionSSN = '" + id + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeACATransactions>>(json);
            return Ok(listData);
        }

        [HttpPost]
        public IHttpActionResult getEmployeeACATransactions([FromBody] EmployeeDetails empdetails)
        {
            Console.WriteLine(empdetails.EmployeeSSN);
            string sSQL = "select * from [ACA].[xferTransaction] where TransactionSSN = '" + empdetails.EmployeeSSN + "' and PositionsequenceNumber = '" + empdetails.FutureUse1 + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeACATransactions>>(json);
            return Ok(listData);
        }
    }
}