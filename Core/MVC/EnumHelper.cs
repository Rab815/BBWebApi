using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using Core.MVC;

public static class EnumHelper
{
    // Methods
    private static string GetDisplayName(FieldInfo field)
    {
        DisplayAttribute customAttribute = CustomAttributeExtensions.GetCustomAttribute<DisplayAttribute>(field, false);
        if (customAttribute != null)
        {
            string name = customAttribute.GetName();
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }
        }
        return field.Name;
    }

    public static IList<SelectListItem> GetSelectList(Type type)
    {
        if (type == null)
        {
            throw Error.ArgumentNull("type");
        }
        if (!IsValidForEnumHelper(type))
        {
            throw Error.Argument("type", MvcResources.EnumHelper_InvalidParameterType, new object[] { type.FullName });
        }
        IList<SelectListItem> list = new List<SelectListItem>();
        Type type2 = Nullable.GetUnderlyingType(type) ?? type;
        if (type2 != type)
        {
            SelectListItem item = new SelectListItem {
                Text = string.Empty,
                Value = string.Empty
            };
            list.Add(item);
        }
        foreach (FieldInfo info in type2.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly))
        {
            object rawConstantValue = info.GetRawConstantValue();
            SelectListItem item2 = new SelectListItem {
                Text = GetDisplayName(info),
                Value = rawConstantValue.ToString()
            };
            list.Add(item2);
        }
        return list;
    }

    public static IList<SelectListItem> GetSelectList(ModelMetadata metadata)
    {
        if (metadata == null)
        {
            throw Error.ArgumentNull("metadata");
        }
        if (metadata.ModelType == null)
        {
            throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidMetadataParameter, new object[0]);
        }
        if (!IsValidForEnumHelper(metadata))
        {
            throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidParameterType, new object[] { metadata.ModelType.FullName });
        }
        return GetSelectList(metadata.ModelType);
    }

    public static IList<SelectListItem> GetSelectList(Type type, Enum value)
    {
        IList<SelectListItem> selectList = GetSelectList(type);
        Type type2 = (value == 0) ? null : value.GetType();
        if (((type2 != null) && (type2 != type)) && (type2 != Nullable.GetUnderlyingType(type)))
        {
            throw Error.Argument("value", MvcResources.EnumHelper_InvalidValueParameter, new object[] { type2.FullName, type.FullName });
        }
        if (((value == 0) && (selectList.Count != 0)) && string.IsNullOrEmpty(selectList[0].Value))
        {
            selectList[0].Selected = true;
            return selectList;
        }
        string str = (value == 0) ? "0" : value.ToString("d");
        bool flag = false;
        for (int i = selectList.Count - 1; !flag && (i >= 0); i--)
        {
            SelectListItem item = selectList[i];
            item.Selected = str == item.Value;
            flag |= item.Selected;
        }
        if (!flag)
        {
            if ((selectList.Count != 0) && string.IsNullOrEmpty(selectList[0].Value))
            {
                selectList[0].Selected = true;
                selectList[0].Value = str;
                return selectList;
            }
            SelectListItem item2 = new SelectListItem {
                Selected = true,
                Text = string.Empty,
                Value = str
            };
            selectList.Insert(0, item2);
        }
        return selectList;
    }

    public static IList<SelectListItem> GetSelectList(ModelMetadata metadata, Enum value)
    {
        if (metadata == null)
        {
            throw Error.ArgumentNull("metadata");
        }
        if (metadata.ModelType == null)
        {
            throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidMetadataParameter, new object[0]);
        }
        if (!IsValidForEnumHelper(metadata))
        {
            throw Error.Argument("metadata", MvcResources.EnumHelper_InvalidParameterType, new object[] { metadata.ModelType.FullName });
        }
        return GetSelectList(metadata.ModelType, value);
    }

    internal static bool HasFlags(Type type)
    {
        Type type2 = Nullable.GetUnderlyingType(type) ?? type;
        return HasFlagsInternal(type2);
    }

    private static bool HasFlagsInternal(Type type)
    {
        return (CustomAttributeExtensions.GetCustomAttribute<FlagsAttribute>(type, false) != null);
    }

    public static bool IsValidForEnumHelper(Type type)
    {
        bool flag = false;
        if (type != null)
        {
            Type type2 = Nullable.GetUnderlyingType(type) ?? type;
            if (type2.IsEnum)
            {
                flag = !HasFlagsInternal(type2);
            }
        }
        return flag;
    }

    public static bool IsValidForEnumHelper(ModelMetadata metadata)
    {
        return ((metadata != null) && IsValidForEnumHelper(metadata.ModelType));
    }
}
