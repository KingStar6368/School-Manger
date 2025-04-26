using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Data.Identity
{
    public static class UserSession
    {
        static UserSession()
        {
            UfovList = new List<UserAccess>();
            AccessCostCenter = new List<int>();
            UserID = 1;
        }

        public static int UserID { get; set; }
        public static string UserName { get; set; }
        public static string Name { get; set; }
        public static int ActiveFiscalYearId { get; set; }
        public static string ActiveFiscalYearName {  get; set; }
        public static List<int> AccessCostCenter { get; set; }
        public static string AccessCostCenterStr { get; set; }
        public static string DataBaseName { get; set; }
        public static List<UserAccess> UfovList { get; set; }
    }

    public class UserAccess
    {
        public string FormName { get; set; }
        public string OprName { get; set; }
    }
}
