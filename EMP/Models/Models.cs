using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMP.Models
{
    class Models
    {

    }
    public class LoginModels
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
    public class ErrorMsg
    {
        public string Message { get; set; }
    }

    public class GetModels
    {
        public int OrganizationId { get; set; }
        public int UserId { get; set; }

        public string CDate { get; set; }

    }
    public class BreakMaster
    {
        public int Id { get; set; } //(int, not null)
        public string Name { get; set; } //(varchar(100), not null)
        public int Max_Break_Time { get; set; } //(int, not null)
        public bool Active { get; set; } //(bit, not null)
        public int OrganizationId { get; set; } //(int, not null)

        public static implicit operator List<object>(BreakMaster v)
        {
            throw new NotImplementedException();
        }
    }
    public class UserAttendanceModel
    {
        public int OrganizationId { get; set; } //(int, not null)
        public int Id { get; set; } //(int, not null)
        public int UserId { get; set; } //(int, not null)
        public string AttendanceDate { get; set; } //(datetime, not null)
        public string Start_Time { get; set; } //(datetime,  null)
        public string End_Time { get; set; } //(datetime,  null)
        public string Total_Time { get; set; } //(datetime,  null)
        public string Late_Time { get; set; } //(datetime,  null)
        public int Status { get; set; } //(int, not null)
    }

    public class UserBreakModel
    {
        public int OrganizationId { get; set; } //(int, not null)
        public int BreakEntryId { get; set; } //(int, not null)
        public int Id { get; set; } //(int, not null)
        public int UserId { get; set; } //(int, not null)
        public string BreakDate { get; set; } //(datetime, not null)
        public string Start_Time { get; set; } //(datetime, not null)
        public string End_Time { get; set; } //(datetime, not null)
        public int Status { get; set; } //(int, not null)
    }

    public class loginresult
    {
        public string token { get; set; } //(var
        public Users user { get; set; }
        public string message { get; set; }
    }
    public class Users
    {
        public int Id { get; set; } //(int, not null)
        public string First_Name { get; set; } //(varchar(100), not null)
        public string Last_Name { get; set; } //(varchar(100), null)
        public string Email { get; set; } //(varchar(100), not null)
        public DateTime DOB { get; set; } //(date, null)
        public DateTime DOJ { get; set; } //(date, null)
        public string Phone { get; set; } //(varchar(100), not null)
        public string UsersName { get; set; } //(varchar(100), not null)
        public string Password { get; set; } //(varchar(100), not null)
        public string Gender { get; set; } //(varchar(100), null)
        public int OrganizationId { get; set; } //(int, not null)
        public int RoleId { get; set; } //(int, not null)
        public int DesignationId { get; set; } //(int, not null)
        public int TeamId { get; set; } //(int, not null)
        public bool Active { get; set; } //(bit, not null)
        public string RoleName { get; set; } //(varchar(100), not null)
        public string AccessLevel { get; set; } //(varchar(100), not null)
        public string DesignationName { get; set; } //(varchar(100), not null)
        public string TeamName { get; set; } //(varchar(100), not null)
    }

    public class URL
    {
        string url;
        string title;
        string browser;
        Nullable<DateTime> lastvisit;
        public URL(string url, string title, string browser, Nullable<DateTime> lastvisit)
        {
            this.url = url;
            this.title = title;
            this.browser = browser;
            this.lastvisit = lastvisit;
        }

        public string getData()
        {
            return browser + " - " + title + " - " + url;
        }
    }
}
