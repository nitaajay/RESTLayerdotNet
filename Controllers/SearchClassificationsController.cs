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
    public class SearchClassificationsController : ApiController
    {

        [HttpPost]
        [Route("api/SearchClassifications")]
        public IHttpActionResult searchClassifications([FromBody] SearchClassifications searchclass)
        {
            Console.WriteLine(searchclass.classificationname);

            string sSQL = "select pk_class_data_id as 'ClassDataID', fk_class_key_id as 'ClassKeyID', CD_Full_class_title_1_60 as 'Title', cd_abbr_class_title as 'Abbr', ";
            sSQL += "  CD_class_code as 'ClassCode'  from csp.xferclassdata where CD_Full_class_title_1_60 LIKE '%" + searchclass.classificationname.Trim() + "%' or ";
            sSQL += "cd_abbr_class_title LIKE '%" + searchclass.classificationname.Trim() + "%'";

            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<ClassificationDataSearchResults>>(json);
            return Ok(listData);
        }

        [HttpPost]
        [Route("api/SearchClassificationsbyClassCode")]
        public IHttpActionResult searchClassificationsbyClassCode([FromBody] SearchClassifications searchclass)
        {
            Console.WriteLine(searchclass.classificationname);

            string sSQL = "select pk_class_data_id as 'ClassDataID', fk_class_key_id as 'ClassKeyID', CD_Full_class_title_1_60 as 'Title', cd_abbr_class_title as 'Abbr', ";
            sSQL += " CD_class_code as 'ClassCode' from csp.xferclassdata where CD_class_code LIKE '" + searchclass.classcode.Trim() + "%'";

            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<ClassificationDataSearchResults>>(json);
            return Ok(listData);
        }
        [HttpPost]
        [Route("api/SearchClassKey")]
        public IHttpActionResult searchClassKey([FromBody] ClassKey classkey)
        {
            Console.WriteLine(classkey.ClassKeyID);

            string sSQL = "select pk_class_key_id as 'ClassKeyID', ck_class_type as 'ClassType', ck_class_code as 'ClassCode' ";
            sSQL += " from csp.xferclasskey where pk_class_key_id = " + classkey.ClassKeyID;

            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<ClassKey>>(json);
            return Ok(listData);
        }

        [HttpPost]
        [Route("api/SearchClassData")]
        public IHttpActionResult searchSalaryRange([FromBody] ClassData classdata)
        {
            Console.WriteLine(classdata.ClassKeyID);

            string sSQL = "select CD_class_code as 'ClassCode', CD_record_eff_cdate as 'RecEffectiveDate', CD_record_process_code as 'RecProcessCode', ";
            sSQL += " cd_major_occupation_grp_id as 'MajorOccuGroupID', cd_full_class_title_1_60 as 'FullClassTitle', CD_Abbr_class_title as 'AbrClassTitle', ";
            sSQL += " cd_pay_letter_nbr as 'PayLetterNumber', cd_exempt_entitlement_code as 'ExemptEntitlementCode', cd_work_week_grp_code_1 as 'WorkWeekGroupCode1' from ";
            sSQL += " [CSP].[xferClassData] where fk_class_key_id = " + classdata.ClassKeyID;

            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<ClassData>>(json);
            return Ok(listData);
        }

        [HttpPost]
        [Route("api/SearchSalaryRange")]
        public IHttpActionResult searchSalaryRange([FromBody] SalaryRange salaryrange)
        {
            Console.WriteLine(salaryrange.ClassKeyID);

            string sSQL = "select a.fk_class_key_id as 'ClassKeyID', a.fk_class_data_id as 'ClassDataID', ";
            sSQL += "SX_CS_CLS_SALRNG_POS as 'SalaryPOS', SR_SALARY_STRUCTURE_CODE as 'SalaryStructureCode', ";
            sSQL += "SR_REGULAR_SALARY_RATE_CNT as 'SalaryRateCount', SR_REGULAR_SALARY_RATE_1 as 'SalaryRate1', ";
            sSQL += "SR_REGULAR_SALARY_RATE_2 as 'SalaryRate2', SR_SALARY_ADJ_CAT_CODE_1 as 'SalaryAdjustCatCode', "; 
            sSQL += "SR_SAL_ADJ_PCT_R_1 as 'SalaryAdjustPercentRate1', SR_PAY_FREQUENCY_IND_1_1_1 as 'PayFreqIndicator', ";
            sSQL += "SR_PAY_FREQUENCY_UNITS_1_1_1 as 'PayFreqUnits', SR_GEN_SAL_INCR_PCT_R_1 as 'IncrementPercent1' ";
            sSQL += "from csp.xfersalaryrange as a, csp.xferclasskey as b, csp.xferclassdata as c ";
            sSQL += "where a.fk_class_data_id = c.pk_class_data_id and a.fk_class_key_id = b.pk_class_key_id ";
            sSQL += "and a.fk_class_key_id = " +  salaryrange.ClassKeyID + " and a.fk_class_data_id in ";
            sSQL += "(SELECT [PK_CLASS_DATA_ID] FROM [ScoDatabank].[CSP].[xferClassData] where [FK_CLASS_KEY_ID] = " + salaryrange.ClassKeyID + ")";

            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<SalaryRange>>(json);
            return Ok(listData);
        }

    }
}