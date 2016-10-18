using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Security
{
    public class UserProfile
    {
        public string UserId { get; set;}      //AspNetUser table
        public string UserName { get; set; }   //AspNetUser table
        public int? EmployeeId { get; set; }   //AspNetUser table
        public int? CustomerId { get; set; }   //AspNetUser table
        public string FirstName { get; set; }  //Employee table or Customer Table
        public string LastName { get; set; }   //Employee table or Customer Table
        public string Email { get; set; }     //AspNetUser table
        public string EmailConfirmed { get; set; }   //AspNetUser table
        public IEnumerable<string> RoleMemberships { get; set; }
    }
}
