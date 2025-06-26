using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Domain.Models
{
    public class Room
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public decimal Price { get; set; }
        public ReadOnlyCollection<string>? Amenities { get; set; }

        // Additional properties can be added as needed
    }
}
