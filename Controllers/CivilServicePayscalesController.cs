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
    public class CivilServicePayscalesController : ApiController
    {

        [Route("api/ClassKeyInformation")]
        public IHttpActionResult getClassKeyInformation()
        {
            Console.WriteLine("in get");
            string sSQL = "select PK_CLASS_KEY_ID as 'ClassKeyID', CK_CLASS_TYPE as 'ClassType', CK_CLASS_CODE as 'ClassCode' from [CSP].[xferClassKey]";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<ClassKey>>(json);
            return Ok(listData);
        }

    }
}