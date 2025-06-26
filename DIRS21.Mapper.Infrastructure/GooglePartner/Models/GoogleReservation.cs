using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.Google.Models
{
    public class GoogleReservation
    {
        public required string Id { get; set; }
        public required string CustomerName { get; set; }
        public DateTime CheckInDate { get; set; }
        //other google specific properties
    }
}
