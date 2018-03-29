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
    public class EmployeePositionDetailsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getEmployeePositionDetails()
        {
            Console.WriteLine("in get");
            string sSQL = "select * from [ACA].[xferPosition] where PositionSSN = '614140684'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeePositionDetails>>(json);
            return Ok(listData);
        }

        [Route("api/EmployeePositionDetails/{id}")]
        public IHttpActionResult getEmployeePositionDetails(string id)
        {
            Console.WriteLine(id);
            string sSQL = "select * from [ACA].[xferPosition] where PositionSSN = '" + id + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeePositionDetails>>(json);
            return Ok(listData);
        }

        [HttpPost]
        public IHttpActionResult getEmployeePositionDetails([FromBody] EmployeeDetails empdetails)
        {
            Console.WriteLine(empdetails.EmployeeSSN);
            string sSQL = "select * from [ACA].[xferPosition] where PositionSSN = '" + empdetails.EmployeeSSN + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeePositionDetails>>(json);
            return Ok(listData);
        }
    }
}