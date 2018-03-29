using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using WebApiSelfHostApp.DataAccessLayer;
using WebApiSelfHostApp.Models;

namespace WebApiSelfHostApp.BusinessLayer
{
    public class UserMasterHelper
    {
        private readonly SqlDbHelper _sqlDbHelper;

        public UserMasterHelper()
        {
            _sqlDbHelper = new SqlDbHelper();
        }

        public List<UserMaster> GetUserMaster(UserMaster userMaster)
        {
            string sqlQuery = "SELECT * FROM UserMaster WHERE Username = '" + userMaster.Username +
                              "' AND Password = '" + userMaster.Password + "'";
            var dataTable = _sqlDbHelper.ExecuteNonQuery(sqlQuery, CommandType.Text);
            var json = JsonConvert.SerializeObject(dataTable);
            var users = JsonConvert.DeserializeObject<List<UserMaster>>(json);
            return users;
        }
    }
}
