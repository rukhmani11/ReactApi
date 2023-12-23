using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoV.Core.Enum
{
    public static class CustomConstants
    {
        public const string dateFormatToDisplay = "dd/MM/yyyy h:mm:ss tt";
        public const string durationFormat = @"hh\:mm";
        public const string durationFormatWithSeconds = @"hh\:mm\:ss";
        public const string themeLightHexCode = "#e3f2fd";
        public const string themeDarkHexCode = "#1976d2";
    }
    public static class RoleEnum
    {
        public const string CompanyAdmin = "CompanyAdmin";
        public const string SiteAdmin = "SiteAdmin";
        public const string CompanyDomainUser = "CompanyDomainUser";
        public const string CompanyUser = "CompanyUser";
    }
}
