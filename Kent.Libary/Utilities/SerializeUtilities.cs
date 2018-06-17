using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Kent.Libary.Utilities
{
   public static class SerializeUtilities
    {
        private static readonly JavaScriptSerializer JavaScriptSerializer;

        static SerializeUtilities()
        {
            JavaScriptSerializer = new JavaScriptSerializer();
        }

        /// <summary>
        /// Serialize object to json string
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Serialize(object model)
        {
            return JavaScriptSerializer.Serialize(model);
        }

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            try
            {
                if (typeof(T).IsValueType || typeof(T) == typeof(string) || typeof(T) == typeof(String))
                {
                    return json.ToType<T>();
                }

                return JavaScriptSerializer.Deserialize<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T DeserializeXml<T>(string xmlString)
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(T));
                return (T)deserializer.Deserialize(new StringReader(xmlString));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
