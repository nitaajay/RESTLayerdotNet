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
    public class ReportingController : ApiController
    {
        [HttpGet]
        [Route("api/AllEmployeePermanentlySep5D6ACodeReport")]
        public IHttpActionResult getEmployeePermanentlySep5D6ACodeReport()
        {
            Console.WriteLine("in EmployeePermanentlySep5D6ACodeReport");
            string sSQL = "";
            sSQL = "select distinct c.EmployeeSSN as 'EmployeeSSN', LTRIM(RTRIM(c.FirstName)) + ' ' + LTRIM(RTRIM(c.MiddleInitial)) + ' ' + LTRIM(RTRIM(c.LastName)) as 'LastName',";
            sSQL = sSQL + "a.AgencyCode as 'Agency', a.TimeBase as 'Timebase', a.AppointmentTenure as 'Tenure', d.PositionNumber as 'PositionNo', ";
            sSQL = sSQL + "d.PositionSequenceNumber as 'PosSeqNo', b.TransactionEffectiveDate as 'EffectiveDate', b.StatusCode as 'ACAStatus'  ";
            sSQL = sSQL + " from ACA.xferPosition as a, ACA.xferTransaction as b, aca.xferEmployee as c, aca.xferPaymentHistory as d";
            sSQL = sSQL + " where c.EmployeeSSN = a.PositionSSN  and c.EmployeeSSN = b.TransactionSSN ";
            sSQL = sSQL + " and a.PositionSequenceNumber = b.PositionsequenceNumber and a.SeparationCodeText = 'SEP' ";
            sSQL = sSQL + "and b.StatusCode not in ( '5D', '6A') and c.EmployeeSSN = d.PaymentHistorySSN";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/AllEmployeeWithOutHealthCoverageReport")]
        public IHttpActionResult getAllEmployeeWithOutHealthCoverageReport()
        {
            Console.WriteLine("in AllEmployeeWithOutHealthCoverageReport");
            string sSQL = "";
            sSQL = "select  a.EmployeeSSN as 'EmployeeSSN',  LTRIM(RTRIM(a.FirstName)) + ' ' + LTRIM(RTRIM(a.MiddleInitial)) + ' ' + LTRIM(RTRIM(a.LastName)) as 'LastName',";
            sSQL = sSQL + " d.StatusCode as 'ACAStatus', e.PositionNumber as 'PositionNo', e.PositionSequenceNumber as 'PosSeqNo'";
            sSQL = sSQL + " from aca.xferEmployee as a, aca.xferHealthRecord as b, ACA.xferPosition as c,";
            sSQL = sSQL + "  ACA.xferTransaction as d, aca.xferPaymentHistory as e";
            sSQL = sSQL + " where(b.HealthConvertDate = null or b.HealthConvertDate = '') and";
            sSQL = sSQL + " a.EmployeeSSN = b.HealthSSN and a.EmployeeSSN = c.PositionSSN and";
            sSQL = sSQL + " a.EmployeeSSN = d.TransactionSSN and a.EmployeeSSN = e.PaymentHistorySSN";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/AllEmployeeWithoutAddressReport")]
        public IHttpActionResult getAllEmployeeWithoutAddressReport()
        {
            Console.WriteLine("in EmployeeWithoutAddressReport");
            string sSQL = "";
            sSQL = "select  EmployeeSSN as 'EmployeeSSN',  LTRIM(RTRIM(FirstName)) + ' ' + LTRIM(RTRIM(MiddleInitial)) + ' ' + LTRIM(RTRIM(LastName)) as 'LastName',";
            sSQL = sSQL + " City as 'City', Street as 'Street', State as 'State', ZipCode as 'Zip'";
            sSQL = sSQL + " from aca.xferEmployee WHERE Street = '' or City = '' or State = '' or ZipCode = ''";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalEmployeeWithoutStreet")]
        public IHttpActionResult getTotalEmployeeWithoutStreet()
        {
            Console.WriteLine("in getTotalEmployeeWithoutStreet");
            string sSQL = "";
            sSQL = "select  count(*) as 'Total'";
            sSQL = sSQL + " from aca.xferEmployee WHERE Street = ''";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalEmployeeWithoutCity")]
        public IHttpActionResult getTotalEmployeeWithoutCity()
        {
            Console.WriteLine("in getTotalEmployeeWithoutCity");
            string sSQL = "";
            sSQL = "select  count(*) as 'Total'";
            sSQL = sSQL + " from aca.xferEmployee WHERE City = ''";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalEmployeeWithoutState")]
        public IHttpActionResult getTotalEmployeeWithoutState()
        {
            Console.WriteLine("in getTotalEmployeeWithoutState");
            string sSQL = "";
            sSQL = "select  count(*) as 'Total'";
            sSQL = sSQL + " from aca.xferEmployee WHERE State = ''";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalEmployeeWithoutZip")]
        public IHttpActionResult getTotalEmployeeWithoutZip()
        {
            Console.WriteLine("in EmployeeWithoutAddressReport");
            string sSQL = "";
            sSQL = "select  count(*) as 'Total'";
            sSQL = sSQL + " from aca.xferEmployee WHERE ZipCode = ''";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

    }
}