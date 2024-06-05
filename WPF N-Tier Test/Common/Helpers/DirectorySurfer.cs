using System.IO;

namespace WPF_N_Tier_Test.Modules.Helpers
{
    internal class DirectorySurfer
    {
        public static string GetInstallLocation()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "") + "Local//Programs//AnisDjaidja//WPF_N_Tier_Test//";
            if (!Directory.Exists(path)) { var d = Directory.CreateDirectory(path); d.Attributes = FileAttributes.Normal; }
            return path;
        }
        public static string GetStorage()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "") + "Local//Programs//AnisDjaidja//WPF_N_Tier_Test//Local//DB//";
            if (!Directory.Exists(path)) { var d = Directory.CreateDirectory(path); d.Attributes = FileAttributes.Normal; }
            return path;
        }        
        public static string GetLicenseDir()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "") + "Local//Programs//AnisDjaidja//WPF_N_Tier_Test//Common//License//";
            if (!Directory.Exists(path)) { var d = Directory.CreateDirectory(path); d.Attributes = FileAttributes.Normal; }
            return path;
        }
        public static string GetCategoryDBFile()
        {
            return GetStorage() + "Category.tjrdb";
        }
        public static string GetChargesDBFile()
        {
            return GetStorage() + "Charges.tjrdb";
        }
        public static string GetStaffDBFile()
        {
            return GetStorage() + "Staff.tjrdb";
        }
        public static string GetSaleDBFile()
        {
            return GetStorage() + "Sales.tjrdb";
        }
        public static string GetStockDBFile()
        {
            return GetStorage() + "Stock.tjrdb";
        }
        public static string GetBusinessDBFile()
        {
            return GetStorage() + "Business.tjrdb";
        }
        public static string GetCustomerDBFile()
        {
            return GetStorage() + "Customers.tjrdb";
        }
        public static string GetSupplierDBFile()
        {
            return GetStorage() + "Suppliers.tjrdb";
        }
        public static bool CheckIntegration()
        {
            List<string> dirs = new List<string>
            {
                "writercore","webviewruntime","Th_wr-fonts",
            };
            foreach(string dir in dirs)
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "") + "Local//Thesis Writer//" + dir + "//"))
                {
                    return false;
                }
            }
            return true;
        }
        public static string GetAppConfig()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//WPF_N_Tier_Test" + "//config.tajir";
        }
        public static string GetAppDocDir()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//WPF_N_Tier_Test";
        }
        public static string GetLicenseCertaficate()
        {
            return GetLicenseDir() + "license.tjlc";
        }
        public static string GetApplicationLocal()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming", "") + "Local//WPF_N_Tier_Test//";
            if (!Directory.Exists(path)) { var d = Directory.CreateDirectory(path); d.Attributes = FileAttributes.System | FileAttributes.Hidden; }
            return path;
        }

        public static string RandomString(int length)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
