using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Model
{
    public class Command
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }
        public Command(int masterAccountNum, int profileNum)
        {
            this.MasterAccountNum = masterAccountNum;
            this.ProfileNum = profileNum;
        } 
    }
}
