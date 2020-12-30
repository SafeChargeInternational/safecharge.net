using Newtonsoft.Json.Serialization;

namespace Safecharge.Utils.Serialization
{
    public class CustomNamingStrategy : CamelCaseNamingStrategy
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName.ToLower().Equals("cvv"))
                return "CVV";

            // return the camelCase
            propertyName = base.ResolvePropertyName(propertyName);

            return propertyName;
        }
    }
}

