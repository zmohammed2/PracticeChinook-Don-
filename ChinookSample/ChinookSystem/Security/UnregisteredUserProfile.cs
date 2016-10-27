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
        public int CustomerEmployeeId { get; set; }    //generated when User is Added
        public string AssignedUserName { get; set; }  //Collected
        public string AssignedEmail { get; set; }     //Collected
        
        public string FirstName { get; set; } //Comes from User Table
        public string LastName { get; set; }  //Comes from the Use Table
        public UnRegisteredUserType UserType { get; set; }
    

    }
}
