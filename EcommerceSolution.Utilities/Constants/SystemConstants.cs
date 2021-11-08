using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Utilities.Constants
{
    public class SystemConstants
    {
        public const string MainConectionString = "EcommerceDb";
        public class AppSettings
        {
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
            public const string LocalAddress = "LocalAddress";
        }

        public class ProductSettings
        {
            public const int NumberOfFeaturedProducts = 10;
            public const int NumberOfLatestProducts = 10;
            public const int NumberOfBestSellerProducts = 8;
        }

        public class ProductConstants
        {
            public const string NA = "N/A";
        }
    }
}