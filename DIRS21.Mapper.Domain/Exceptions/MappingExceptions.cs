using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Domain.Exceptions
{
    public class MappingNotFoundException : Exception
    {
        public MappingNotFoundException()
        {
        }

        public MappingNotFoundException(string message)
            : base(message)
        {
        }

        public MappingNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public MappingNotFoundException(string sourceType, string targetType)
            : base($"Mapping not found for source type '{sourceType}' and target type '{targetType}'.")
        {
        }
    }

    public class InvalidMappingException : Exception
    {
        public InvalidMappingException()
        {
        }

        public InvalidMappingException(string message)
            : base(message)
        {
        }

        public InvalidMappingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    public class TypeMismatchException : Exception
    {
        public TypeMismatchException()
        {
        }

        public TypeMismatchException(string message) : base(message)
        {
        }

        public TypeMismatchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    public class MappingValidationException : Exception
    {
        public MappingValidationException()
        {
        }

        public MappingValidationException(string message) : base(message)
        {
        }

        public MappingValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
