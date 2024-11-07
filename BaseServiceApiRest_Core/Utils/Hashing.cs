using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServiceApiRest_Core.Utils
{
    public class Hashing
    {
        public static string Key = "0e66f487e59b30cf3035729db18c4825faf4ce23d8da54533cf59a92534e0483";

        public static string UseArgon2(string value)
        {
            return Argon2.Hash(value);
        }

        public static bool VerifyArgon2(string original, string value)
        {
            return Argon2.Verify(original, value);
        }
    }
}
