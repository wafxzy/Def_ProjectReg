using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonReg.BLL.Options
{
    public static class HashPasswordOptions
    {
        public const int SALT = 128 / 8;
        public const int NUM_BYTES_REQUESTED = 256 / 8;
        public const int ITERATION_COUNT = 10000;
    }

}
