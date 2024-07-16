using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP
{
    public static class Message
    {
        #region Common
        public static string Error = "Oops! Something want wrong please try again...";
        public static string Success = "Success";
        public static string NotFound = "Not found";
        public static string AlreadyExists = "User already exists!";
        public static string UnAuthorized = "User is unauthorized";
        public static string CredentialFailed = "User creation failed! Please check user details and try again.";
        public static string PasswordReset = "Passsword reset successfully";
        public static string CreateSuccess = " Update Successfully..";
        public static string DeleteSuccess = " Deleted Successfully..";
        #endregion

        #region Login
        public static string LoginSuccess = "Login Succesfully";
        public static string LoginError = "Invalid UserName And Password";
        #endregion

        #region Company
        public static string CompanySuccess = "Register company details successfully";
        public static string CompanyCount = "Company Datas Found";
        public static string CompanyError = "Oops! something wrong when process with company details";
        public static string CompanyUpdateSuccess = "Update company details successfully";
        #endregion

        #region Email
        public static string ForgotSuccessSend = "Forgot password email send successfully on your email.";
        #endregion

        #region Emmployee
        public static string EmployeeCreated = "Employee created successfully";
        public static string EmployeeCreatedError = "Oops! something wrong employee not created";
        public static string EmployeeUpdated = "Employee updated successfully";
        public static string EmployeeUpdatedError = "Oops! something wrong employee not updated";
        public static string EmployeeDeleted = "Employee deleted successfully";
        public static string EmployeeDeletedError = "Oops! something wrong employee not deleted";

        #endregion
        #region Cashier
        public static string CashierCreate =  "Cashier Update Successfully..";
        public static string CashierDelete = "Cashier Deleted Successfully..";
        #endregion
        #region Customer
        public static string CustomerCreated = "Customer created successfully";
        public static string CustomerCreatedError = "Oops! something wrong Customer not created";
        public static string CustomerDeleted = "Customer deleted successfully";
        public static string CustomerUpdatedError = "Oops! something wrong Customer not updated";
        public static string CustomerUpdated = "Customer updated successfully";
        public static string CustomerDeletedError = "Oops! something wrong Customer not deleted";
        #endregion

        #region Appuser
        public static string AppuserCreated = "Appuser created successfully";
        public static string AppuserCreatedError = "Oops! something wrong Appuser not created";
        public static string AppuserUpdated = "Appuser updated successfully";
        public static string AppuserUpdatedError = "Oops! something wrong Appuser not updated";
        public static string AppuserDeleted = "Appuser deleted successfully";
        public static string AppuserDeletedError = "Oops! something wrong Appuser not deleted";
        #endregion
    }
}
