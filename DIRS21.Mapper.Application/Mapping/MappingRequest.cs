using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Application.Mapping
{
    public record MappingRequest(object Data, string SourceType, string TargetType);

}
