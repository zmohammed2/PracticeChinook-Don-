﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.Security
{
    public static class SecurityRoles
    {
        public const string WebsiteAdmins = "WebsiteAdmins";
        public const string RegisteredUsers = "RegisteredUsers";
        public const string Staff = "Staff";
        public const string Auditor = "Auditor";

        //readonly
        internal static List<string> StartupSecurityRoles
        {
            get
            {
                List<string> roleList = new List<string>();
                roleList.Add(WebsiteAdmins);
                roleList.Add(RegisteredUsers);
                roleList.Add(Staff);
                roleList.Add(Auditor);
                return roleList;
            }
        }
    }
}
