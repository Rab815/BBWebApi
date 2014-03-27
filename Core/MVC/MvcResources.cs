using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVC
{
    internal class MvcResources
    {
        // Fields
        private static CultureInfo resourceCulture;
        private static ResourceManager resourceMan;

        // Methods
        internal MvcResources()
        {
        }

        // Properties
        internal static string ActionMethodSelector_AmbiguousMatch
        {
            get { return ResourceManager.GetString("ActionMethodSelector_AmbiguousMatch", resourceCulture); }
        }

        internal static string ActionMethodSelector_AmbiguousMatchType
        {
            get { return ResourceManager.GetString("ActionMethodSelector_AmbiguousMatchType", resourceCulture); }
        }

        internal static string ArgumentMustBeGreaterThanOrEqualTo
        {
            get { return ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", resourceCulture); }
        }

        internal static string AsyncActionDescriptor_CannotExecuteSynchronously
        {
            get
            {
                return ResourceManager.GetString("AsyncActionDescriptor_CannotExecuteSynchronously", resourceCulture);
            }
        }

        internal static string AsyncActionMethodSelector_AmbiguousMethodMatch
        {
            get { return ResourceManager.GetString("AsyncActionMethodSelector_AmbiguousMethodMatch", resourceCulture); }
        }

        internal static string AsyncActionMethodSelector_CouldNotFindMethod
        {
            get { return ResourceManager.GetString("AsyncActionMethodSelector_CouldNotFindMethod", resourceCulture); }
        }

        internal static string AsyncCommon_AsyncResultAlreadyConsumed
        {
            get { return ResourceManager.GetString("AsyncCommon_AsyncResultAlreadyConsumed", resourceCulture); }
        }

        internal static string AsyncCommon_ControllerMustImplementIAsyncManagerContainer
        {
            get
            {
                return ResourceManager.GetString("AsyncCommon_ControllerMustImplementIAsyncManagerContainer",
                    resourceCulture);
            }
        }

        internal static string AsyncCommon_InvalidAsyncResult
        {
            get { return ResourceManager.GetString("AsyncCommon_InvalidAsyncResult", resourceCulture); }
        }

        internal static string AsyncCommon_InvalidTimeout
        {
            get { return ResourceManager.GetString("AsyncCommon_InvalidTimeout", resourceCulture); }
        }

        internal static string AttributeRouting_CouldNotInferAreaNameFromMissingNamespace
        {
            get
            {
                return ResourceManager.GetString("AttributeRouting_CouldNotInferAreaNameFromMissingNamespace",
                    resourceCulture);
            }
        }

        internal static string AuthorizeAttribute_CannotUseWithinChildActionCache
        {
            get
            {
                return ResourceManager.GetString("AuthorizeAttribute_CannotUseWithinChildActionCache", resourceCulture);
            }
        }

        internal static string ChildActionOnlyAttribute_MustBeInChildRequest
        {
            get { return ResourceManager.GetString("ChildActionOnlyAttribute_MustBeInChildRequest", resourceCulture); }
        }

        internal static string ClientDataTypeModelValidatorProvider_FieldMustBeDate
        {
            get
            {
                return ResourceManager.GetString("ClientDataTypeModelValidatorProvider_FieldMustBeDate", resourceCulture);
            }
        }

        internal static string ClientDataTypeModelValidatorProvider_FieldMustBeNumeric
        {
            get
            {
                return ResourceManager.GetString("ClientDataTypeModelValidatorProvider_FieldMustBeNumeric",
                    resourceCulture);
            }
        }

        internal static string Common_NoRouteMatched
        {
            get { return ResourceManager.GetString("Common_NoRouteMatched", resourceCulture); }
        }

        internal static string Common_NullOrEmpty
        {
            get { return ResourceManager.GetString("Common_NullOrEmpty", resourceCulture); }
        }

        internal static string Common_PartialViewNotFound
        {
            get { return ResourceManager.GetString("Common_PartialViewNotFound", resourceCulture); }
        }

        internal static string Common_PropertyCannotBeNullOrEmpty
        {
            get { return ResourceManager.GetString("Common_PropertyCannotBeNullOrEmpty", resourceCulture); }
        }

        internal static string Common_PropertyNotFound
        {
            get { return ResourceManager.GetString("Common_PropertyNotFound", resourceCulture); }
        }

        internal static string Common_TriState_False
        {
            get { return ResourceManager.GetString("Common_TriState_False", resourceCulture); }
        }

        internal static string Common_TriState_NotSet
        {
            get { return ResourceManager.GetString("Common_TriState_NotSet", resourceCulture); }
        }

        internal static string Common_TriState_True
        {
            get { return ResourceManager.GetString("Common_TriState_True", resourceCulture); }
        }

        internal static string Common_TypeMustDriveFromType
        {
            get { return ResourceManager.GetString("Common_TypeMustDriveFromType", resourceCulture); }
        }

        internal static string Common_ValueNotValidForProperty
        {
            get { return ResourceManager.GetString("Common_ValueNotValidForProperty", resourceCulture); }
        }

        internal static string Common_ViewNotFound
        {
            get { return ResourceManager.GetString("Common_ViewNotFound", resourceCulture); }
        }

        internal static string CompareAttribute_MustMatch
        {
            get { return ResourceManager.GetString("CompareAttribute_MustMatch", resourceCulture); }
        }

        internal static string CompareAttribute_UnknownProperty
        {
            get { return ResourceManager.GetString("CompareAttribute_UnknownProperty", resourceCulture); }
        }

        internal static string Controller_UnknownAction
        {
            get { return ResourceManager.GetString("Controller_UnknownAction", resourceCulture); }
        }

        internal static string Controller_UnknownAction_NoActionName
        {
            get { return ResourceManager.GetString("Controller_UnknownAction_NoActionName", resourceCulture); }
        }

        internal static string Controller_UpdateModel_UpdateUnsuccessful
        {
            get { return ResourceManager.GetString("Controller_UpdateModel_UpdateUnsuccessful", resourceCulture); }
        }

        internal static string Controller_Validate_ValidationFailed
        {
            get { return ResourceManager.GetString("Controller_Validate_ValidationFailed", resourceCulture); }
        }

        internal static string ControllerBase_CannotExecuteWithNullHttpContext
        {
            get
            {
                return ResourceManager.GetString("ControllerBase_CannotExecuteWithNullHttpContext", resourceCulture);
            }
        }

        internal static string ControllerBase_CannotHandleMultipleRequests
        {
            get { return ResourceManager.GetString("ControllerBase_CannotHandleMultipleRequests", resourceCulture); }
        }

        internal static string ControllerBuilder_ErrorCreatingControllerFactory
        {
            get
            {
                return ResourceManager.GetString("ControllerBuilder_ErrorCreatingControllerFactory", resourceCulture);
            }
        }

        internal static string ControllerBuilder_FactoryReturnedNull
        {
            get { return ResourceManager.GetString("ControllerBuilder_FactoryReturnedNull", resourceCulture); }
        }

        internal static string ControllerBuilder_MissingIControllerFactory
        {
            get { return ResourceManager.GetString("ControllerBuilder_MissingIControllerFactory", resourceCulture); }
        }

        internal static string CshtmlView_ViewCouldNotBeCreated
        {
            get { return ResourceManager.GetString("CshtmlView_ViewCouldNotBeCreated", resourceCulture); }
        }

        internal static string CshtmlView_WrongViewBase
        {
            get { return ResourceManager.GetString("CshtmlView_WrongViewBase", resourceCulture); }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        internal static string DataAnnotationsModelMetadataProvider_UnknownProperty
        {
            get
            {
                return ResourceManager.GetString("DataAnnotationsModelMetadataProvider_UnknownProperty", resourceCulture);
            }
        }

        internal static string DataAnnotationsModelMetadataProvider_UnreadableProperty
        {
            get
            {
                return ResourceManager.GetString("DataAnnotationsModelMetadataProvider_UnreadableProperty",
                    resourceCulture);
            }
        }

        internal static string DataAnnotationsModelValidatorProvider_ConstructorRequirements
        {
            get
            {
                return ResourceManager.GetString("DataAnnotationsModelValidatorProvider_ConstructorRequirements",
                    resourceCulture);
            }
        }

        internal static string DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements
        {
            get
            {
                return
                    ResourceManager.GetString(
                        "DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements", resourceCulture);
            }
        }

        internal static string DefaultControllerFactory_ControllerNameAmbiguous_WithoutRouteUrl
        {
            get
            {
                return ResourceManager.GetString("DefaultControllerFactory_ControllerNameAmbiguous_WithoutRouteUrl",
                    resourceCulture);
            }
        }

        internal static string DefaultControllerFactory_ControllerNameAmbiguous_WithRouteUrl
        {
            get
            {
                return ResourceManager.GetString("DefaultControllerFactory_ControllerNameAmbiguous_WithRouteUrl",
                    resourceCulture);
            }
        }

        internal static string DefaultControllerFactory_DirectRouteAmbiguous
        {
            get { return ResourceManager.GetString("DefaultControllerFactory_DirectRouteAmbiguous", resourceCulture); }
        }

        internal static string DefaultControllerFactory_ErrorCreatingController
        {
            get
            {
                return ResourceManager.GetString("DefaultControllerFactory_ErrorCreatingController", resourceCulture);
            }
        }

        internal static string DefaultControllerFactory_NoControllerFound
        {
            get { return ResourceManager.GetString("DefaultControllerFactory_NoControllerFound", resourceCulture); }
        }

        internal static string DefaultControllerFactory_TypeDoesNotSubclassControllerBase
        {
            get
            {
                return ResourceManager.GetString("DefaultControllerFactory_TypeDoesNotSubclassControllerBase",
                    resourceCulture);
            }
        }

        internal static string DefaultInlineConstraintResolver_AmbiguousCtors
        {
            get { return ResourceManager.GetString("DefaultInlineConstraintResolver_AmbiguousCtors", resourceCulture); }
        }

        internal static string DefaultInlineConstraintResolver_CouldNotFindCtor
        {
            get
            {
                return ResourceManager.GetString("DefaultInlineConstraintResolver_CouldNotFindCtor", resourceCulture);
            }
        }

        internal static string DefaultInlineConstraintResolver_TypeNotConstraint
        {
            get
            {
                return ResourceManager.GetString("DefaultInlineConstraintResolver_TypeNotConstraint", resourceCulture);
            }
        }

        internal static string DefaultModelBinder_ValueInvalid
        {
            get { return ResourceManager.GetString("DefaultModelBinder_ValueInvalid", resourceCulture); }
        }

        internal static string DefaultModelBinder_ValueRequired
        {
            get { return ResourceManager.GetString("DefaultModelBinder_ValueRequired", resourceCulture); }
        }

        internal static string DefaultViewLocationCache_NegativeTimeSpan
        {
            get { return ResourceManager.GetString("DefaultViewLocationCache_NegativeTimeSpan", resourceCulture); }
        }

        internal static string DependencyResolver_DoesNotImplementICommonServiceLocator
        {
            get
            {
                return ResourceManager.GetString("DependencyResolver_DoesNotImplementICommonServiceLocator",
                    resourceCulture);
            }
        }

        internal static string DirectRoute_AmbiguousMatch
        {
            get { return ResourceManager.GetString("DirectRoute_AmbiguousMatch", resourceCulture); }
        }

        internal static string DirectRoute_InvalidParameter_Action
        {
            get { return ResourceManager.GetString("DirectRoute_InvalidParameter_Action", resourceCulture); }
        }

        internal static string DirectRoute_InvalidParameter_Controller
        {
            get { return ResourceManager.GetString("DirectRoute_InvalidParameter_Controller", resourceCulture); }
        }

        internal static string DirectRoute_MissingActionDescriptors
        {
            get { return ResourceManager.GetString("DirectRoute_MissingActionDescriptors", resourceCulture); }
        }

        internal static string DirectRoute_MissingControllerDescriptor
        {
            get { return ResourceManager.GetString("DirectRoute_MissingControllerDescriptor", resourceCulture); }
        }

        internal static string DirectRoute_MissingControllerType
        {
            get { return ResourceManager.GetString("DirectRoute_MissingControllerType", resourceCulture); }
        }

        internal static string DirectRoute_RouteHandlerNotSupported
        {
            get { return ResourceManager.GetString("DirectRoute_RouteHandlerNotSupported", resourceCulture); }
        }

        internal static string EnumHelper_InvalidMetadataParameter
        {
            get { return ResourceManager.GetString("EnumHelper_InvalidMetadataParameter", resourceCulture); }
        }

        internal static string EnumHelper_InvalidParameterType
        {
            get { return ResourceManager.GetString("EnumHelper_InvalidParameterType", resourceCulture); }
        }

        internal static string EnumHelper_InvalidValueParameter
        {
            get { return ResourceManager.GetString("EnumHelper_InvalidValueParameter", resourceCulture); }
        }

        internal static string ExceptionViewAttribute_NonExceptionType
        {
            get { return ResourceManager.GetString("ExceptionViewAttribute_NonExceptionType", resourceCulture); }
        }

        internal static string ExpressionHelper_InvalidIndexerExpression
        {
            get { return ResourceManager.GetString("ExpressionHelper_InvalidIndexerExpression", resourceCulture); }
        }

        internal static string FilterAttribute_OrderOutOfRange
        {
            get { return ResourceManager.GetString("FilterAttribute_OrderOutOfRange", resourceCulture); }
        }

        internal static string GlobalFilterCollection_UnsupportedFilterInstance
        {
            get
            {
                return ResourceManager.GetString("GlobalFilterCollection_UnsupportedFilterInstance", resourceCulture);
            }
        }

        internal static string HtmlHelper_InvalidHttpMethod
        {
            get { return ResourceManager.GetString("HtmlHelper_InvalidHttpMethod", resourceCulture); }
        }

        internal static string HtmlHelper_InvalidHttpVerb
        {
            get { return ResourceManager.GetString("HtmlHelper_InvalidHttpVerb", resourceCulture); }
        }

        internal static string HtmlHelper_MissingSelectData
        {
            get { return ResourceManager.GetString("HtmlHelper_MissingSelectData", resourceCulture); }
        }

        internal static string HtmlHelper_SelectExpressionNotEnumerable
        {
            get { return ResourceManager.GetString("HtmlHelper_SelectExpressionNotEnumerable", resourceCulture); }
        }

        internal static string HtmlHelper_TextAreaParameterOutOfRange
        {
            get { return ResourceManager.GetString("HtmlHelper_TextAreaParameterOutOfRange", resourceCulture); }
        }

        internal static string HtmlHelper_WrongSelectDataType
        {
            get { return ResourceManager.GetString("HtmlHelper_WrongSelectDataType", resourceCulture); }
        }

        internal static string HttpRouteBuilder_CouldNotResolveConstraint
        {
            get { return ResourceManager.GetString("HttpRouteBuilder_CouldNotResolveConstraint", resourceCulture); }
        }

        internal static string JsonRequest_GetNotAllowed
        {
            get { return ResourceManager.GetString("JsonRequest_GetNotAllowed", resourceCulture); }
        }

        internal static string JsonValueProviderFactory_RequestTooLarge
        {
            get { return ResourceManager.GetString("JsonValueProviderFactory_RequestTooLarge", resourceCulture); }
        }

        internal static string ModelBinderAttribute_ErrorCreatingModelBinder
        {
            get { return ResourceManager.GetString("ModelBinderAttribute_ErrorCreatingModelBinder", resourceCulture); }
        }

        internal static string ModelBinderAttribute_TypeNotIModelBinder
        {
            get { return ResourceManager.GetString("ModelBinderAttribute_TypeNotIModelBinder", resourceCulture); }
        }

        internal static string ModelBinderDictionary_MultipleAttributes
        {
            get { return ResourceManager.GetString("ModelBinderDictionary_MultipleAttributes", resourceCulture); }
        }

        internal static string ModelMetadata_PropertyNotSettable
        {
            get { return ResourceManager.GetString("ModelMetadata_PropertyNotSettable", resourceCulture); }
        }

        internal static string MvcForm_ConstructorObsolete
        {
            get { return ResourceManager.GetString("MvcForm_ConstructorObsolete", resourceCulture); }
        }

        internal static string MvcRazorCodeParser_CannotHaveModelAndInheritsKeyword
        {
            get
            {
                return ResourceManager.GetString("MvcRazorCodeParser_CannotHaveModelAndInheritsKeyword", resourceCulture);
            }
        }

        internal static string MvcRazorCodeParser_ModelKeywordMustBeFollowedByTypeName
        {
            get
            {
                return ResourceManager.GetString("MvcRazorCodeParser_ModelKeywordMustBeFollowedByTypeName",
                    resourceCulture);
            }
        }

        internal static string MvcRazorCodeParser_OnlyOneModelStatementIsAllowed
        {
            get
            {
                return ResourceManager.GetString("MvcRazorCodeParser_OnlyOneModelStatementIsAllowed", resourceCulture);
            }
        }

        internal static string MvcRouteHandler_RouteValuesHasNoController
        {
            get { return ResourceManager.GetString("MvcRouteHandler_RouteValuesHasNoController", resourceCulture); }
        }

        internal static string OutputCacheAttribute_CannotNestChildCache
        {
            get { return ResourceManager.GetString("OutputCacheAttribute_CannotNestChildCache", resourceCulture); }
        }

        internal static string OutputCacheAttribute_ChildAction_UnsupportedSetting
        {
            get
            {
                return ResourceManager.GetString("OutputCacheAttribute_ChildAction_UnsupportedSetting", resourceCulture);
            }
        }

        internal static string OutputCacheAttribute_InvalidDuration
        {
            get { return ResourceManager.GetString("OutputCacheAttribute_InvalidDuration", resourceCulture); }
        }

        internal static string OutputCacheAttribute_InvalidVaryByParam
        {
            get { return ResourceManager.GetString("OutputCacheAttribute_InvalidVaryByParam", resourceCulture); }
        }

        internal static string RedirectAction_CannotRedirectInChildAction
        {
            get { return ResourceManager.GetString("RedirectAction_CannotRedirectInChildAction", resourceCulture); }
        }

        internal static string ReflectedActionDescriptor_CannotCallInstanceMethodOnNonControllerType
        {
            get
            {
                return ResourceManager.GetString(
                    "ReflectedActionDescriptor_CannotCallInstanceMethodOnNonControllerType", resourceCulture);
            }
        }

        internal static string ReflectedActionDescriptor_CannotCallMethodsWithOutOrRefParameters
        {
            get
            {
                return ResourceManager.GetString("ReflectedActionDescriptor_CannotCallMethodsWithOutOrRefParameters",
                    resourceCulture);
            }
        }

        internal static string ReflectedActionDescriptor_CannotCallOpenGenericMethods
        {
            get
            {
                return ResourceManager.GetString("ReflectedActionDescriptor_CannotCallOpenGenericMethods",
                    resourceCulture);
            }
        }

        internal static string ReflectedActionDescriptor_CannotCallStaticMethod
        {
            get
            {
                return ResourceManager.GetString("ReflectedActionDescriptor_CannotCallStaticMethod", resourceCulture);
            }
        }

        internal static string ReflectedActionDescriptor_ParameterCannotBeNull
        {
            get
            {
                return ResourceManager.GetString("ReflectedActionDescriptor_ParameterCannotBeNull", resourceCulture);
            }
        }

        internal static string ReflectedActionDescriptor_ParameterNotInDictionary
        {
            get
            {
                return ResourceManager.GetString("ReflectedActionDescriptor_ParameterNotInDictionary", resourceCulture);
            }
        }

        internal static string ReflectedActionDescriptor_ParameterValueHasWrongType
        {
            get
            {
                return ResourceManager.GetString("ReflectedActionDescriptor_ParameterValueHasWrongType", resourceCulture);
            }
        }

        internal static string ReflectedParameterBindingInfo_MultipleConverterAttributes
        {
            get
            {
                return ResourceManager.GetString("ReflectedParameterBindingInfo_MultipleConverterAttributes",
                    resourceCulture);
            }
        }

        internal static string RemoteAttribute_NoUrlFound
        {
            get { return ResourceManager.GetString("RemoteAttribute_NoUrlFound", resourceCulture); }
        }

        internal static string RemoteAttribute_RemoteValidationFailed
        {
            get { return ResourceManager.GetString("RemoteAttribute_RemoteValidationFailed", resourceCulture); }
        }

        internal static string RequireHttpsAttribute_MustUseSsl
        {
            get { return ResourceManager.GetString("RequireHttpsAttribute_MustUseSsl", resourceCulture); }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    ResourceManager manager = new ResourceManager("System.Web.Mvc.Properties.MvcResources",
                        typeof (MvcResources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string Route_CannotHaveCatchAllInMultiSegment
        {
            get { return ResourceManager.GetString("Route_CannotHaveCatchAllInMultiSegment", resourceCulture); }
        }

        internal static string Route_CannotHaveConsecutiveParameters
        {
            get { return ResourceManager.GetString("Route_CannotHaveConsecutiveParameters", resourceCulture); }
        }

        internal static string Route_CannotHaveConsecutiveSeparators
        {
            get { return ResourceManager.GetString("Route_CannotHaveConsecutiveSeparators", resourceCulture); }
        }

        internal static string Route_CatchAllMustBeLast
        {
            get { return ResourceManager.GetString("Route_CatchAllMustBeLast", resourceCulture); }
        }

        internal static string Route_InvalidConstraint
        {
            get { return ResourceManager.GetString("Route_InvalidConstraint", resourceCulture); }
        }

        internal static string Route_InvalidParameterName
        {
            get { return ResourceManager.GetString("Route_InvalidParameterName", resourceCulture); }
        }

        internal static string Route_InvalidRouteTemplate
        {
            get { return ResourceManager.GetString("Route_InvalidRouteTemplate", resourceCulture); }
        }

        internal static string Route_MismatchedParameter
        {
            get { return ResourceManager.GetString("Route_MismatchedParameter", resourceCulture); }
        }

        internal static string Route_RepeatedParameter
        {
            get { return ResourceManager.GetString("Route_RepeatedParameter", resourceCulture); }
        }

        internal static string RouteAreaPrefix_CannotEnd_WithForwardSlash
        {
            get { return ResourceManager.GetString("RouteAreaPrefix_CannotEnd_WithForwardSlash", resourceCulture); }
        }

        internal static string RoutePrefix_CannotStartOrEnd_WithForwardSlash
        {
            get { return ResourceManager.GetString("RoutePrefix_CannotStartOrEnd_WithForwardSlash", resourceCulture); }
        }

        internal static string RoutePrefix_CannotSupportMultiRoutePrefix
        {
            get { return ResourceManager.GetString("RoutePrefix_CannotSupportMultiRoutePrefix", resourceCulture); }
        }

        internal static string RoutePrefix_PrefixCannotBeNull
        {
            get { return ResourceManager.GetString("RoutePrefix_PrefixCannotBeNull", resourceCulture); }
        }

        internal static string RouteTemplate_CannotStart_WithForwardSlash
        {
            get { return ResourceManager.GetString("RouteTemplate_CannotStart_WithForwardSlash", resourceCulture); }
        }

        internal static string SelectExtensions_InvalidExpressionParameterNoMetadata
        {
            get
            {
                return ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterNoMetadata",
                    resourceCulture);
            }
        }

        internal static string SelectExtensions_InvalidExpressionParameterNoModelType
        {
            get
            {
                return ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterNoModelType",
                    resourceCulture);
            }
        }

        internal static string SelectExtensions_InvalidExpressionParameterType
        {
            get
            {
                return ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterType", resourceCulture);
            }
        }

        internal static string SelectExtensions_InvalidExpressionParameterTypeHasFlags
        {
            get
            {
                return ResourceManager.GetString("SelectExtensions_InvalidExpressionParameterTypeHasFlags",
                    resourceCulture);
            }
        }

        internal static string SessionStateTempDataProvider_SessionStateDisabled
        {
            get
            {
                return ResourceManager.GetString("SessionStateTempDataProvider_SessionStateDisabled", resourceCulture);
            }
        }

        internal static string SingleServiceResolver_CannotRegisterTwoInstances
        {
            get
            {
                return ResourceManager.GetString("SingleServiceResolver_CannotRegisterTwoInstances", resourceCulture);
            }
        }

        internal static string SubRouteCollection_DuplicateRouteName
        {
            get { return ResourceManager.GetString("SubRouteCollection_DuplicateRouteName", resourceCulture); }
        }

        internal static string SynchronizationContextUtil_ExceptionThrown
        {
            get { return ResourceManager.GetString("SynchronizationContextUtil_ExceptionThrown", resourceCulture); }
        }

        internal static string TaskAsyncActionDescriptor_CannotExecuteSynchronously
        {
            get
            {
                return ResourceManager.GetString("TaskAsyncActionDescriptor_CannotExecuteSynchronously", resourceCulture);
            }
        }

        internal static string TemplateHelpers_NoTemplate
        {
            get { return ResourceManager.GetString("TemplateHelpers_NoTemplate", resourceCulture); }
        }

        internal static string TemplateHelpers_TemplateLimitations
        {
            get { return ResourceManager.GetString("TemplateHelpers_TemplateLimitations", resourceCulture); }
        }

        internal static string Templates_TypeMustImplementIEnumerable
        {
            get { return ResourceManager.GetString("Templates_TypeMustImplementIEnumerable", resourceCulture); }
        }

        internal static string TypeCache_DoNotModify
        {
            get { return ResourceManager.GetString("TypeCache_DoNotModify", resourceCulture); }
        }

        internal static string TypeHelpers_CannotCreateInstance
        {
            get { return ResourceManager.GetString("TypeHelpers_CannotCreateInstance", resourceCulture); }
        }

        internal static string TypeMethodMustNotReturnNull
        {
            get { return ResourceManager.GetString("TypeMethodMustNotReturnNull", resourceCulture); }
        }

        internal static string ValidatableObjectAdapter_IncompatibleType
        {
            get { return ResourceManager.GetString("ValidatableObjectAdapter_IncompatibleType", resourceCulture); }
        }

        internal static string ValueProviderResult_ConversionThrew
        {
            get { return ResourceManager.GetString("ValueProviderResult_ConversionThrew", resourceCulture); }
        }

        internal static string ValueProviderResult_NoConverterExists
        {
            get { return ResourceManager.GetString("ValueProviderResult_NoConverterExists", resourceCulture); }
        }

        internal static string ViewDataDictionary_ModelCannotBeNull
        {
            get { return ResourceManager.GetString("ViewDataDictionary_ModelCannotBeNull", resourceCulture); }
        }

        internal static string ViewDataDictionary_WrongTModelType
        {
            get { return ResourceManager.GetString("ViewDataDictionary_WrongTModelType", resourceCulture); }
        }

        internal static string ViewMasterPage_RequiresViewPage
        {
            get { return ResourceManager.GetString("ViewMasterPage_RequiresViewPage", resourceCulture); }
        }

        internal static string ViewPageHttpHandlerWrapper_ExceptionOccurred
        {
            get { return ResourceManager.GetString("ViewPageHttpHandlerWrapper_ExceptionOccurred", resourceCulture); }
        }

        internal static string ViewStartPage_RequiresMvcRazorView
        {
            get { return ResourceManager.GetString("ViewStartPage_RequiresMvcRazorView", resourceCulture); }
        }

        internal static string ViewUserControl_RequiresViewDataProvider
        {
            get { return ResourceManager.GetString("ViewUserControl_RequiresViewDataProvider", resourceCulture); }
        }

        internal static string ViewUserControl_RequiresViewPage
        {
            get { return ResourceManager.GetString("ViewUserControl_RequiresViewPage", resourceCulture); }
        }

        internal static string WebFormViewEngine_UserControlCannotHaveMaster
        {
            get { return ResourceManager.GetString("WebFormViewEngine_UserControlCannotHaveMaster", resourceCulture); }
        }

        internal static string WebFormViewEngine_WrongViewBase
        {
            get { return ResourceManager.GetString("WebFormViewEngine_WrongViewBase", resourceCulture); }
        }
    }
}
