using System;

using System.Configuration;

namespace Dodo.Core.Common.Helpers
{
    public class ConfigCultureHelper
    {
        public static String CurrentThreadCultureInfo
        {
            get { return ConfigurationManager.AppSettings["CurrentThreadCultureInfo"] ?? ConfigurationManager.AppSettings["CurrentCultureInfo"]; }
        }
    }
}