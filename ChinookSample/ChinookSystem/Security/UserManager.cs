using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity.EntityFramework;    //UserStore
using Microsoft.AspNet.Identity;                    //UserManager
using System.ComponentModel;     //ODS
using ChinookSystem.DAL;         //context class
using ChinookSystem.Data.Entities;    //Entity classes
#endregion
namespace ChinookSystem.Security
{
    [DataObject]
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }

        //setting up the default webMaster
        #region Constants
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        private const string STR_USERNAME_FORMAT = "{0}.{1}";
        private const string STR_EMAIL_FORMAT = "{0}@Chinook.ca";
        private const string STR_WEBMASTER_USERNAME = "Webmaster";
        #endregion

        public void AddWebmaster()
        {


            if (!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                var webMasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };
                //This create Command is from the inheritant UserManager class
               // This command creates a record on the security Users Table (AspNetUsers)
                this.Create(webMasterAccount, STR_DEFAULT_PASSWORD);
                //this Add to Role Command is from the inherited UserManager class
                //This command creates a record on the Security UserRole Table (AspNetUserRoles)
                this.AddToRole(webMasterAccount.Id, SecurityRoles.WebsiteAdmins);
            }

         } //eom

        //create the CRUD methods for adding a user to the security user tables
        //read of data to diplay on GridView
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<UnRegisteredUserProfile> ListAllUnregisteredUsers()
        {
            using (var context = new ChinookContext())
            {
                //IEnumerable set containing th elist of employees
                var registeredEmployees = (from emp in Users
                                          where emp.EmployeeId.HasValue
                                          select emp.EmployeeId).ToList();
                //compare the Ienumerable set to the uSer data table Employees
                var unregisteredEmployees = (from emp in context.Employees
                                            where !registeredEmployees.Any(eid => emp.EmployeeId == eid)
                                            select new UnRegisteredUserProfile()
                                            {
                                                CustomerEmployeeId = emp.EmployeeId,
                                                FirstName = emp.FirstName,
                                                LastName = emp.LastName,
                                                UserType = UnRegisteredUserType.Employee
                                            }).ToList();

                //List() set containing th elist of customerIds
                var registeredCustomers = (from cus in Users
                                          where cus.CustomerId.HasValue
                                          select cus.CustomerId).ToList();
                //compare the Ienumerable set to the uSer data table Employees
                var unregisteredCustomers = (from cus in context.Customers
                                            where !registeredCustomers.Any(cid => cus.CustomerId == cid)
                                            select new UnRegisteredUserProfile()
                                            {
                                                CustomerEmployeeId = cus.CustomerId,
                                                FirstName = cus.FirstName,
                                                LastName = cus.LastName,
                                                UserType = UnRegisteredUserType.Customer
                                            }).ToList();
                //combine the two physically identical layout datasets
                return unregisteredEmployees.Union(unregisteredCustomers).ToList();

            }

        }//eom

        //register a user to the user Table (gridview)
        public void RegisterUser(UnRegisteredUserProfile userinfo)
        {
            //basic information needed for the security User Record
            //password, Email, username
            //you could randomly generate a password, we will use the default password
            //the instance of the required user is based on our ApplicationUser
            var newuseraccount = new ApplicationUser()
            {
                UserName = userinfo.AssignedUserName,
                Email = userinfo.AssignedEmail
            };

            //set the customerId or employeeId
            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        newuseraccount.CustomerId = userinfo.CustomerEmployeeId;
                        break;
                    }

                case UnRegisteredUserType.Employee:
                    {
                        newuseraccount.CustomerId = userinfo.CustomerEmployeeId;
                        break;
                    }
            }

            //create the actual AspNet User record
            this.Create(newuseraccount, STR_DEFAULT_PASSWORD);

            //assingned user to an appropiate Role
            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.RegisteredUsers);
                        break;
                    }

                case UnRegisteredUserType.Employee:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.Staff);
                        break;
                    }
            }



        }//eom

        //list all current Users
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<UserProfile>ListAllUsers()
        {
            // we will be using the role manager to get roles
            var rm = new RoleManager();

            //get teh current Users of the User security table
            var results = from person in Users.ToList()
                          select new UserProfile()
                          {
                              UserId = person.Id,
                              UserName = person.UserName,
                              Email = person.Email,
                              EmailConfirmed = person.EmailConfirmed,
                              CustomerId = person.CustomerId,
                              EmployeeId = person.EmployeeId,
                              RoleMemberships = person.Roles.Select(r => rm.FindById(r.RoleId).Name)
                          };
            //Using our own Dta Tables gather the User Firstname and lastName
            using (var context = new ChinookContext())
            {
                Employee etemp;
                Customer ctemp;
                foreach(var person in results)
                {
                    if (person.EmployeeId.HasValue)
                    {
                        etemp = context.Employees.Find(person.EmployeeId);
                        person.FirstName = etemp.FirstName;
                        person.LastName = etemp.LastName;
                    }
                    else if (person.CustomerId.HasValue)
                    {
                        ctemp = context.Customers.Find(person.CustomerId);
                        person.FirstName = ctemp.FirstName;
                        person.LastName = ctemp.LastName;
                    }
                    else
                    {
                        person.FirstName = "unKnown";
                        person.LastName = "";
                    }
                }
            }
            return results.ToList();
        }

        //add a user to the user Table (ListView)
        [DataObjectMethod(DataObjectMethodType.Insert,true)]
        public void AddUser(UserProfile userinfo)
        {
            //create an instance representing the new user
            var useraccount = new ApplicationUser()
            {
                UserName = userinfo.UserName,
                Email = userinfo.Email
            };

            //to create the new user on the physical Users Table
            this.Create(useraccount, STR_DEFAULT_PASSWORD);

            //to Create the UserRoles which were choosen at insert time
            foreach(var roleName in userinfo.RoleMemberships)
            {
                this.AddToRole(useraccount.Id, roleName);
            }
        }//eom

        //delete a user from teh user table (ListView)
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void RemoveUser(UserProfile userinfo)
        {
            //business rule
            //the webMaster cannot be deleted

            //realise that the only information you have at this time is the DataKeyNames Values which is the UserId
            //(on the User security table the field is Id)

            //odtain the username from the security user table using 
            //the user Id value
            string UserName = this.Users.Where(u => u.Id == userinfo.UserId).Select(u => u.UserName).SingleOrDefault().ToString();

            //remove the User
            if(UserName.Equals(STR_WEBMASTER_USERNAME))
            {
                throw new Exception("the webmaster account cannot be removed.");
            }
            this.Delete(this.FindById(userinfo.UserId));

        }



    }//eoc(end of class)



}//eon(end of NameSpace)
