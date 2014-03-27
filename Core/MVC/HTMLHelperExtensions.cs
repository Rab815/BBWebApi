using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http.Metadata;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ModelMetadata = System.Web.Mvc.ModelMetadata;

namespace Core.MVC
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, object attributes = null)
        {
            //Get metadata from enum
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            
            Type type2 = Nullable.GetUnderlyingType(enumType) ?? enumType;
            IList<SelectListItem> list = (from info in type2.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                let rawConstantValue = info.GetRawConstantValue()
                select new SelectListItem
                {
                    Text = GetDisplayName(info), Value = rawConstantValue.ToString()
                }).ToList();

            //Return the regular DropDownlist helper
            return htmlHelper.DropDownListFor(expression, list, attributes);

        }

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

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }



        /// <summary>
        /// Returns Description Attribute information for an Enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ToDescription(this Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var attributes = (DescriptionAttribute[]) value.GetType().GetField(
                Convert.ToString(value)).GetCustomAttributes(typeof (DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : Convert.ToString(value);
        }
    }
}
