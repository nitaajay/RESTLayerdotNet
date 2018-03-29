using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using BusinessLayer.Models;
using DataAccessLayer;
using Newtonsoft.Json;
using System;

namespace WebApiDataBaseConnectivity.Controllers
{
    public class UserLoginController : ApiController
    {

        [HttpPost]
        public IHttpActionResult UserLogin([FromBody] UserLogin userLogin)
        {
            try
            {
                string sSQL = "";
                var oreturnValue = new AuthenticateUserReturn();
                //oreturnValue.returnCount = 1;
                // return Ok(oreturnValue);
                var appBlock = new SqlDbConnectionBaseClass();
                SqlParameter[] parameters =
                {
                new SqlParameter("@userName", SqlDbType.NVarChar) {Value = userLogin.Username},
                new SqlParameter("@password", SqlDbType.NVarChar) {Value = userLogin.Password},
                new SqlParameter("@usercount", SqlDbType.NVarChar) {Value = "-1"}
            };
                //var result = appBlock.ExecuteProcedureForSelect("SpAuthenticateUser", parameters, "");
                sSQL = "select * from Usermaster where username ='" + userLogin.Username + "' and Password = '" + userLogin.Password + "'";
                var result = appBlock.ExecuteNonQuery(sSQL);
                Console.Write("Got returned");
                var json = JsonConvert.SerializeObject(result);
                Console.Write(result.ToString());
                return Ok(result);
            } catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return (null);
            }
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var appBlock = new SqlDbConnectionBaseClass();
            SqlParameter[] parameters =
            {
                new SqlParameter("@userName", SqlDbType.NVarChar) {Value = ""},
                new SqlParameter("@password", SqlDbType.NVarChar) {Value = ""}
            };
            var result = appBlock.ExecuteProcedureForSelect("SpCheckUserLogin", parameters, "");
            var json = JsonConvert.SerializeObject(result);
            var listData = JsonConvert.DeserializeObject<List<AuthenticateUserReturn>>(json);
            return Ok(listData);
        }

