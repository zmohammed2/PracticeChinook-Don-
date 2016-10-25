using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Security
{
    public  enum UnRegisteredUserType { undefined, Employee, Customer}
    public class UnRegisteredUserProfile
    {
        public int UserId { get; set; }    //generated when User is Added
        public string UserName { get; set; }  //Collected
        public string Email { get; set; }     //Collected
        
        public string FirstName { get; set; } //Comes from User Table
        public string LastName { get; set; }  //Comes from the Use Table
        public UnRegisteredUserType UserType { get; set; }
    

    }
}
