using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.HotelPartner.Models
{
    public class HotelInventory
    {
        public string? PropertyCode { get; set; }
        public ReadOnlyCollection<HotelRoomType>? RoomTypes { get; set; }
    }
}
