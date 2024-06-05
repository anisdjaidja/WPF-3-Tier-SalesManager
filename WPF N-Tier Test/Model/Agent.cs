using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WPF_N_Tier_Test_Data_Access.DTOs;

namespace WPF_N_Tier_Test.Model
{
    public class Agent: User
    {
        public string SecurePassword {
            get { return Hash(Password); }
            set { if(!string.IsNullOrEmpty(value))
                    Password = value;
                }
        }
        private static string Hash(string UnsecurePWD)
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
