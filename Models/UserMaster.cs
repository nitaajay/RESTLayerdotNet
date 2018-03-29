using System;

namespace WebApiSelfHostApp.Models
{
    public class UserMaster
    {
        public int UserId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
