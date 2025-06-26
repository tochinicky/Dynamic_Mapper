using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Domain.Models
{
    public class HotelProperty
    {
        public string? HotelId { get; set; }
        public string? Name { get; set; }
        public ReadOnlyCollection<Room>? Rooms { get; set; }
    }
}
