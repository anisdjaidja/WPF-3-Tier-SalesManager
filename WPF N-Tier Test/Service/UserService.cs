using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using WPF_N_Tier_Test_Data_Access.DataAccess;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Service
{
    public class UserService
    {
        private SalesContext dbContext;

        public UserService(SalesDesignTimeContextFactory contextFactory)
        {
            this.dbContext = contextFactory.CreateDbContext(null);
            Initialize();
        }
        public async void Initialize()
        {
            if (dbContext.Users.Count() == 0)
            {
                string file = File.ReadAllText(@"E:\\Current Dev\\WPF N-Tier Test\\WPF N-Tier Test\\Service\\Seeds\\users.json");
                var ppl = JsonConvert.DeserializeObject<User[]>(file);
                foreach(User user in ppl)
                {
                    string unsecurepwd = user.Password;
                    user.Password = Hash(unsecurepwd);
                }
                await dbContext.AddRangeAsync(ppl!);
                await dbContext.SaveChangesAsync();
                ReportSuccess("Seeded Users Table with fake data");
            }
        }

        internal async Task<bool> Login(string name, string pwd)
        {
            return dbContext.Users.Where(x => x.UserName == name && x.Password == Hash(pwd)).FirstOrDefault() != null;
        }
        public static string Hash(string UnsecurePWD)
        {
            using (var sha = SHA512.Create())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(UnsecurePWD + "seed");
                byte[] hashBytes = sha.ComputeHash(textBytes);
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    }
}
