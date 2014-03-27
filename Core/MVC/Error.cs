using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Core.MVC
{
    internal static class Error
    {
        // Methods
        internal static ArgumentException Argument(string parameterName, string messageFormat, params object[] messageArgs)
        {
            return new ArgumentException(Format(messageFormat, messageArgs), parameterName);
        }

        public static ArgumentOutOfRangeException ArgumentMustBeGreaterThanOrEqualTo(string parameterName, int actualValue, int minValue)
        {
            return new ArgumentOutOfRangeException(parameterName, actualValue, string.Format(CultureInfo.CurrentCulture, MvcResources.ArgumentMustBeGreaterThanOrEqualTo, new object[] { minValue }));
        }

        public static Exception ArgumentNull(string parameterName)
        {
            return new ArgumentNullException(parameterName);
        }

        public static InvalidOperationException AsyncActionMethodSelector_CouldNotFindMethod(string methodName, Type controllerType)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.AsyncActionMethodSelector_CouldNotFindMethod, new object[] { methodName, controllerType }));
        }

        public static InvalidOperationException AsyncCommon_AsyncResultAlreadyConsumed()
        {
            return new InvalidOperationException(MvcResources.AsyncCommon_AsyncResultAlreadyConsumed);
        }

        public static InvalidOperationException AsyncCommon_ControllerMustImplementIAsyncManagerContainer(Type actualControllerType)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.AsyncCommon_ControllerMustImplementIAsyncManagerContainer, new object[] { actualControllerType }));
        }

        public static ArgumentException AsyncCommon_InvalidAsyncResult(string parameterName)
        {
            return new ArgumentException(MvcResources.AsyncCommon_InvalidAsyncResult, parameterName);
        }

        public static ArgumentOutOfRangeException AsyncCommon_InvalidTimeout(string parameterName)
        {
            return new ArgumentOutOfRangeException(parameterName, MvcResources.AsyncCommon_InvalidTimeout);
        }

        public static InvalidOperationException ChildActionOnlyAttribute_MustBeInChildRequest(ActionDescriptor actionDescriptor)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ChildActionOnlyAttribute_MustBeInChildRequest, new object[] { actionDescriptor.ActionName }));
        }

        internal static string Format(string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args);
        }

        public static InvalidOperationException InvalidOperation(string messageFormat, params object[] args)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, messageFormat, args));
        }

        public static ArgumentException ParameterCannotBeNullOrEmpty(string parameterName)
        {
            return new ArgumentException(MvcResources.Common_NullOrEmpty, parameterName);
        }

        public static InvalidOperationException PropertyCannotBeNullOrEmpty(string propertyName)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_PropertyCannotBeNullOrEmpty, new object[] { propertyName }));
        }

        public static SynchronousOperationException SynchronizationContextUtil_ExceptionThrown(Exception innerException)
        {
            return new SynchronousOperationException(MvcResources.SynchronizationContextUtil_ExceptionThrown, innerException);
        }

        public static InvalidOperationException ViewDataDictionary_ModelCannotBeNull(Type modelType)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ViewDataDictionary_ModelCannotBeNull, new object[] { modelType }));
        }

        public static InvalidOperationException ViewDataDictionary_WrongTModelType(Type valueType, Type modelType)
        {
            return new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ViewDataDictionary_WrongTModelType, new object[] { valueType, modelType }));
        }
    }


}
