﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BusinessRuleException
/// //the content that will be placed in an instance of this class
/// //will come ffrom user code within your application source (ie BLL business rule exception)
/// 
///
/// </summary>
[Serializable]
public class BusinessRuleException : Exception
{
    public List<string> RuleDetails { get; set; }
    public BusinessRuleException(string message, List<string> reasons)
        : base(message)
    {
        this.RuleDetails = reasons;
    }
}