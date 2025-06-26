using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Domain.Interfaces
{
    public interface IMappingProvider<in TSource, out TTarget>
    {
        TTarget Map(TSource source);
    }
}
