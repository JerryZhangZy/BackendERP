using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public interface IServiceManager<TPayload>
        where TPayload : IPayload
    {
        Task<byte[]> ExportAsync(TPayload payload);
        byte[] Export(TPayload payload);

        void Import(TPayload payload, IFormFileCollection files);
        Task ImportAsync(TPayload payload, IFormFileCollection files);
    }
}
