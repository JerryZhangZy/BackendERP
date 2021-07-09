using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DigitBridge.Base.Utility;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public static class TableRepositoryExtensions
    {
        public static IList<TEntity> ClearMetaData<TEntity>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.ClearMetaData<TEntity, long>();

        public static IList<TEntity> ClearMetaData<TEntity, TId>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            tableRepositories?.ForEach(i => i?.ClearMetaData());
            return tableRepositories;
        }

        public static IList<TEntity> SetAllowNull<TEntity>(this IList<TEntity> lst, bool allowNull)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.SetAllowNull<TEntity, long>(allowNull);

        public static IList<TEntity> SetAllowNull<TEntity, TId>(this IList<TEntity> lst, bool allowNull) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            tableRepositories?.ForEach(i => i?.SetAllowNull(allowNull));
            return tableRepositories;
        }

        public static IList<TEntity> SetDataBaseFactory<TEntity>(this IList<TEntity> lst, IDataBaseFactory dbFactory)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.SetDataBaseFactory<TEntity, long>(dbFactory);

        public static IList<TEntity> SetDataBaseFactory<TEntity, TId>(this IList<TEntity> lst, IDataBaseFactory dbFactory) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            tableRepositories?.ForEach(i => i?.SetDataBaseFactory(dbFactory));
            return tableRepositories;
        }

        public static IList<TEntity> ConvertDbFieldsToData<TEntity>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.ConvertDbFieldsToData<TEntity, long>();

        public static IList<TEntity> ConvertDbFieldsToData<TEntity, TId>(this IList<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var tableRepositories = lst.ToList();
            tableRepositories?.ForEach(i => i?.ConvertDbFieldsToData());
            return tableRepositories;
        }

        public static bool Save<TEntity>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.Save<TEntity, long>();

        public static bool Save<TEntity, TId>(this IList<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = true;
            foreach (var tableRepository in lst?.Where(x => x != null))
            {
                var rtn1 = tableRepository.Save();
                rtn = rtn1 && rtn;
            }
            return rtn;
        }

        public static int Delete<TEntity>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.Delete<TEntity, long>();

        public static int Delete<TEntity, TId>(this IList<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = 0;
            foreach (var tableRepository in lst?.Where(x => x != null))
                rtn += tableRepository.Delete();
            return rtn;
        }

        public static async Task<bool> SaveAsync<TEntity>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => await lst.SaveAsync<TEntity, long>().ConfigureAwait(false);

        public static async Task<bool> SaveAsync<TEntity, TId>(this IList<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = true;
            foreach (var tableRepository in lst?.Where(x => x != null))
            {
                var rtn1 = await tableRepository.SaveAsync().ConfigureAwait(false);
                rtn = rtn1 && rtn;
            }
            return rtn;
        }

        public static async Task<int> DeleteAsync<TEntity>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => await lst.DeleteAsync<TEntity, long>().ConfigureAwait(false);

        public static async Task<int> DeleteAsync<TEntity, TId>(this IList<TEntity> lst) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var rtn = 0;
            foreach (var tableRepository in lst?.Where(x => x != null))
                rtn += await tableRepository.DeleteAsync().ConfigureAwait(false);
            return rtn;
        }

        public static TEntity FindByRowNum<TEntity>(this IList<TEntity> lst, long rowNum)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindByRowNum<TEntity, long>(rowNum);

        public static TEntity FindByRowNum<TEntity, TId>(this IList<TEntity> lst, long rowNum) where TEntity : TableRepository<TEntity, TId>, new()
        {
            return (lst == null || rowNum <= 0) ? null : lst.FirstOrDefault(item => item.RowNum == rowNum);
        }

        public static TEntity FindById<TEntity>(this IList<TEntity> lst, string uniqueId)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindById<TEntity, long>(uniqueId);

        public static TEntity FindById<TEntity, TId>(this IList<TEntity> lst, string uniqueId) where TEntity : TableRepository<TEntity, TId>, new()
        {
            return (lst == null || string.IsNullOrEmpty(uniqueId))
                ? null
                : lst.FirstOrDefault(item => item.UniqueId == uniqueId);
        }

        public static TEntity FindByObject<TEntity>(this IList<TEntity> lst, TEntity obj)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindByObject<TEntity, long>(obj);

        public static TEntity FindByObject<TEntity, TId>(this IList<TEntity> lst, TEntity obj) where TEntity : TableRepository<TEntity, TId>, new()
        {
            if (obj.RowNum <= 0 && string.IsNullOrEmpty(obj.UniqueId)) return null;

            if (lst.Count <= 0) return null;

            var index = lst.IndexOf(obj);
            if (index >= 0)
                return lst[index];

            return lst.FirstOrDefault(item =>
                (obj.RowNum > 0 && item.RowNum == obj.RowNum) ||
                (!string.IsNullOrEmpty(obj.UniqueId) && item.UniqueId == obj.UniqueId));
        }

        public static IList<TEntity> CopyFrom<TEntity>(this IList<TEntity> lst, IList<TEntity> lstFrom, IEnumerable<string> ignoreColumns)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst?.CopyFrom<TEntity, long>(lstFrom, ignoreColumns);

        public static IList<TEntity> CopyFrom<TEntity, TId>(this IList<TEntity> lst, IList<TEntity> lstFrom, IEnumerable<string> ignoreColumns)
            where TEntity : TableRepository<TEntity, TId>, new()
        {
            // if copy multiple items, need copy by same order of from list
            var lstOrig = new List<TEntity>(lst);
            lst.Clear();
            foreach (TEntity l in lstFrom)
            {
                if (l == null) continue;
                var o = l.RowNum > 0
                    ? lstOrig.FindByRowNum<TEntity, TId>(l.RowNum)
                    : lstOrig.FindByObject<TEntity, TId>(l);
                if (o is null)
                    o = l;
                else
                    lstOrig.Remove(o);

                o.SetAllowNull(false);
                lst.Add(o);
                o.CopyFrom(l, ignoreColumns);
            }
            return lstOrig;
        }

        public static IList<TEntity> CopyFrom<TEntity, TId>(this IList<TEntity> lst, TEntity obj, IEnumerable<string> ignoreColumns)
            where TEntity : TableRepository<TEntity, TId>, new()
        {
            if (obj == null) return lst;
            var o = obj.RowNum > 0
                ? lst.FindByRowNum<TEntity, TId>(obj.RowNum)
                : lst.FindByObject<TEntity, TId>(obj);
            if (o == null)
            {
                o = new TEntity().SetAllowNull(false);
                lst.Add(o);
            }
            o.CopyFrom(obj, ignoreColumns);
            return lst;
        }

        public static IList<TEntity> CopyFrom<TEntity>(this IList<TEntity> lst, IList<TEntity> lstFrom)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst?.CopyFrom<TEntity, long>(lstFrom);

        public static IList<TEntity> CopyFrom<TEntity, TId>(this IList<TEntity> lst, IList<TEntity> lstFrom) where TEntity : TableRepository<TEntity, TId>, new()
        {
            // if copy multiple items, need copy by same order of from list
            var lstOrig = new List<TEntity>(lst);
            lst.Clear();
            foreach (TEntity l in lstFrom)
            {
                if (l == null) continue;
                var o = l.RowNum > 0
                    ? lstOrig.FindByRowNum<TEntity, TId>(l.RowNum)
                    : lstOrig.FindByObject<TEntity, TId>(l);
                if (o is null)
                    o = l;
                else
                    lstOrig.Remove(o);

                o.SetAllowNull(false);
                lst.Add(o);
                o.CopyFrom(l);
            }
            return lstOrig;
        }

        public static IList<TEntity> CopyFrom<TEntity, TId>(this IList<TEntity> lst, TEntity obj) where TEntity : TableRepository<TEntity, TId>, new()
        {
            if (obj == null) return lst;
            var o = obj.RowNum > 0
                ? lst.FindByRowNum<TEntity, TId>(obj.RowNum)
                : lst.FindByObject<TEntity, TId>(obj);
            if (o == null)
            {
                o = new TEntity().SetAllowNull(false);
                lst.Add(o);
            }
            o.CopyFrom(obj);
            return lst;
        }

        public static TEntity AddOrReplace<TEntity>(this IList<TEntity> lst, TEntity obj)
            where TEntity : TableRepository<TEntity, long>, new()
        {
            var exist = lst.FindByObject(obj);
            if (exist is null)
                lst.Add(obj);
            else
            {
                var index = lst.IndexOf(exist);
                if (index >= 0)
                    lst[index] = obj;
            }
            return obj;
        }

        public static IList<TEntity> FindNotExistsByRowNum<TEntity>(this IList<TEntity> lst,
            IList<TEntity> lstMatch)
            where TEntity : TableRepository<TEntity, long>, new()
        {
            return lst.Where(x => x.RowNum > 0 && !lstMatch.Any(m => m.RowNum == x.RowNum)).ToList();
        }

        public static TEntity FindBy<TEntity>(this IList<TEntity> lst, Func<TEntity, bool> predicate)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.FindBy<TEntity, long>(predicate);

        public static TEntity FindBy<TEntity, TId>(this IList<TEntity> lst, Func<TEntity, bool> predicate) where TEntity : TableRepository<TEntity, TId>, new()
        {
            return (lst == null) ? null : (TEntity)lst.FirstOrDefault(predicate);
        }

        public static TEntity AddOrReplaceBy<TEntity>(this IList<TEntity> lst, TEntity obj, Func<TEntity, bool> predicate)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.AddOrReplaceBy<TEntity, long>(obj, predicate);

        public static TEntity AddOrReplaceBy<TEntity, TId>(this IList<TEntity> lst, TEntity obj, Func<TEntity, bool> predicate) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var exist = lst.FindBy<TEntity, TId>(predicate);
            if (exist is null)
                lst.Add(obj);
            else
            {
                var index = lst.IndexOf(exist);
                if (index >= 0)
                    lst[index] = obj;
            }
            return obj;
        }

        public static TEntity RemoveObject<TEntity>(this IList<TEntity> lst, TEntity obj)
            where TEntity : TableRepository<TEntity, long>, new()
        {
            var exist = lst.FindByObject(obj);
            if (exist != null)
                lst.Remove(exist);
            return exist;
        }
        public static IList<TEntity> RemoveBy<TEntity>(this IList<TEntity> lst, Func<TEntity, bool> predicate)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.RemoveBy<TEntity, long>(predicate);

        public static IList<TEntity> RemoveBy<TEntity, TId>(this IList<TEntity> lst, Func<TEntity, bool> predicate) where TEntity : TableRepository<TEntity, TId>, new()
        {
            var removeList = lst.Where(predicate).ToList();
            foreach (var remove in removeList)
                lst.Remove(remove);
            return removeList;
        }
        public static IList<TEntity> RemoveEmpty<TEntity>(this IList<TEntity> lst)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.RemoveBy<TEntity, long>(x => x.IsEmpty);

        public static bool EqualsList<TEntity>(this IList<TEntity> lst, IList<TEntity> listOther)
            where TEntity : TableRepository<TEntity, long>, new()
            => lst.EqualsList<TEntity, long>(listOther);

        public static bool EqualsList<TEntity, TId>(this IList<TEntity> lst, IList<TEntity> listOther) where TEntity : TableRepository<TEntity, TId>, new()
        {
            if (lst.Count != listOther.Count) return false;
            for (var i = 0; i < lst.Count; i++)
            {
                if (!lst[i].Equals(listOther[i]))
                    return false;
            }
            return true;
        }

    }
}