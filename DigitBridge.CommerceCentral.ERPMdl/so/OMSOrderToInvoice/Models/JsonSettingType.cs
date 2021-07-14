using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
namespace GhpIntegration.OrderImportApmMdl.Models
{
    public class CommerceCentralChannelAccountType
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string AccountName { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string AccountId { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "{0} MaximumLength is 2 characters")]
        public string BranchId { get; set; }
    }
    public class CommerceCentralSettingType
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string CentralChannel { get; set; }

        public string CentralOrderView { get; set; }
        public List<CommerceCentralChannelAccountType> CentralChannelAccounts { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public string DatabaseSourceId { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0} MaximumLength is 10, MinimumLength is 2")]
        public string SourceId { get; set; }

        [Required]
        [CustomValidation(typeof(CustomerValidation), nameof(CustomerValidation.TargetApmValidate))]
        public string TargetAPM { get; set; }
    }
    public class CustomerValidation
    {
        public static ValidationResult TargetApmValidate(string targetApm)
        {
            return targetApm == "ASA" || targetApm == "Vibes" ? ValidationResult.Success :
                new ValidationResult("TargetAPM must be \"ASA\" or \"Vibes\" ");
        }
        public static ValidationResult RawOrderGetOrderMethodValidate(string getMethod)
        {
            return getMethod == "FTP" || getMethod == "API" ? ValidationResult.Success :
                new ValidationResult("RawOrder GetMethod must be \"FTP\" or \"API\" ");
        }
    }
    public class DatabaseConnectionType
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string Server { get; set; }
        public bool NeedPassword { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
    public class ImportOrderChannelSettingType
    {
        [Required]
        public string Channel { get; set; }

        [Required]
        public string OrderView { get; set; }

        [Required]
        public string DatabaseSourceId { get; set; }

        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0} MaximumLength is 10, MinimumLength is 2")]
        public string SourceId { get; set; }

        public List<CommerceCentralChannelAccountType> ChannelAccounts { get; set; }

    }
    public class ImportOrderSettingType
    {
        public bool Disable = false;
        [Required(ErrorMessage = "{0} is required.")]
        public string RawOrderSource { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string RawOrderDb { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public List<ImportOrderChannelSettingType> Channels { get; set; }

    }

    public class AutoImportSettingType
    {
        public bool Disable = false;
        [Required]
        public string DbSource { get; set; }
        public int Sequence = 0;
        public DatabaseConnectionType SourceDatabaseConn { get; set; }
        public List<ImportOrderSettingType> ImportOrders { get; set; }
    }

    public class FtpSettingType
    {

        [Required]
        public string FtpServer { get; set; }
        [Required]

        public int FtpPort { get; set; }
        [Required]
        public string FtpExportPath { get; set; }
        public List<FtpAccountSettingType> FtpAccounts { get; set; }

    }
    public class FtpAccountSettingType
    {
        public bool Disable = false;
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string FtpAccount { get; set; }
        [Required]
        public string FtpPassword { get; set; }
    }

    public class RawOrderAccountSettingType
    {
        public bool Disable = false;
        [Required]
        public string Channel { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string RawOrderDb { get; set; }

        [Required]
        [CustomValidation(typeof(CustomerValidation), nameof(CustomerValidation.RawOrderGetOrderMethodValidate))]
        public string GetOrderMethod { get; set; }

        public FtpSettingType FtpSetting { get; set; }
        public List<CommerceCentralChannelAccountType> Accounts { get; set; }
    }

    public class RawOrderChannelSettingType
    {
        public DatabaseConnectionType TargetDatabaseConn { get; set; }
        public List<RawOrderAccountSettingType> Channels { get; set; }
    }
}
