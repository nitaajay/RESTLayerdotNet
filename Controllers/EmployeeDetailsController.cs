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
    public class EmployeeDetailsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult getEmployeeDetails()
        {
            Console.WriteLine("in get");
            string sSQL = "select * from [ACA].[xferEmployee] where EmployeeSSN = '614140684'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeDetails>>(json);
            return Ok(listData);
        }

        [Route("api/EmployeeDetails/{id}")]
        public IHttpActionResult getEmployeeDetails(string id )
        {
            Console.WriteLine(id);
            string sSQL = "select * from [ACA].[xferEmployee] where EmployeeSSN = '" + id + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeDetails>>(json);
            return Ok(listData);
        }

        [HttpPost]
        public IHttpActionResult getEmployeeDetails([FromBody] EmployeeDetails empdetails)
        {
            Console.WriteLine(empdetails.EmployeeSSN);
            string sSQL = "select * from [ACA].[xferEmployee] where EmployeeSSN = '" + empdetails.EmployeeSSN + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeDetails>>(json);
            return Ok(listData);
        }

        [HttpPost]
        [Route("api/EmployeeTransactionsHistory")]
        public IHttpActionResult getEmployeeTransactionsHistory([FromBody] EmployeeTransactionsHistory emptranshistory)
        {
            Console.WriteLine(emptranshistory.EmployeeSSN);
            string sSQL = "select EmployeeSSN, CBID, CountyCode, AppointmentType, HistoryRemarks, PayLetterNumber, TimeBase,";
            sSQL += "AppointmentTenure, AppointmentMonths, AppointmentExpirationDate, SalaryPER, PayFrequency, BasePay,";
            sSQL += "SalaryRate, SalaryTotal, SalaryFull, PlusSalary, PlusSalaryExpirationDate, AnniversaryDate,";
            sSQL += "AcceleratedAnniversaryDate, AlternateRange, PayrollStatus, ShiftDifferential, SpecialPay, WorkWeekGroup,";
            sSQL += "FireSeasonSalaryRate, PriorServiceCode, RetirementSystem, PERSRetirementMemberDate, SafetyMember,";
            sSQL += "Survivor, OASDI, RetirementRate, ExemptAuthority, PersAccountCode, PersEmployerCode, ClassType,";
            sSQL += "FixedMaintMonthlyDeduction, EstablishedEarningsID1, EstablishedEarningsAmount1, EstablishedEarningsID2,";
            sSQL += "EstablishedEarningsAmount2, EstablishedEarningsID3, EstablishedEarningsAmount3, ImmediatePay,";
            sSQL += "LumSumSickLeave, LumSumSickHours, LumSumVaction, LumSumVactionHours, LumSumExtraHours, LumSumOvertimeHours,";
            sSQL += "LumSumPayCode, LumSumUnitSerial, SeparationExpirationDate, IntermittentBeginDate1, IntermittentEndDate1,";
            sSQL += "IntermittentHours1, IntermittentBeginDate2, IntermittentEndDate2, IntermittentHours2, IntermittentBeginDate3,";
            sSQL += "IntermittentEndDate3, IntermittentHours3, IntermittentHoursExpect, LegalReferenceAnnuitant ";
            sSQL += " from [EHDB].[xferTransactionHistory] ";
            sSQL += " where EmployeeSSN = '" + emptranshistory.EmployeeSSN + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeTransactionsHistory>>(json);
            return Ok(listData);
        }

        [HttpPost]
        [Route("api/EmployeeTransactionsHistoryListing")]
        public IHttpActionResult getEmployeeTransactionsHistoryListing([FromBody] EmployeeTransactionsHistory emptranshistory)
        {
            Console.WriteLine(emptranshistory.EmployeeSSN);
            string sSQL = "SELECT EmployeeSSN, PositionSequence,PositionNumber,TransactionEffectiveDate";
            sSQL += " ,TransactionCode,EntryDate,PPSDReference1,HistoryType,HistoryRemarks ";
            sSQL += " from [EHDB].[xferTransactionHistory] ";
            sSQL += " where EmployeeSSN = '" + emptranshistory.EmployeeSSN + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeTransactionsHistory>>(json);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/EmployeeTransactionsHistoryWhere")]
        public IHttpActionResult getEmployeeTransactionsHistoryWhere([FromBody] EmployeeTransactionsHistory emptranshistory)
        {
            Console.WriteLine(emptranshistory.EmployeeSSN);
            string sSQL = "select EmployeeSSN, CBID, CountyCode, AppointmentType, HistoryRemarks, PayLetterNumber, TimeBase,";
            sSQL += "AppointmentTenure, AppointmentMonths, AppointmentExpirationDate, SalaryPER, PayFrequency, BasePay,";
            sSQL += "SalaryRate, SalaryTotal, SalaryFull, PlusSalary, PlusSalaryExpirationDate, AnniversaryDate,";
            sSQL += "AcceleratedAnniversaryDate, AlternateRange, PayrollStatus, ShiftDifferential, SpecialPay, WorkWeekGroup,";
            sSQL += "FireSeasonSalaryRate, PriorServiceCode, RetirementSystem, PERSRetirementMemberDate, SafetyMember,";
            sSQL += "Survivor, OASDI, RetirementRate, ExemptAuthority, PersAccountCode, PersEmployerCode, ClassType,";
            sSQL += "FixedMaintMonthlyDeduction, EstablishedEarningsID1, EstablishedEarningsAmount1, EstablishedEarningsID2,";
            sSQL += "EstablishedEarningsAmount2, EstablishedEarningsID3, EstablishedEarningsAmount3, ImmediatePay,";
            sSQL += "LumSumSickLeave, LumSumSickHours, LumSumVaction, LumSumVactionHours, LumSumExtraHours, LumSumOvertimeHours,";
            sSQL += "LumSumPayCode, LumSumUnitSerial, SeparationExpirationDate, IntermittentBeginDate1, IntermittentEndDate1,";
            sSQL += "IntermittentHours1, IntermittentBeginDate2, IntermittentEndDate2, IntermittentHours2, IntermittentBeginDate3,";
            sSQL += "IntermittentEndDate3, IntermittentHours3, IntermittentHoursExpect, LegalReferenceAnnuitant ";
            sSQL += " from [EHDB].[xferTransactionHistory] ";
            sSQL += " where EmployeeSSN = '" + emptranshistory.EmployeeSSN + "' and ";
            sSQL += "  PositionSequence = '" + emptranshistory.PositionSequence + "' and ";
            sSQL += " PositionNumber = '" + emptranshistory.PositionNumber + "' and entrydate = '" + emptranshistory.EntryDate + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<EmployeeTransactionsHistory>>(json);
            return Ok(listData);
        }

    }
}