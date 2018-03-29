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
    public class DashboardController : ApiController
    {
        [HttpGet]
        [Route("api/TotalACAEmployees")]
        public IHttpActionResult getTotalACAEmployees()
        {
            Console.WriteLine("in TotalACA");
            string sSQL = "select count(*) from [ACA].[xferEmployee]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalACAHealthRecords")]
        public IHttpActionResult getTotalACAHealthRecords()
        {
            Console.WriteLine("in TotalACA");
            string sSQL = "select count(*) from [ACA].[xferHealthRecord]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalACAPaymentRecords")]
        public IHttpActionResult getTotalACAPaymentRecords()
        {
            Console.WriteLine("in TotalACA");
            string sSQL = "select count(*) from [ACA].[xferPaymentHistory]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalACATransRecords")]
        public IHttpActionResult getTotalACATransRecords()
        {
            try
            {
                Console.WriteLine("in TotalACATransRecords\n");
                string sSQL = "select count(*) from [ACA].[xferTransaction]";
                var appBlock = new SqlDbConnectionBaseClass();
                var result = appBlock.ExecuteForSelect(sSQL);
                Console.Write("Results returned!\n");
                return Ok(result);
            } catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return (null);
            }
        }

        [HttpGet]
        [Route("api/TotalClassKeys")]
        public IHttpActionResult getTotalClassKeys()
        {
            Console.WriteLine("in TotalClassKeys");
            string sSQL = "select count(*) from [CSP].[xferClassKey]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalClassData")]
        public IHttpActionResult getTotalClassData()
        {
            Console.WriteLine("in TotalClassData");
            string sSQL = "select count(*) from [CSP].[xferClassData]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalSalaryRanges")]
        public IHttpActionResult getTotalSalaryRanges()
        {
            Console.WriteLine("in TotalSalaryRanges");
            string sSQL = "select count(*) from [CSP].[xferSalaryRange]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalTIMSTables")]
        public IHttpActionResult getTotalTIMSTables()
        {
            Console.WriteLine("in TotalTIMSTables");
            string sSQL = "select count(*) from [TIMS].[xferT_TIMS_TABLE]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        // following is for CSP Dashboard
        [HttpGet]
        [Route("api/ClassDataAudit")]
        public IHttpActionResult getClassDataAudit()
        {
            Console.WriteLine("in ClassDataAudit");
            string sSQL = "SELECT[CD_AUDIT_TRAIL_USER_ID] as 'AuditUserID'";
            sSQL += " ,[CD_CLASS_TYPE] as 'ClassType' ";
            sSQL += " ,[CD_CLASS_CODE] as 'ClassCode' ";
            sSQL += " ,[CD_RECORD_EFF_CDATE] as 'EffDate' ";
            sSQL += " ,[CD_PROCESSING_CDATE] as 'ProcessingDate' ";
            sSQL += " ,[CD_AUDIT_OVERRIDE_CODE] as 'OverrideCode' ";
            sSQL += " ,[CD_RECORD_PROCESS_CODE] as 'ProcessCode' ";
            sSQL += " ,[CD_MAJOR_OCCUPATION_GRP_ID] as 'OccuGroupID' ";
            sSQL += " ,[CD_MAJOR_OCCUPATION_GRP_NBR] as 'OccGroupNumber' ";
            sSQL += " ,[CD_CLASS_ESTABLISH_CDATE] as 'ClassEstDate' ";
            sSQL += " ,[CD_CLASS_STATUS_IND] as 'ClassStatusInd' ";
            sSQL += " ,[CD_CBID_DESIGNATION_ID] as 'CBIDDesignationID' ";
            sSQL += " ,[CD_CBID_UNIT_ID] as 'CBIDUnitID' ";
            sSQL += " ,[CD_FULL_CLASS_TITLE_1_60] as 'ClassTitle1' ";
            sSQL += " ,[CD_FULL_CLASS_TITLE_61_120] as 'ClassTitle2' ";
            sSQL += " ,[CD_ABBR_CLASS_TITLE] as 'AbbrClassTitle' ";
            sSQL += " ,[CD_PAY_LETTER_NBR] as 'LetterNumber' ";
            sSQL += " FROM[ScoDatabank].[CSP].[xferClassData] ";
            sSQL += " where CD_PROCESSING_CDATE LIKE '" + System.DateTime.Today.Year + "%' ";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<ClassDataAudit>>(json);
            return Ok(listData);
        }

        [HttpGet]
        [Route("api/TotalAuditUsersClassDatainCurrentYearMonth/{id}")]
        public IHttpActionResult getTotalAuditUsersinCurrentYearMonth(int id)
        {
            Console.WriteLine("in TotalAuditUsersClassDatainCurrentYearMonth");
            string sSQL = "SELECT count(*) as 'Column1'";
            sSQL += " FROM [CSP].[xferClassData] ";
            sSQL += " where CD_PROCESSING_CDATE LIKE ";
            sSQL += " CAST((CAST(YEAR(GETDATE()) as VARCHAR) + ";
            sSQL += " RIGHT('0' + CAST(" + id + " as VARCHAR), 2) + '%') as VARCHAR) ";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalEmploymentHistoryRecords")]
        public IHttpActionResult getTotalEmploymentHistoryRecords()
        {
            Console.WriteLine("in TotalEmploymentHistoryRecords");
            string sSQL = "SELECT count(*) as 'Column1'";
            sSQL += " FROM [EHDB].[xferEmploymentHistory] ";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalCivilEmployeeHistory")]
        public IHttpActionResult getTotalCivilEmployeeHistory()
        {
            Console.WriteLine("in TotalCivilEmployeeHistory");
            string sSQL = "SELECT count(*) as 'Column1'";
            sSQL += " FROM [EHDB].[xferEmploymentHistory] where employmenttype LIKE 'CIVIL%'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalCSUCEmployeeHistory")]
        public IHttpActionResult getTotalCSUCEmployeeHistory()
        {
            Console.WriteLine("in TotalCSUCEmployeeHistory");
            string sSQL = "SELECT count(*) as 'Column1'";
            sSQL += " FROM [EHDB].[xferEmploymentHistory] where employmenttype LIKE 'CSUC%'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalEmployeeTransactions")]
        public IHttpActionResult getTotalEmployeeTransactions()
        {
            Console.WriteLine("in TotalCSUCEmployeeHistory");
            string sSQL = "SELECT count(*) as 'Column1'";
            sSQL += " FROM [EHDB].[xferTransactionHistory]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalUniqueEmployeesinTransactions")]
        public IHttpActionResult getTotalUniqueEmployeesinTransactions()
        {
            Console.WriteLine("in TotalUniqueEmployeesinTransactions");
            string sSQL = "select distinct EmployeeSSN from [EHDB].[xferTransactionHistory]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalFTEmployees")]
        public IHttpActionResult getTotalFTEmployees()
        {
            Console.WriteLine("in TotalFTEmployees");
            string sSQL = "select count(*) as 'Column1' from [EHDB].[xferTransactionHistory] where timebase = 'FT'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalINTEmployees")]
        public IHttpActionResult getTotalINTEmployees()
        {
            Console.WriteLine("in TotalINTEmployees");
            string sSQL = "select count(*) as 'Column1' from [EHDB].[xferTransactionHistory] where timebase = 'INT'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalINDEmployees")]
        public IHttpActionResult getTotalINDEmployees()
        {
            Console.WriteLine("in TotalINDEmployees");
            string sSQL = "select count(*) as 'Column1' from [EHDB].[xferTransactionHistory] where timebase = 'IND'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/TotalbasedonPositionStatus/{id}")]
        public IHttpActionResult getTotalbasedonPositionStatus(string id)
        {
            Console.WriteLine("in TotalbasedonPositionStatus");
            string sSQL = "";

            switch (id)
            {
                case "active": 
                    sSQL = "select count(*) as Column1 from [ScoDatabank].[EHDB].[xferEmploymentHistory] where positionstatus LIKE 'ACTIVE%'";
                    break;
                case "onleave":
                    sSQL = "select count(*) as Column1 from[ScoDatabank].[EHDB].[xferEmploymentHistory] where positionstatus LIKE 'ON LEAVE%'";
                    break;
                case "separated":
                    sSQL = "select count(*) as Column1 from[ScoDatabank].[EHDB].[xferEmploymentHistory] where positionstatus LIKE 'SEPARATED%'";
                    break;
                case "tempsep":
                    sSQL = "select count(*) as Column1 from[ScoDatabank].[EHDB].[xferEmploymentHistory] where positionstatus LIKE 'TEMP SEP%'";
                    break;
            }
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/DataRefreshLog")]
        public IHttpActionResult getDataRefreshLog()
        {
            Console.WriteLine("in DataRefreshLog");
            string sSQL = "SELECT [DataRefreshID] as 'ID', [SchemaAbbreviation] as 'Abbr', [SchemaName] as 'Schema'";
            sSQL += " ,[Date] as 'DateRefreshed', [Time] as 'Timerefreshed'";
            sSQL += " FROM[APP].[DataRefreshLog]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }
    }
}