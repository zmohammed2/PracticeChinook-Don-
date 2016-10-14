﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel; //ODS
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class CustomerController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RepresentitiveCustomers> RepresentitiveCustomers_Get(int employeeid)
        {
            //set up transaction area
            using (var context = new ChinookContext())
            {
                var results = from x in context.Customers
                              where x.SupportRepId == employeeid
                              orderby x.LastName, x.FirstName
                              select new RepresentitiveCustomers
                              {
                                  Name = x.LastName + ", " + x.FirstName,
                                  City = x.City,
                                  State = x.State,
                                  Phone = x.Phone,
                                  Email = x.Email
                              };
                return results.ToList();
            }
        }
    }
}
