using System;

namespace DigitBridge.CommerceCentral.YoPoco
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class FieldAttribute : Attribute
	{
		private readonly string	_FieldName;

	    public					FieldAttribute(string FieldName) 
		{
			_FieldName = FieldName;
			IsSerialized = false;
		}

		public string			FieldName { get { return _FieldName;  } }
	    public bool             IsSerialized { get; set; }
	    public string           Serializer { get; set; }
	}

    public interface ISerializer
    {
        object                  Serialize(object objToSerialize);
        object                  Deserialize(Type type, object objToDeserialize);
    }
}