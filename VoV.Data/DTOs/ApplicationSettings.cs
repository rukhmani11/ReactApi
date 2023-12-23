

using Microsoft.EntityFrameworkCore.Migrations;

namespace VoV.Data.DTOs
{
    public class ApplicationSettings
    {
        public string JwtValidAudience { get; set; }
        public string JwtValidIssuer { get; set; }
        public string JwtSecret { get; set; }

        public string MailFrom { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string DocStoreFolderPath { get; set; }
    }
    //If multiple context then (VoV is multi context)
    //add-migration initial -context VoVDbContext
    //Update-Database -context VoVDbContext
    //remove-migration initial -context VoVDbContext
    //drop-database

    //for single context
    //add-migration DefaultConstraint
    //Update-Database 
    //remove-migration initial 

    //Scaffolding Command 
    //Scaffold-DbContext "Data Source=SENTIENTPC-112\SQLEXPRESS2014;Initial Catalog=VoV;Persist Security Info=True;User ID=sa;Password=Sentient@123" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context SampleTestDBContext -force -nopluralize
}
