using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace HeroicCRM.Web.Utilities
{
	public static class JsonExtensions
	{
		public static string ToJson<T>(this T obj, bool includeNull = true)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Converters = new JsonConverter[] { new StringEnumConverter() },
				NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
			};

			return JsonConvert.SerializeObject(obj, settings);
		}

        public static string ToCamelCaseName<TModel, TProp>(this Expression<Func<TModel, TProp>> property)
        {
            //Turns x => x.SomeProperty.SomeValue into "SomeProperty.SomeValue"
            var pascalCaseName = ExpressionHelper.GetExpressionText(property);

            //Turns "SomeProperty.SomeValue" into "someProperty.someValue"
            var camelCaseName = ConvertFullNameToCamelCase(pascalCaseName);
            return camelCaseName;
        }

        /// <summary>
        /// Converts expressions of the form Some.PropertyName to some.propertyName
        /// </summary>
        /// <param name="pascalName"></param>
        /// <returns></returns>
        private static string ConvertFullNameToCamelCase(string pascalName)
        {
            var parts = pascalName.Split('.').Select(ConvertToCamelCase);

            return string.Join(".", parts);
        }

        /// <summary>
        /// Turns a single name into camel case
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string ConvertToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            if (!char.IsUpper(s[0]))
            {
                return s;
            }
            char[] chars = s.ToCharArray();
            for(int i = 0; i < chars.Length; i++)
            {
                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }
                chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
            }
            return new string(chars);
        }
	}
}