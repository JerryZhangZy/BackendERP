using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// 设置属性值代理
    /// </summary>
    /// <param name="source">源对象</param>
    /// <param name="value">属性值</param>
    public delegate void SetValueHandler(object source, object value);
    /// <summary>
    /// 获取属性值代理
    /// </summary>
    /// <param name="source">源对象</param>
    /// <returns>属性值</returns>
    public delegate object GetValueHandler(object source);

    /// <summary>
    /// 构造函数代理
    /// </summary>
    /// <returns></returns>
    public delegate object CreateInstanceHandler();

    /// <summary>
    /// 调用函数代理
    /// </summary>
    /// <param name="target"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public delegate object InvokeMethodHandler(object target, object[] parameters);

    /// <summary>
    /// 类型的快速Emit调用
    /// </summary>
    public class FastType
    {
        private static Dictionary<string, Dictionary<string, PropertyHandler>> _propertyHandlers = new Dictionary<string, Dictionary<string, PropertyHandler>>();
        private static Dictionary<string, CreateInstanceHandler> _instanceHandlers = new Dictionary<string, CreateInstanceHandler>();
        private static Dictionary<string, Dictionary<string, InvokeMethodHandler>> _methodHandlers = new Dictionary<string, Dictionary<string, InvokeMethodHandler>>();

        private static object _lockObj = new object();

        /// <summary>
        /// 创建类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Instance(Type type)
        {
            lock (_lockObj)
            {
                if (!_instanceHandlers.ContainsKey(type.FullName))
                {
                    _instanceHandlers[type.FullName] = FastTypeEmit.GetInstanceCreator(type);
                }
            }

            CreateInstanceHandler creator = null;

            if (_instanceHandlers.ContainsKey(type.FullName))
                creator = _instanceHandlers[type.FullName];

            if (creator != null)
                return creator();

            return null;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="property">属性名称</param>
        /// <param name="value">属性值</param>
        public static void Set(object source, string property, object value)
        {
            PropertyHandler handler = GetPropertyHandler(source.GetType(), property);
            if (handler != null && handler.Set != null)
                handler.Set(source, value);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="property">属性名称</param>
        /// <returns>属性值</returns>
        public static object Get(object source, string property)
        {
            PropertyHandler handler = GetPropertyHandler(source.GetType(), property);
            if (handler != null && handler.Get != null)
                return handler.Get(source);
            return null;
        }

        /// <summary>
        /// 创建类型
        /// </summary>
        /// <returns></returns>
        public static T Instance<T>() where T : class
        {
            return Instance(typeof(T)) as T;
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="type">调用方法的类型</param>
        /// <param name="methodName">类型名称</param>
        /// <param name="target">调用方法的对象</param>
        /// <param name="parameters">调用方法的参数</param>
        /// <returns>调用方法的结果，调用失败返回null</returns>
        public static object Invoke(Type type, string methodName, object target, object[] parameters)
        {
            StringBuilder methodNameBuilder = new StringBuilder();
            methodNameBuilder.Append(methodName);
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    Type typeOfParameter = p.GetType();
                    methodNameBuilder.Append("_");
                    methodNameBuilder.Append(typeOfParameter.Name);
                }
            }

            var name = methodNameBuilder.ToString();

            lock (_lockObj)
            {
                if (!_methodHandlers.ContainsKey(type.FullName))
                    _methodHandlers[type.FullName] = new Dictionary<string, InvokeMethodHandler>();

                if (!_methodHandlers[type.FullName].ContainsKey(name))
                {

                    var ms =
                        type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                            .Where(m => m.Name.ToLower() == methodName.ToLower())
                            .ToArray();
                    var methodInfo = GetMethodInfo(ms, parameters);
                    if (methodInfo != null)
                    {
                        _methodHandlers[type.FullName][name] =
                            FastTypeEmit.GetMethodInvoker(methodInfo);
                    }
                    else
                    {
                        _methodHandlers[type.FullName][name] = null;
                    }
                }
            }

            if (_methodHandlers.ContainsKey(type.FullName) && _methodHandlers[type.FullName].ContainsKey(name))
            {
                var caller = _methodHandlers[type.FullName][name];
                if (caller != null)
                    return caller(target, parameters);
            }
            return null;
        }

        /// <summary>
        /// 获取方法信息
        /// </summary>
        /// <param name="methods"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static MethodInfo GetMethodInfo(MethodInfo[] methods, object[] parameters)
        {
            if (methods.Length == 1 || parameters == null || parameters.Length == 0)
                return methods[0];

            foreach (var m in methods)
            {
                var ps = m.GetParameters();
                if (ps.Length != parameters.Length)
                    continue;

                bool found = true;
                for (int i = 0; i < ps.Length; i++)
                {
                    if (ps[i].ParameterType != parameters[i].GetType())
                    {
                        found = false;
                    }
                }
                if (found) return m;
            }
            return null;
        }

        /// <summary>
        /// 取得属性句柄
        /// </summary>
        /// <param name="type">属性所属的类型</param>
        /// <param name="property">属性名称</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns>属性调用句柄，调用失败返回null</returns>
        public static PropertyHandler GetPropertyHandler(Type type, string property,bool ignoreCase=true)
        {

            Dictionary<string, PropertyHandler> handlers = GetPropertyHandler(type);
            if (handlers.ContainsKey(property))
                return handlers[property];

            if (ignoreCase)
            {
                foreach(var key in handlers.Keys)
                {
                    if(key.ToLower()==property.ToLower())
                    {
                        return handlers[key];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 取得属性句柄
        /// </summary>
        /// <typeparam name="T">属性所属的类型</typeparam>
        /// <param name="property">属性名称</param>
        /// <returns>属性调用句柄，调用失败返回null</returns>
        public static PropertyHandler GetPropertyHandler<T>(string property)
        {
            return GetPropertyHandler(typeof(T), property);
        }

        /// <summary>
        /// 取得类型所有属性的句柄
        /// </summary>
        /// <param name="type">属性所述的类型</param>
        /// <returns>类型所有属性的句柄，调用失败返回null</returns>
        public static Dictionary<string, PropertyHandler> GetPropertyHandler(Type type)
        {
            lock (_lockObj)
            {
                if (!_propertyHandlers.ContainsKey(type.FullName))
                {
                    _propertyHandlers[type.FullName] = new Dictionary<string, PropertyHandler>();
                    foreach (var propertyInfo in type.GetProperties())
                    {
                        _propertyHandlers[type.FullName][propertyInfo.Name] = new PropertyHandler(propertyInfo);
                    }
                }
            }
            if (_propertyHandlers.ContainsKey(type.FullName))
                return _propertyHandlers[type.FullName];

            return null;
        }

        /// <summary>
        /// 返回属性名称列表
        /// </summary>
        /// <returns></returns>
        public static List<string> PropertyNames(Type type)
        {
            if (GetPropertyHandler(type) != null)
                return GetPropertyHandler(type).Keys.ToList();

            return new List<string>();
        }
    }

    /// <summary>
    /// FastType内部Emit方法
    /// </summary>
    internal class FastTypeEmit
    {
        /// <summary>
        /// 创建构造函数代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CreateInstanceHandler GetInstanceCreator(Type type)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, type, new Type[0]);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
            ilGenerator.Emit(OpCodes.Ret);
            CreateInstanceHandler creator =
                (CreateInstanceHandler)dynamicMethod.CreateDelegate(typeof(CreateInstanceHandler));
            return creator;
        }

        /// <summary>
        /// 创建方法代理
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static InvokeMethodHandler GetMethodInvoker(MethodInfo methodInfo)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object),
                new Type[] { typeof(object), typeof(object[]) }, methodInfo.DeclaringType.Module);

            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

            ParameterInfo[] parameters = methodInfo.GetParameters();

            Type[] paramTypes = new Type[parameters.Length];

            // copies the parameter types to an array
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                    paramTypes[i] = parameters[i].ParameterType.GetElementType();
                else
                    paramTypes[i] = parameters[i].ParameterType;
            }

            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];

            // generates a local variable for each parameter
            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = ilGenerator.DeclareLocal(paramTypes[i], true);
            }

            // creates code to copy the parameters to the local variables
            for (int i = 0; i < paramTypes.Length; i++)
            {
                ilGenerator.Emit(OpCodes.Ldarg_1);
                EmitFastInt(ilGenerator, i);
                ilGenerator.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(ilGenerator, paramTypes[i]);
                ilGenerator.Emit(OpCodes.Stloc, locals[i]);
            }

            if (!methodInfo.IsStatic)
            {
                // loads the object into the stack
                ilGenerator.Emit(OpCodes.Ldarg_0);
            }

            // loads the parameters copied to the local variables into the stack
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                    ilGenerator.Emit(OpCodes.Ldloca_S, locals[i]);
                else
                    ilGenerator.Emit(OpCodes.Ldloc, locals[i]);
            }

            // calls the method
            if (!methodInfo.IsStatic)
            {
                ilGenerator.EmitCall(OpCodes.Callvirt, methodInfo, null);
            }
            else
            {
                ilGenerator.EmitCall(OpCodes.Call, methodInfo, null);
            }

            // creates code for handling the return value
            if (methodInfo.ReturnType == typeof(void))
            {
                ilGenerator.Emit(OpCodes.Ldnull);
            }
            else
            {
                EmitBoxIfNeeded(ilGenerator, methodInfo.ReturnType);
            }

            // iterates through the parameters updating the parameters passed by ref
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                {
                    ilGenerator.Emit(OpCodes.Ldarg_1);
                    EmitFastInt(ilGenerator, i);
                    ilGenerator.Emit(OpCodes.Ldloc, locals[i]);
                    if (locals[i].LocalType.IsValueType)
                        ilGenerator.Emit(OpCodes.Box, locals[i].LocalType);
                    ilGenerator.Emit(OpCodes.Stelem_Ref);
                }
            }

            // returns the value to the caller
            ilGenerator.Emit(OpCodes.Ret);

            // converts the DynamicMethod to a FastInvokeHandler delegate to call to the method
            InvokeMethodHandler invoker =
                (InvokeMethodHandler)dynamicMethod.CreateDelegate(typeof(InvokeMethodHandler));

            return invoker;
        }

        /// <summary>
        /// 创建属性赋值代理
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static SetValueHandler CreatePropertySetHandler(PropertyInfo property)
        {
            try
            {
                Type type = property.PropertyType;
                if (!type.IsValueType && type != typeof(string) && type != typeof(byte[]))
                    return null;

                DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, null,
                    new Type[] { typeof(object), typeof(object) }, property.DeclaringType.Module);
                ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldarg_1);
                if (type.IsValueType)
                {
                    ilGenerator.Emit(OpCodes.Unbox_Any, type);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Castclass, type);
                }
                ilGenerator.EmitCall(OpCodes.Callvirt, property.GetSetMethod(), null);
                ilGenerator.Emit(OpCodes.Ret);
                SetValueHandler setter = (SetValueHandler)dynamicMethod.CreateDelegate(typeof(SetValueHandler));
                return setter;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建属性取值代理
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static GetValueHandler CreatePropertyGetHandler(PropertyInfo property)
        {
            try
            {
                Type type = property.PropertyType;
                if (!type.IsValueType && type != typeof(string) && type != typeof(byte[]))
                    return null;

                DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object),
                    new Type[] { typeof(object) }, property.DeclaringType.Module);

                ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.EmitCall(OpCodes.Callvirt, property.GetGetMethod(), null);
                if (type.IsValueType)
                {
                    ilGenerator.Emit(OpCodes.Box, type);
                }
                ilGenerator.Emit(OpCodes.Ret);
                GetValueHandler getter = (GetValueHandler)dynamicMethod.CreateDelegate(typeof(GetValueHandler));
                return getter;
            }
            catch
            {
                return null;
            }
        }

        private static void EmitCastToReference(ILGenerator ilGenerator, System.Type type)
        {
            if (type.IsValueType)
            {
                ilGenerator.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Castclass, type);
            }
        }

        private static void EmitBoxIfNeeded(ILGenerator ilGenerator, System.Type type)
        {
            if (type.IsValueType)
            {
                ilGenerator.Emit(OpCodes.Box, type);
            }
        }

        private static void EmitFastInt(ILGenerator ilGenerator, int value)
        {
            switch (value)
            {
                case -1:
                    ilGenerator.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    ilGenerator.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    ilGenerator.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    ilGenerator.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    ilGenerator.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    ilGenerator.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    ilGenerator.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    ilGenerator.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                ilGenerator.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ldc_I4, value);
            }
        }
    }

    /// <summary>
    /// 属性代理
    /// </summary>
    public class PropertyHandler
    {
        public PropertyHandler(PropertyInfo property)
        {
            if (property.CanWrite)
                _setValue = FastTypeEmit.CreatePropertySetHandler(property);
            if (property.CanRead)
                _getValue = FastTypeEmit.CreatePropertyGetHandler(property);
            _property = property;
        }

        private PropertyInfo _property;
        public PropertyInfo Property
        {
            get
            {
                return _property;
            }
            set
            {
                _property = value;
            }
        }
        private GetValueHandler _getValue;
        /// <summary>
        /// 取得属性值方法代理
        /// </summary>
        public GetValueHandler Get
        {
            get
            {
                return _getValue;
            }

        }
        private SetValueHandler _setValue;
        /// <summary>
        /// 设置属性值方法代理
        /// </summary>
        public SetValueHandler Set
        {
            get
            {
                return _setValue;
            }

        }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type PropertyType
        {
            get
            {
                return _property.PropertyType;
            }
        }
    }
}

