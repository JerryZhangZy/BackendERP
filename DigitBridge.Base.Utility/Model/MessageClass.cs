using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DigitBridge.Base.Utility
{
    public enum MessageLevel
    {   //
        // Summary:
        //     Debug.
        Debug = 1,
        //
        // Summary:
        //     Informational.
        Info = 2,
        //
        // Summary:
        //     Warning.
        Warning = 3,
        //
        // Summary:
        //     Error.
        Error = 4,
        //
        // Summary:
        //     Fatal.
        Fatal = 5
    }

    [Serializable()]
    public class MessageClass
    {
        /// <summary>
        /// Error code.
        /// Optional,
        /// Default value is Empty.
        /// </summary>
        [DataMember(Name = "level")]
        public MessageLevel Level { get; set; }

        /// <summary>
        /// Error code.
        /// Optional,
        /// Default value is Empty.
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Error message.
        /// Optional,
        /// Default value is Empty.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        public MessageClass() { }
        public MessageClass(string message, MessageLevel level = MessageLevel.Error, string code = null)
        {
            Code = code;
            Level = level;
            Message = message;
        }
    }

    public static class MessageClassExtension
    {
        public static MessageClass FindMessage(this IEnumerable<MessageClass> lst, string message) =>
            (lst == null) ? null : lst.FirstOrDefault(x => x.Message.EqualsIgnoreSpace(message));

        public static IEnumerable<MessageClass> FindLevel(this IEnumerable<MessageClass> lst, MessageLevel level) =>
            lst?.Where(x => x.Level == level);

        public static IList<MessageClass> Add(this IList<MessageClass> lst, MessageClass obj)
        {
            if (lst == null)
                lst = new List<MessageClass>();
            lst.Add(obj);
            return lst;
        }

        public static IList<MessageClass> Add(this IList<MessageClass> lst, string message, MessageLevel level, string code = null)
        {
            if (lst == null)
                lst = new List<MessageClass>();
            lst.Add(new MessageClass(message, level, code));
            return lst;
        }
        public static IList<MessageClass> Add(this IList<MessageClass> lst, IList<MessageClass> messages)
        {
            if (lst == null)
                lst = new List<MessageClass>();
            lst = lst.Concat(messages).ToList();
            return lst;
        }

        public static bool Remove(this IList<MessageClass> lst, MessageClass obj) =>
            lst.Remove(obj);

        public static IList<MessageClass> RemoveBy(this IList<MessageClass> lst, MessageLevel level) =>
            lst.RemoveBy(x => x.Level == level);

        public static IList<MessageClass> RemoveBy(this IList<MessageClass> lst, Func<MessageClass, bool> predicate)
        {
            var removeList = lst.Where(predicate).ToList();
            foreach (var remove in removeList)
                lst.Remove(remove);
            return removeList;
        }

        public static IList<MessageClass> RemoveEmpty(this IList<MessageClass> lst) =>
            lst.RemoveBy(x => string.IsNullOrEmpty(x.Message));


        public static IList<MessageClass> AddInfo(this IList<MessageClass> lst, string message, string code = null) =>
            lst?.Add(message, MessageLevel.Info, code);
        public static IList<MessageClass> AddWarning(this IList<MessageClass> lst, string message, string code = null) =>
            lst?.Add(message, MessageLevel.Warning, code);
        public static IList<MessageClass> AddError(this IList<MessageClass> lst, string message, string code = null) =>
            lst?.Add(message, MessageLevel.Error, code);
        public static IList<MessageClass> AddFatal(this IList<MessageClass> lst, string message, string code = null) =>
            lst?.Add(message, MessageLevel.Fatal, code);
        public static IList<MessageClass> AddDebug(this IList<MessageClass> lst, string message, string code = null) =>
            lst?.Add(message, MessageLevel.Debug, code);

    }
}