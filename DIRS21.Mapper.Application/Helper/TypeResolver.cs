using DIRS21.Mapper.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIRS21.Mapper.Infrastructure.Google.Models;
using DIRS21.Mapper.Infrastructure.RestaurantPartner.Models;
using DIRS21.Mapper.Infrastructure.HotelPartner.Models;
using DIRS21.Mapper.Domain.Exceptions;

namespace DIRS21.Mapper.Application.Helper
{
    public static class TypeResolver
    {
        private static readonly Dictionary<string, Type> _typeCache = new()
        {
            //DIRS21 Internal Models
            ["Model.Reservation"] = typeof(Reservation),
            ["Model.RestaurantBooking"] = typeof(RestaurantBooking),
            ["Model.HotelProperty"] = typeof(HotelProperty),
            ["Model.Room"] = typeof(Room),
          

            //Google Partner Models
            ["Google.GoogleReservation"] = typeof(GoogleReservation),

            //Restaurant Partner Models
            ["Restaurant.RestaurantReservation"] = typeof(RestaurantReservation),

            //Hotel Partner Models
            ["Hotel.HotelReservation" ] = typeof(HotelReservation),
            ["Hotel.HotelInventory"] = typeof(HotelInventory),
            ["Hotel.HotelRoomType"] = typeof(HotelRoomType),

        };
        public static Type ResolveType(string typeName)
        {
            if(string.IsNullOrWhiteSpace(typeName)) throw new ArgumentException("Type name cannot be empty", nameof(typeName));

            if (_typeCache.TryGetValue(typeName, out var type))
            {
                return type;
            }

            throw new TypeMismatchException($"Type '{typeName}' is not registered. " + "Supported types: " + string.Join(", ", _typeCache.Keys));
        }
    }
}
