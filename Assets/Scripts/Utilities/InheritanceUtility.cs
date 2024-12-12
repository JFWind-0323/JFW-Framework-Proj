using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Utilities
{
    public static class InheritanceUtility
    {
        public static List<Type> GetAllDerivedTypes<T>()
        {
            return GetAllDerivedTypes(typeof(T));
        }

        public static List<Type> GetAllDerivedTypes(this Type baseType, string assemblyName = "Assembly-CSharp")
        {
            List<Type> derivedTypes = new List<Type>();
            Assembly assembly = Assembly.Load(assemblyName);
            // 获取所有已加载的程序集

            try
            {
                // 获取程序集中的所有类型
                foreach (Type type in assembly.GetTypes())
                {
                    // 检查类型是否继承自指定的基类
                    if (type.IsSubclassOf(baseType))
                    {
                        derivedTypes.Add(type);
                    }
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                // 处理类型加载异常
                foreach (var ex in e.LoaderExceptions)
                {
                    Debug.LogWarning(ex);
                }
            }
            catch (Exception e)
            {
                // 处理其他异常
                Debug.LogWarning(e);
            }


            return derivedTypes;
        }
    }
}