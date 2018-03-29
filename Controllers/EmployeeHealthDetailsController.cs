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
    public class EmployeeHealthDetailsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getEmployeeHealthDetails()
        {
            Console.WriteLine("in get");
            string sSQL = "select * from [ACA].[xferHealthRecord] where HealthSSN = '614140684'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeHealthDetails>>(json);
            return Ok(listData);
        }

        [Route("api/EmployeeHealthDetails/{id}")]
        public IHttpActionResult getEmployeeHealthDetails(string id)
        {
            Console.WriteLine(id);
            string sSQL = "select * from [ACA].[xferHealthRecord] where HealthSSN = '" + id + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeHealthDetails>>(json);
            return Ok(listData);
        }

        [HttpPost]
        public IHttpActionResult getEmployeeHealthDetails([FromBody] EmployeeDetails empdetails)
        {
            Console.WriteLine(empdetails.EmployeeSSN);
            string sSQL = "select * from [ACA].[xferHealthRecord] where HealthSSN = '" + empdetails.EmployeeSSN + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeHealthDetails>>(json);
            return Ok(listData);
        }
    }
}