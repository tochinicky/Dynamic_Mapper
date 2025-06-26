using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.RestaurantPartner.Models
{
    public class RestaurantReservation
    {
        public required string ReservationCode { get; set; }
        public string? VenueName { get; set; }
        public DateTime SlotDateTime { get; set; }
        public int Guests { get; set; }
    }
}
