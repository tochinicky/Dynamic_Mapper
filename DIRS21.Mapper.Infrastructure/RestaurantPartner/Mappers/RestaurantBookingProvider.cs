using DIRS21.Mapper.Domain.Interfaces;
using DIRS21.Mapper.Domain.Models;
using DIRS21.Mapper.Infrastructure.RestaurantPartner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.RestaurantPartner.Mapping
{
    public class RestaurantBookingProvider : IMappingProvider<RestaurantBooking, RestaurantReservation>, IMappingProvider<RestaurantReservation, RestaurantBooking>
    {

        public RestaurantReservation Map(RestaurantBooking source)
        {
            ArgumentNullException.ThrowIfNull(source);
            return new RestaurantReservation
            {
                ReservationCode = $"RES-{source.BookingId}",
                VenueName = source.RestaurantName,
                SlotDateTime = source.BookingTime
            };
        }

        public RestaurantBooking Map(RestaurantReservation source)
        {
            ArgumentNullException.ThrowIfNull(source);
            return new RestaurantBooking
            {
                BookingId = source.ReservationCode.Split('-')[1],
                RestaurantName = source.VenueName,
                BookingTime = source.SlotDateTime
            };
        }
    }
}
