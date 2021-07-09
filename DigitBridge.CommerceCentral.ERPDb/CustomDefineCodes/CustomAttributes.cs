using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    [Serializable]
    public class CustomAttributes : ICloneable, IEquatable<CustomAttributes>
    {
        protected IDictionary<string, CustomAttributeProfile> _customAttributeProfiles;
        protected Dictionary<string, object> _customAttributeValues;
        protected string _attributeFor;

        public IDictionary<string, CustomAttributeProfile> Profiles => _customAttributeProfiles;
        public Dictionary<string, object> Values => _customAttributeValues;
        public string AttributeFor => _attributeFor;

        #region DataBase
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore, IgnoreCompare]
        protected IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
            return;
        }

        #endregion DataBase

        public CustomAttributes(IDataBaseFactory dataBaseFactory, string attributeFor)
        {
            _dbFactory = dataBaseFactory;
            _attributeFor = attributeFor;
            _customAttributeProfiles = new Dictionary<string, CustomAttributeProfile>();
            _customAttributeValues = new Dictionary<string, object>();
        }
        public CustomAttributes(IDataBaseFactory dataBaseFactory, string type, IDictionary<string, CustomAttributeProfile> customAttributeProfiles, Dictionary<string, object> customAttributeValues)
        {
            _dbFactory = dataBaseFactory;
            _attributeFor = type;
            _customAttributeProfiles = customAttributeProfiles;
            _customAttributeValues = customAttributeValues;
        }

        public void LoadFromValueString(string json)
        {
            LoadProfiles();
            _customAttributeValues = json.ToDicationary();
        }
        public void LoadFromJObject(JObject json)
        {
            LoadProfiles();
            _customAttributeValues = json.ToDicationary();
        }
        public void LoadProfiles()
        {
            if (_customAttributeProfiles != null && _customAttributeProfiles.Count > 0) return;
            var lstProfiles = dbFactory.GetFromCache<List<CustomAttributeProfile>>(
                $"CustomAttributeProfile: AttributeFor: {_attributeFor}",
                () => dbFactory.Find<CustomAttributeProfile>("WHERE AttributeFor = @0 ORDER BY Seq", _attributeFor).ToList()
            );

            _customAttributeProfiles = new Dictionary<string, CustomAttributeProfile>();
            if (lstProfiles != null && lstProfiles.Count > 0)
            {
                foreach (var profile in lstProfiles)
                {
                    if (profile is null || string.IsNullOrEmpty(profile.AttributeName)) continue;
                    _customAttributeProfiles.Add(profile.AttributeName, profile);
                }
            }
        }
        public CustomAttributes Clear() => ClearValues();

        public CustomAttributes CopyFrom(CustomAttributes other)
        {
            _customAttributeValues = other.Values.Clone();
            return this;
        }

        public object Clone() => new CustomAttributes(_dbFactory, _attributeFor, _customAttributeProfiles, _customAttributeValues.Clone());

        public bool Equals(CustomAttributes other) => other is CustomAttributes && _customAttributeValues.IsEqualTo(other.Values);

        public string ToValueString() => (_customAttributeValues is null) ? string.Empty : _customAttributeValues.ToJsonString();

        public CustomAttributes LoadJson(JObject json)
        {
            LoadProfiles();
            LoadFromJObject((JObject)json["values"]);
            return this;
        }
        public JObject ToJson()
        {
            return new JObject()
            {
                { "schema", JObject.FromObject(_customAttributeProfiles) },
                { "values", _customAttributeValues.ToJObject() } };
        }

        #region CustomAttributeProfile define list

        public CustomAttributeProfile GetProfile(string name) => _customAttributeProfiles.GetValue(name);
        public CustomAttributes AddProfile(CustomAttributeProfile profile)
        {
            _customAttributeProfiles[profile.AttributeName] = profile;
            return this;
        }
        public void SetProfile(string name, CustomAttributeProfile profile)
        {
            if (profile is null)
            {
                _customAttributeProfiles.Remove(name);
                return;
            }
            _customAttributeProfiles[name] = profile;
        }
        public void RemoveProfile(string name)
        {
            _customAttributeProfiles.Remove(name);
            return;
        }
        public CustomAttributes SetProfiles(IDictionary<string, CustomAttributeProfile> customAttributeProfiles)
        {
            _customAttributeProfiles = customAttributeProfiles;
            return this;
        }
        public CustomAttributes ClearProfiles()
        {
            _customAttributeProfiles.Clear();
            return this;
        }

        #endregion CustomAttributeProfile define list

        #region CustomAttribute value list

        public T GetValue<T>(string name, bool insensitive = false)
        {
            return (T)GetValue(typeof(T), name, insensitive);
        }
        public object GetValue(Type type, string name, bool insensitive = false)
        {
            var value = (insensitive)
                ? _customAttributeValues
                    .FirstOrDefault(md => md.Key.Equals(name, StringComparison.OrdinalIgnoreCase))
                    .Maybe(v => v.Value)
                : _customAttributeValues.GetValue(name);

            if (value != null)
                return value.ConvertObject(type, name);

            var profile = GetProfile(name);
            if (profile?.DefaultValue != null)
                value = profile.DefaultValue;

            return value.ConvertObject(type, name);
        }

        public void SetValue<T>(string name, T value)
        {
            if (value is null)
            {
                _customAttributeValues.Remove(name);
                return;
            }

            if (value is string)
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    _customAttributeValues.Remove(name);
                    return;
                }
            }

            var profile = GetProfile(name);
            if (profile is null)
            {
                _customAttributeValues.Remove(name); // in case property was removed
                return;
            }

            var valueAsString = value.ToString();
            if (!string.IsNullOrEmpty(profile.OptionList) && profile.OptionList.SplitTo<string>('|').Contains(valueAsString))
                return;

            //if ((profile.DefaultValue != null) && profile.DefaultValue.Equals(valueAsString, StringComparison.OrdinalIgnoreCase))
            //    return;

            _customAttributeValues[name] = value;
        }

        public void Remove(string name)
        {
            _customAttributeValues.Remove(name);
        }
        public CustomAttributes ClearValues()
        {
            _customAttributeValues.Clear();
            return this;
        }
        public CustomAttributes SetValues(Dictionary<string, object> customAttributeValues)
        {
            _customAttributeValues = customAttributeValues;
            return this;
        }
        public CustomAttributes SetValues(JObject json)
        {
            _customAttributeValues = json.ToDicationary();
            return this;
        }

        #endregion CustomAttribute value list


    }
}
