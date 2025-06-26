using DIRS21.Mapper.Application.Helper;
using DIRS21.Mapper.Domain.Exceptions;
using DIRS21.Mapper.Domain.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Application.Mapping
{
    public class MapHandler
    {

        private readonly IServiceProvider _serviceProvider;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public MapHandler(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public object Map(object data, string sourceType, string targetType)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data), "Data to map cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(sourceType))
                throw new ArgumentNullException(nameof(sourceType), "Source type cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(targetType))
                throw new ArgumentNullException(nameof(targetType), "Target type cannot be null or empty.");


            Type source = TypeResolver.ResolveType(sourceType);
            Type target = TypeResolver.ResolveType(targetType);

            //Validate data type against source type
            object? typedData = null;
            try
            {
                if (data is JsonElement jsonElement)
                {
                    typedData = JsonSerializer.Deserialize(jsonElement.GetRawText(), source, _jsonOptions)
                        ?? throw new MappingValidationException($"Failed to deserialize data to type '{sourceType}'");
                }
                else if (data is JObject jObject)
                {
                    typedData = jObject.ToObject(source)
                        ?? throw new MappingValidationException($"Failed to convert JSObject to type '{sourceType}'");
                }
                else
                {
                    if (!source.IsInstanceOfType(data))
                    {
                        throw new TypeMismatchException($"Input data type ({data.GetType().Name}) " +
                            $"does not match expected source type ({source.Name}) for '{sourceType}'");
                    }
                }
            }
            catch (JsonException ex)
            {

                throw new MappingValidationException($"Invalid JSON format: {ex.Message}");
            }

            //Get the generic type definition of the mapping provider interface
            var mapperType = typeof(IMappingProvider<,>).MakeGenericType(source, target);

            //Retrieve mapper from DI   
            var mapper = _serviceProvider.GetService(mapperType) ?? throw new MappingNotFoundException(sourceType, targetType);

            //Invoke the Map method on the mapper instance
            return mapperType.GetMethod("Map")!.Invoke(mapper, new[] { typedData })
                ?? throw new InvalidMappingException($"Mapping failed for source type '{sourceType}' and target type '{targetType}'.");
        }
    }
}
