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
    public class SearchEmployeesController : ApiController
    {

        [HttpPost]
        public IHttpActionResult getEmployeeDetails([FromBody] SearchEmployees searchemployees)
        {
            Console.WriteLine(searchemployees.firstname);
            
            string sSQL = "select EmployeeSSN, FirstName, LastName from [ACA].[xferEmployee] ";
            string sConditionOne = "";
            string sConditionTwo = "";
            if (searchemployees.firstname.Trim() == "") { }
            else
            {
                sConditionOne = " FirstName LIKE '%" + searchemployees.firstname.ToUpper() + "%'";
            }
            if (searchemployees.lastname.Trim() == "") { }
            else
            {
                sConditionTwo = " LastName LIKE '%" + searchemployees.lastname.ToUpper() + "%'";
            }
            if (sConditionOne == "" && sConditionTwo == "")
            {
                sSQL = sSQL + " TOP [100]";
            }
            if (sConditionOne == "" && sConditionTwo != "")
            {
                sSQL = sSQL + " WHERE " + sConditionTwo;
            }
            if (sConditionOne != "" && sConditionTwo == "")
            {
                sSQL = sSQL + " WHERE " + sConditionOne;
            }
            if (sConditionOne != "" && sConditionTwo != "")
            {
                sSQL = sSQL + " WHERE " + sConditionOne + " AND " + sConditionTwo;
            }
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeSearchResults>>(json);
            return Ok(listData);
        }
    }
}