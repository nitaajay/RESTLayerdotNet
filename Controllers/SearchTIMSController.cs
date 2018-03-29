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
    public class SearchTIMSController : ApiController
    {

        [HttpPost]
        [Route("api/SearchTIMSTables")]
        public IHttpActionResult searchClassifications([FromBody] TIMSTableListing timstablelisting)
        {
            Console.WriteLine(timstablelisting.TableID);

            string sSQL = "SELECT DBKEY as 'DBKey',TIMT_TABLE_ID as 'TableID',TIMT_TBL_NAME as 'TableName',TIMT_TBL_INFORMATION_MSG as 'TableInfo', ";
            sSQL += " TIMT_TBL_SCNDRY_KEY_IND as 'SecondaryKeyInd',TIMT_TBL_ENTRY_CHAR_CNT as 'DataCharCount', ";
            sSQL += " TIMT_EFFECTIVE_HIST_IND as 'EffectiveHistInd',TIMT_CONTNS_EFF_HIST_REQD_IND as 'ContainsEffectiveHistReqdInd',";
            sSQL += " TIMT_EXTERNAL_EDIT_PGM_NAME as 'ExternalEditProgram' FROM TIMS.xferT_TIMS_TABLE ";
            sSQL += " where TIMT_TBL_NAME LIKE '%" + timstablelisting.TableID + "%' or TIMT_TABLE_ID LIKE '%" + timstablelisting.TableID + "%' or TIMT_TBL_INFORMATION_MSG LIKE '%" + timstablelisting.TableID + "%'";

            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<TIMSTableListing>>(json);
            return Ok(listData);
        }

        [HttpPost]
        [Route("api/getTIMSTableDetails")]
        public IHttpActionResult getTIMSTableDetails([FromBody] TIMSTableDetails timstabledetails)
        {
            Console.WriteLine(timstabledetails.DBKey);

            string sSQL = "SELECT A.DBKEY as 'DBKey', A.TIMT_TBL_DATA_MASK_9_X_79 as 'DataMask' ";
            sSQL += " ,b.timki_tbl_prmry_key as 'BKey1', b.timki_tbl_scndry_key as 'BKey2', substring(c.timdi_tbl_entry_data_9_X_79,1,79) as 'DataLine1'";
            sSQL += ", substring(c.timdi_tbl_entry_data_9_X_79, 80, 79) as 'DataLine2',substring(c.timdi_tbl_entry_data_9_X_79, 159, 79) as 'DataLine3'";
            sSQL += ",substring(c.timdi_tbl_entry_data_9_X_79, 238, 79) as 'DataLine4',substring(c.timdi_tbl_entry_data_9_X_79, 317, 79) as 'DataLine5'";
            sSQL += " FROM TIMS.xferT_TIMS_TABLE A";
            sSQL += " INNER JOIN TIMS.xferS_UX_TABLE_ENTRY_I s1 ON A.DBKEY = S1.OWNER_DBKEY";
            sSQL += " INNER JOIN TIMS.xferT_TBL_ENTRY_KEY_I B ON S1.MEMBER_DBKEY = B.DBKEY";
            sSQL += " INNER JOIN TIMS.xferS_TABLE_KEY_DATA_I s2 ON B.DBKEY = S2.OWNER_DBKEY";
            sSQL += " INNER JOIN TIMS.xferT_TBL_ENTRY_DATA_I C ON S2.MEMBER_DBKEY = C.DBKEY";
            sSQL += " WHERE A.DBKEY = " + timstabledetails.DBKey + "  order by b.timki_tbl_prmry_key";

            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<TIMSTableDetails>>(json);
            return Ok(listData);
        }
    }
}