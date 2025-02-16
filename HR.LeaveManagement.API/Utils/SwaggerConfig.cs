using Microsoft.OpenApi.Models;
using System.Text;

namespace HR.LeaveManagement.API.Utils
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "LeaveManagement", Version = "v2" });
                c.CustomSchemaIds(GenerateSchemaId);
            });
            return services;
        }

        private static string GenerateSchemaId(Type type) 
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }
            var genericType = type.GetGenericTypeDefinition();
            var genericArguments = type.GetGenericArguments();
            StringBuilder sb = new StringBuilder();
            sb.Append(RemoveGenericTypeParameterCount(genericType.Name));
            sb.Append("<");

            for ( var i = 0; i < genericArguments.Length; i++)
            {
                if(i > 0)
                {
                    sb.Append(",");
                    
                }
                sb.Append(GenerateSchemaId(genericArguments[i]));
            }

            sb.Append(">");
            return sb.ToString();
        }

        private static string RemoveGenericTypeParameterCount(string typeName)
        {
            // Remove the backtick and number from the type name (e.g., "BaseQueryListResponse`1" -> "BaseQueryListResponse")
            int backtickIndex = typeName.IndexOf('`');
            return backtickIndex >= 0 ? typeName.Substring(0, backtickIndex) : typeName;
        }
    }
}
