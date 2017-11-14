using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Devdog.SceneCleanerPro
{
    public static class ReflectionUtility 
    {
        public static Type[] GetAllClassesWithAttribute(Type attribute)
        {
            var allClasses = GetAllTypesThatImplement(typeof(object));
            return allClasses.Where(o => o.GetCustomAttributes(attribute, true).Length > 0).ToArray();
        }

        public static FieldInfo[] GetFieldsWithAttributeInherited(object obj, Type attribute)
        {
            var fieldInfo = new List<FieldInfo>();
            GetAllFieldsInherited(obj.GetType(), fieldInfo);

            var fieldNames = new List<FieldInfo>();
            foreach (var info in fieldInfo.Where(o => o.GetCustomAttributes(attribute, true).Length > 0).ToArray())
            {
                fieldNames.Add(info);
            }

            return fieldNames.ToArray();
        }

        public static void GetAllSerializableFieldsInherited(System.Type startType, List<FieldInfo> appendList)
        {
            GetAllFieldsInherited(startType, appendList, BindingFlags.Public | BindingFlags.NonPublic);
            appendList.RemoveAll(o => (o.IsPrivate || o.IsFamily) && o.GetCustomAttributes(typeof (SerializeField), true).Length == 0);
            appendList.RemoveAll(o => o.GetCustomAttributes(typeof (NonSerializedAttribute), true).Length > 0);
            appendList.RemoveAll(o => o.GetCustomAttributes(typeof (HideInInspector), true).Length > 0);
//            appendList.Reverse();
        }

        public static void GetAllFieldsInherited(System.Type startType, List<FieldInfo> appendList)
        {
            GetAllFieldsInherited(startType, appendList, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static void GetAllFieldsInherited(System.Type startType, List<FieldInfo> appendList, BindingFlags flags)
        {
            if (startType == typeof (MonoBehaviour) || startType == null || startType == typeof (object))
            {
                return;
            }

            // Copied fields can be restricted with BindingFlags
            var fields = startType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var fieldInfo in fields)
            {
                if (appendList.Any(o => o.Name == fieldInfo.Name) == false)
                {
                    appendList.Add(fieldInfo);
                }
            }

            // Keep going untill we hit UnityEngine.MonoBehaviour type or null.
            GetAllFieldsInherited(startType.BaseType, appendList);
        }

        public static FieldInfo GetFieldInherited(System.Type startType, string fieldName)
        {
            if (startType == typeof(MonoBehaviour) || startType == null || startType == typeof(object))
                return null;

            // Copied fields can be restricted with BindingFlags
            var field = startType.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return field;
            }

            // Keep going untill we hit UnityEngine.MonoBehaviour type or null.
            return GetFieldInherited(startType.BaseType, fieldName);
        }

        public static Type[] GetAllTypesThatImplement(Type type)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(o => o.GetTypes());
            types = types.Where(o => type.IsAssignableFrom(o));
            types = types.Where(o => o.IsClass);
            types = types.Where(o => o.IsAbstract == false && o.IsInterface == false);
            
            return types.ToArray();
        }

        public static object CreateDeepClone(object obj)
        {
            var l = new List<FieldInfo>();
            GetAllFieldsInherited(obj.GetType(), l);

            var constructor = obj.GetType().GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                null,
                new Type[0],
                null
            );
            var result = constructor.Invoke(new object[0]);

            foreach (var fieldInfo in l)
            {
                var val = fieldInfo.GetValue(obj);
                fieldInfo.SetValue(result, val);
            }

            return result;
        }
    }
}