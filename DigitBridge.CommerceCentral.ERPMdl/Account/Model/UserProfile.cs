using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class UserProfile
    {
        /// <summary>
        /// MasterAccountNum
        /// </summary>
        public int MasterAccountNum { get; set; }

        /// <summary>
        /// ProfileNum
        /// </summary>
        public int ProfileNum { get; set; }

        /// <summary>
        /// company name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// is use backdoor
        /// </summary>
        public string IsBackdoor { get; set; }

        /// <summary>
        /// backdoor email
        /// </summary>
        public string BackdoorEmail { get; set; }

        /// <summary>
        /// user permissions list
        /// </summary>
        public List<UserPermission> Permissions { get; set; }

        public LoginProfileModel ToLoginProfileModel()
        {
            return new LoginProfileModel()
            {
                MasterAccountNum = this.MasterAccountNum,
                ProfileNum = this.ProfileNum,
                DisplayName = this.DisplayName,
                Email = this.Email,
            };
        }
    }

    public static class UserProfileExtension
    {
        public static IList<LoginProfileModel> ToLoginProfileModel(this IList<UserProfile> lst)
            => (lst == null || lst.Count == 0)
                ? null
                : lst.Select(x => x.ToLoginProfileModel()).ToList();
    }
}
