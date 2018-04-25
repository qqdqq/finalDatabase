using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ThemeParkDatabase.Authorization
{
    public static class AuthorizationOperations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

        public static OperationAuthorizationRequirement Details =
            new OperationAuthorizationRequirement { Name = Constants.DetailsOperationName };

        public static OperationAuthorizationRequirement Edit =
           new OperationAuthorizationRequirement { Name = Constants.EditOperationName };

        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

        public static OperationAuthorizationRequirement Approve =
            new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };

        public static OperationAuthorizationRequirement Reject =
            new OperationAuthorizationRequirement { Name = Constants.RejectOperationName };

    }

    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string DetailsOperationName = "View";
        public static readonly string EditOperationName = "Edit";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";
        public static readonly string RejectOperationName = "Reject";

        public static readonly string AdministratorsRole = "Admin";
        public static readonly string ManagerRole = "Managers";
        public static readonly string EmployeeRole = "Employee";
        
    }
}
