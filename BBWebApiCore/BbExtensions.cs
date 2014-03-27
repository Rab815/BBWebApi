using Bloomberglp.Blpapi;

namespace BloombergWebAPICore
{
    public static class BbExtensions
    {
        //value == DBNull.Value ? default(T) : (T)Convert.ChangeType(value, typeof(T)
        /// <summary>
        /// Retrieves a child element, inspects the datatype and retrieves it appropriately
        /// </summary>
        /// <param name="element">parent element</param>
        /// <param name="name">name of the child element to get by datatype</param>
        /// <returns></returns>
        public static dynamic GetElementValueByDataType(this Element element, Name name)
        {
            return GetElementValueByDataType(element, name.ToString());
        }
        public static dynamic GetElementValueByDataType(this Element element, string name)
        {
            // gets the child element being looked for by name and inspects the datatype
            // then based on the parent retrieves that child elements value by datatype appropriately for json
            var childelement = element.GetElement(name);
            switch (childelement.Datatype)
            {
                case Schema.Datatype.BOOL:
                    return element.GetElementAsBool(name);
                    break;
                case Schema.Datatype.BYTEARRAY: 
                    return element.GetElementAsBytes(name);
                    break;
                case Schema.Datatype.CHAR: 
                    return element.GetElementAsChar(name);
                    break;
                case Schema.Datatype.DATE:
                    return element.GetElementAsDate(name);
                    break;
                case Schema.Datatype.DATETIME: 
                    return element.GetElementAsDatetime(name);
                    break;
                case Schema.Datatype.FLOAT32:
                    return element.GetElementAsFloat32(name);
                    break;
                case Schema.Datatype.FLOAT64: 
                    return element.GetElementAsFloat64(name);
                    break;
                case Schema.Datatype.INT32:
                    return element.GetElementAsInt32(name);
                    break;
                case Schema.Datatype.INT64:
                    return element.GetElementAsInt64(name);
                    break;
                case Schema.Datatype.STRING: 
                    return element.GetElementAsString(name);
                    break;
                case Schema.Datatype.TIME:
                    return element.GetElementAsTime(name);
                    break;
                case Schema.Datatype.SEQUENCE:
                case Schema.Datatype.CHOICE:
                case  Schema.Datatype.ENUMERATION:
                    return element.GetElementAsString(name);
                default:
                    return element.GetElementAsName(name);
                    break;
            }
        }
    }
}
