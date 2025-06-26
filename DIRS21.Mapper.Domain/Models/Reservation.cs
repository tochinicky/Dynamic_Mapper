using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Domain.Models
{
    public class Reservation
    {
        public required string Id { get; set; }
        public required string GuestName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string? RoomType { get; set; }
        public string? Status { get; set; }
        public decimal TotalAmount { get; set; }

        // Additional properties can be added as needed
    }
}
