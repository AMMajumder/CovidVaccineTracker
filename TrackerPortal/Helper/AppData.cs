using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackerPortal.Helper
{
    public class AppData
    {
        public static IConfiguration configuration { get; set; }
    }
}
