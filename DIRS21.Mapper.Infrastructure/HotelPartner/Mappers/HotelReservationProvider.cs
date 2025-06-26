using DIRS21.Mapper.Domain.Interfaces;
using DIRS21.Mapper.Domain.Models;
using DIRS21.Mapper.Infrastructure.HotelPartner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.HotelPartner.Mapping
{
    public class HotelReservationProvider : IMappingProvider<Reservation, HotelReservation>, IMappingProvider<HotelReservation, Reservation>
    {

        public Reservation Map(HotelReservation source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return new Reservation
            {
                Id = source.ConfirmationNumber.Split('-')[1],
                CheckIn = source.CheckInDate,
                CheckOut = source.CheckOutDate,
                GuestName = source.GuestName
            };
        }

        public HotelReservation Map(Reservation source)
        {
            ArgumentNullException.ThrowIfNull(source);
            return new HotelReservation
            {
                ConfirmationNumber = $"HOTEL-{source.Id}",
                CheckInDate = source.CheckIn,
                CheckOutDate = source.CheckOut,
                GuestName = source.GuestName
            };
        }
    }
}