        [HttpGet]
        [Route("api/getUserDetails/{id}")]
        public IHttpActionResult getUserDetails(int id)
        {
            Console.WriteLine("in getUserDetails");
            string sSQL = "";
            sSQL = "select UserID, LTRIM(RTRIM(Username)) as Username, Password, LTRIM(RTRIM(ContactName)) as ContactName from DBO.UserMaster";
            sSQL = sSQL + " where UserID =" + id;
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/getMenus")]
        public IHttpActionResult getMenus()
        {
            Console.WriteLine("in getMenus");
            string sSQL = "";
            sSQL = "select MenuID, MenuTitle, IsParent, MenuLink, ParentID from APP.MenuMaster";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/getUserParentMenus/{id}")]
        public IHttpActionResult getUserParentMenus(int id)
        {
            Console.WriteLine("in getUserParentMenus");
            string sSQL = "";
            sSQL = "select MenuID, LTRIM(RTRIM(MenuTitle)) as MenuTitle, LTRIM(RTRIM(MenuLink)) as MenuLink, LTRIM(RTRIM(menuicon)) as menuicon from app.menumaster where menuid in ";
            sSQL = sSQL + "(select menuid from app.menumaster where parentid = 0)";
            sSQL = sSQL + "and menuid in (select menuid from app.usermenumapping ";
            sSQL = sSQL + " where userid = " + id + ")";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/getUserAllParentMenus")]
        public IHttpActionResult getUserAllParentMenus()
        {
            Console.WriteLine("in getUserAllParentMenus");
            string sSQL = "";
            sSQL = "select MenuID, LTRIM(RTRIM(MenuTitle)) as MenuTitle, LTRIM(RTRIM(MenuLink)) as MenuLink, LTRIM(RTRIM(menuicon)) as menuicon from app.menumaster where menuid in ";
            sSQL = sSQL + "(select menuid from app.menumaster where parentid = 0)";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/getUserChildMenus/{id}")]
        public IHttpActionResult getUserChildMenus(int id)
        {
            Console.WriteLine("in getUserChildMenus");
            string sSQL = "";
            sSQL = "select MenuID, LTRIM(RTRIM(MenuTitle)) as MenuTitle, LTRIM(RTRIM(MenuLink)) as MenuLink, LTRIM(RTRIM(menuicon)) as menuicon  from app.menumaster ";
            sSQL = sSQL + " where parentid = " + id ;
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/getUserAllChildMenus/{id}")]
        public IHttpActionResult getUserAllChildMenus(int id)
        {
            Console.WriteLine("in getUserAllChildMenus");
            string sSQL = "";
            sSQL = "select MenuID, LTRIM(RTRIM(MenuTitle)) as MenuTitle, LTRIM(RTRIM(MenuLink)) as MenuLink, LTRIM(RTRIM(menuicon)) as menuicon  from app.menumaster ";
            sSQL = sSQL + " where parentid = " + id;
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }
        [HttpGet]
        [Route("api/getUserListing")]
        public IHttpActionResult getUserListing()
        {
            Console.WriteLine("in getUserListing");
            string sSQL = "";
            sSQL = "select UserID, Username, ContactName from DBO.UserMaster";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/logUserAudit/{id}")]
        public IHttpActionResult logUserAudit(int id)
        {
            Console.WriteLine("in logUserAudit");
            string sSQL = "";
            sSQL = "insert into App.UserAudit(UserID, Action, Date, Time) values(";
            sSQL += id + ", 'Login Event', '" + System.DateTime.Today.Month + "/" + System.DateTime.Today.Day + "/" + System.DateTime.Today.Year + "', '" + DateTime.Now.ToString() + "')"; 
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteForSelect(sSQL);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/ValidateNewUser")]
        public IHttpActionResult ValidateNewUser([FromBody] UserLogin userLogin)
        {
            string sSQL = "";
            sSQL = "select count(*) as 'Column1' from Usermaster ";
            sSQL += " where Username = '" + userLogin.Username + "'";
            var appBlock = new SqlDbConnectionBaseClass();
            var result = appBlock.ExecuteNonQuery(sSQL);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/CreateNewUser")]
        public IHttpActionResult CreateNewUser([FromBody] UserLogin userLogin)
        {
            try
            {
                string sSQL = "";
                sSQL = "insert into DBO.Usermaster (Username, Password, ContactName) ";
                sSQL += "values('" + userLogin.Username + "', '" + userLogin.Password + "', '" + userLogin.ContactName + "')";
                var appBlock = new SqlDbConnectionBaseClass();
                var result1 = appBlock.ExecuteNonQuery(sSQL);
                // retrieve the ID of new user and return it back
                sSQL = "select UserID from Usermaster where Username='" + userLogin.Username + "'";
                var result = appBlock.ExecuteNonQuery(sSQL);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return (null);
            }
        }


        [HttpPost]
        [Route("api/UpdateUser")]
        public IHttpActionResult UpdateUser([FromBody] UserLogin userLogin)
        {
            try
            {
                string sSQL = "";
                sSQL = "Update DBO.Usermaster set Username = '" + userLogin.Username + "'";

                if (userLogin.Password == "") { }
                else
                {
                    sSQL += ", Password = '" + userLogin.Password + "'";
                }
                sSQL += " where UserID =" + userLogin.UserId;
                var appBlock = new SqlDbConnectionBaseClass();
                var result = appBlock.ExecuteNonQuery(sSQL);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return (null);
            }
        }

        [HttpPost]
        [Route("api/CreateUserMenuMapping")]
        public IHttpActionResult CreateUserMenuMapping([FromBody] UserMenuMapping usermenumapping)
        {
            try
            {
                string sSQL = "";
                sSQL = "insert into APP.UserMenuMapping (UserID, MenuID) ";
                sSQL += "values(" + usermenumapping.UserID + ", " + usermenumapping.MenuID + ")";
                var appBlock = new SqlDbConnectionBaseClass();
                var result = appBlock.ExecuteNonQuery(sSQL);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return (null);
            }
        }


        [HttpGet]
        [Route("api/DeleteUserMenuMapping/{id}")]
        public IHttpActionResult DeleteUserMenuMapping(int id)
        {
            try
            {
                string sSQL = "";
                sSQL = "delete from APP.UserMenuMapping ";
                sSQL += " where UserID =" + id ;
                var appBlock = new SqlDbConnectionBaseClass();
                var result = appBlock.ExecuteNonQuery(sSQL);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return (null);
            }
        }
    }
}
