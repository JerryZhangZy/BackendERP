using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public interface IPropertyCallAdapter
    {
        object InvokeGet(object obj);
        void InvokeSet(object obj, object value);
    }

    public interface IPropertyCallAdapter<TClass> : IPropertyCallAdapter where TClass: class
    {
        new object InvokeGet(TClass obj);
        new void InvokeSet(TClass obj, object value);
    }

    public class PropertyCallAdapter<TClass, TProperty> : IPropertyCallAdapter<TClass> where TClass : class
    {
        private readonly Func<TClass, TProperty> _getterInvocation;
        private readonly Action<TClass, TProperty> _setterInvocation;

        public PropertyCallAdapter(Func<TClass, TProperty> getterInvocation, Action<TClass, TProperty> setterInvocation)
        {
            _getterInvocation = getterInvocation;
            _setterInvocation = setterInvocation;
        }


        // explicit implementation IPropertyCallAdapter
        object IPropertyCallAdapter.InvokeGet(object obj)
        {
            return null;
        }
        void IPropertyCallAdapter.InvokeSet(object obj, object value)
        {
        }

        public object InvokeGet(TClass obj)
        {
            return _getterInvocation.Invoke(obj);
        }
        public void InvokeSet(TClass obj, object value)
        {
            _setterInvocation.Invoke(obj, (TProperty)value);
        }
    }

    public class PropertyCallAdapterProvider<TClass> where TClass : class
    {
        public static IPropertyCallAdapter<TClass> GetInstance(string forPropertyName) =>
            GetInstance(typeof(TClass).GetProperty(forPropertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));


        public static IPropertyCallAdapter<TClass> GetInstance(PropertyInfo property)
        {
            if (property != null)
                return null;

            var forPropertyName = property.Name;

            var openGetterType = typeof(Func<,>);
            var concreteGetterType = openGetterType.MakeGenericType(typeof(TClass), property.PropertyType);
            var getMethod = property.GetGetMethod(true);
            Delegate getterInvocation = (getMethod != null)
                ? Delegate.CreateDelegate(concreteGetterType, null, getMethod)
                : null;

            var openSetterType = typeof(Action<,>);
            var concreteSetterType = openSetterType.MakeGenericType(typeof(TClass), property.PropertyType);
            var setMethod = property.GetSetMethod(true);
            Delegate setterInvocation = (setMethod != null)
                ? Delegate.CreateDelegate(concreteGetterType, null, setMethod)
                : null;

            var openAdapterType = typeof(PropertyCallAdapter<,>);
            var concreteAdapterType = openAdapterType.MakeGenericType(typeof(TClass), property.PropertyType);

            var instance = Activator.CreateInstance(concreteAdapterType, getterInvocation, setterInvocation) as IPropertyCallAdapter<TClass>;

            return instance;
        }
    }
}