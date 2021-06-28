using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface IStructureRepository<TEntity> where TEntity : StructureRepository<TEntity>, new()
    {
        IDataBaseFactory dbFactory { get; }
        IDatabase db { get; }
        bool AllowNull { get; }
        bool IsNew { get; }

        TEntity SetAllowNull(bool allowNull);
        TEntity SetDataBaseFactory(IDataBaseFactory dbFactory);
        ITransaction GetTransaction();
    }
}