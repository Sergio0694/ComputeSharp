using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DirectX12GameEngine.Shaders
{
    public static class MemberInfoExtensions
    {
        public static Type GetElementOrDeclaredType(this Type type) => type.IsArray ? type.GetElementType() : type;

        public static bool IsOverride(this MethodInfo methodInfo)
        {
            return methodInfo != methodInfo.GetBaseDefinition();
        }

        public static IList<MethodInfo> GetBaseMethods(this MethodInfo methodInfo)
        {
            List<MethodInfo> methodInfos = new List<MethodInfo>() { methodInfo };

            if (methodInfo.IsOverride())
            {
                MethodInfo? currentMethodInfo = methodInfo.DeclaringType.BaseType?.GetMethod(methodInfo.Name);

                while (currentMethodInfo != null)
                {
                    methodInfos.Add(currentMethodInfo);
                    currentMethodInfo = currentMethodInfo.DeclaringType.BaseType?.GetMethod(currentMethodInfo.Name);
                }
            }

            return methodInfos;
        }

        public static IEnumerable<Type> GetNestedTypesInTypeHierarchy(this Type type, BindingFlags bindingAttr)
        {
            IEnumerable<Type> nestedTypes = type.GetNestedTypes(bindingAttr);

            Type parent = type.BaseType;

            while (parent != null)
            {
                nestedTypes = parent.GetNestedTypes(bindingAttr).Concat(nestedTypes);
                parent = parent.BaseType;
            }

            return nestedTypes;
        }

        public static IEnumerable<MemberInfo> GetMembersInTypeHierarchy(this Type type, BindingFlags bindingAttr)
        {
            Dictionary<Type, int> lookup = new Dictionary<Type, int>();

            int index = 0;
            lookup[type] = index++;

            Type parent = type.BaseType;

            while (parent != null)
            {
                lookup[parent] = index;
                index++;
                parent = parent.BaseType;
            }

            return type.GetMembers(bindingAttr).OrderByDescending(prop => lookup[prop.DeclaringType]);
        }

        public static IEnumerable<MemberInfo> GetMembersInTypeHierarchyInOrder(this Type type, BindingFlags bindingAttr)
        {
            return type.GetMembersInTypeHierarchy(bindingAttr)
                .GroupBy(m => m.DeclaringType)
                .Select(g => g.OrderBy(m => m.GetCustomAttribute<ShaderResourceAttribute>()?.Order))
                .SelectMany(m => m);
        }

        public static IEnumerable<MemberInfo> GetMembersInOrder(this Type type, BindingFlags bindingAttr)
        {
            return type.GetMembers(bindingAttr)
                .OrderBy(m => m.GetCustomAttribute<ShaderResourceAttribute>()?.Order);
        }

        public static object? GetMemberValue(this MemberInfo memberInfo, object? obj) => memberInfo switch
        {
            FieldInfo fieldInfo => obj is null ? null : fieldInfo.GetValue(obj),
            PropertyInfo propertyInfo => obj is null ? null : propertyInfo.GetValue(obj),
            _ => null
        };

        public static Type? GetMemberType(this MemberInfo memberInfo, object? obj) => memberInfo switch
        {
            FieldInfo fieldInfo => obj is null ? fieldInfo.FieldType : fieldInfo.GetValue(obj)?.GetType() ?? fieldInfo.FieldType,
            PropertyInfo propertyInfo => obj is null ? propertyInfo.PropertyType : propertyInfo.GetValue(obj)?.GetType() ?? propertyInfo.PropertyType,
            _ => null
        };

        public static Type? GetMemberType(this MemberInfo memberInfo) => memberInfo switch
        {
            FieldInfo fieldInfo => fieldInfo.FieldType,
            PropertyInfo propertyInfo => propertyInfo.PropertyType,
            _ => null
        };

        public static ShaderResourceAttribute? GetResourceAttribute(this MemberInfo memberInfo, Type? memberType)
        {
            ShaderResourceAttribute? resourceType = memberInfo.GetCustomAttribute<ShaderResourceAttribute>();

            if (resourceType is null) return null;

            return resourceType.Override
                ? resourceType
                : memberType?.GetCustomAttribute<ShaderResourceAttribute>() ?? resourceType;
        }

        public static bool IsStatic(this MemberInfo memberInfo) => memberInfo switch
        {
            FieldInfo fieldInfo => fieldInfo.IsStatic,
            PropertyInfo propertyInfo => propertyInfo.GetAccessors(true)[0].IsStatic,
            _ => false
        };
    }
}
