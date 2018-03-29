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
    public class EHEmployeeController : ApiController
    {


        [HttpPost]
        [Route("api/EHEmployeeDetails")]
        public IHttpActionResult getEmployeeDetails([FromBody] EHEmployeeDetails empdetails)
        {
            Console.WriteLine(empdetails.EmployeeSSN);
            string sSQL = "SELECT top 100 [EmployeeSSN] as 'EmployeeSSN'";
            sSQL += ",[PositionSequence] as 'PositionSeq'";
            sSQL += ",[PositionNumber] as 'PositionNo'";
            sSQL += ",[Timebase] as 'Timebase'";
            sSQL += ",[FullTimeEquivalent] as 'FTEquivalent'";
            sSQL += ",[PositionStatus] as 'PosStatus'";
            sSQL += ",[EmploymentType] as 'EmploymentType'";
            sSQL += ",[Department] as 'Dept'";
            sSQL += ",[Facility] as 'Facility'";
            sSQL += ",[OutofServiceCode] as 'OutofService'";
            sSQL += "FROM[ScoDatabank].[EHDB].[xferEmploymentHistory] where EmployeeSSN = '" + empdetails.EmployeeSSN + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EHEmployeeDetails>>(json);
            return Ok(listData);
        }
    }
}