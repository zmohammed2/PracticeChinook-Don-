﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity.EntityFramework;    //IdentityUser
#endregion
namespace ChinookSystem.Security
{
    // You can add User data for the user by adding more properties 
    //to your User class, 
    //please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeId { get; set; }
        public int? CustomerId { get; set; }
    }
}
