using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Domain.Models
{
    public class RestaurantBooking
    {
        public string? BookingId { get; set; }
        public string? RestaurantName { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
