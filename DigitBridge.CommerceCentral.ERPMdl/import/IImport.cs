using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    interface IImport<T> where T : class
    {
        Task PrePareData(T dto);
    }
}
