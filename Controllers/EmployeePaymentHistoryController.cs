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
    public class EmployeePaymentHistoryController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getEmployeePaymentHistory()
        {
            Console.WriteLine("in get");
            string sSQL = "select * from [ACA].[xferPaymentHistory] where PaymentHistorySSN = '614140684'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeePaymentHistory>>(json);
            return Ok(listData);
        }

        [Route("api/EmployeePaymentHistory/{id}")]
        public IHttpActionResult getEmployeePaymentHistory(string id)
        {
            Console.WriteLine(id);
            string sSQL = "select * from [ACA].[xferPaymentHistory] where PaymentHistorySSN = '" + id + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeePaymentHistory>>(json);
            return Ok(listData);
        }

        [HttpPost]
        public IHttpActionResult getEmployeePaymentHistory([FromBody] EmployeeDetails empdetails)
        {
            Console.WriteLine(empdetails.EmployeeSSN);
            string sSQL = "select * from [ACA].[xferPaymentHistory] where PaymentHistorySSN = '" + empdetails.EmployeeSSN + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeePaymentHistory>>(json);
            return Ok(listData);
        }
    }
}