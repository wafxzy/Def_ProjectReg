using CommonReg.BLL.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.BLL.Helpers
{
    public static class HashPasswordHelper
    {
        public static string CalculatePasswordHash(string password, Guid salt = default)
        {
            Byte[] saltInBytes = salt.ToByteArray();
            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltInBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: HashPasswordOptions.ITERATION_COUNT,
                numBytesRequested: HashPasswordOptions.NUM_BYTES_REQUESTED));
            return passwordHash;
        }
    }
}
