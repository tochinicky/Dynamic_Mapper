using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.HotelPartner.Models
{
    public class HotelReservation
    {
        public required string ConfirmationNumber { get; set; }
        public string? PropertyId { get; set; }
        public required string GuestName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
