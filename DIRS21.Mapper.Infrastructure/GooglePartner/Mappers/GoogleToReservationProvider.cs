using DIRS21.Mapper.Domain.Exceptions;
using DIRS21.Mapper.Domain.Interfaces;
using DIRS21.Mapper.Domain.Models;
using DIRS21.Mapper.Infrastructure.Google.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.Google.Mapping
{
    public class GoogleToReservationProvider : IMappingProvider<GoogleReservation, Reservation>, IMappingProvider<Reservation, GoogleReservation>
    {
        public Reservation Map(GoogleReservation source)
        {
            try
            {
                if (source is null) throw new MappingValidationException("Source cannot be null");
                return new Reservation
                {
                    Id = source.Id,
                    GuestName = source.CustomerName,
                    CheckIn = source.CheckInDate
                    // Map other properties as needed
                };
            }
            catch (InvalidMappingException)
            {

                throw new ArgumentException("Invalid source type for mapping.");
            }

        }
        public GoogleReservation Map(Reservation source)
        {
            try
            {
                if (source is null) throw new MappingValidationException("Source cannot be null");
                if (string.IsNullOrEmpty(source.Id)) throw new MappingValidationException("Reservation ID is required");
                return new GoogleReservation
                {
                    Id = source.Id,
                    CustomerName = source.GuestName,
                    CheckInDate = source.CheckIn
                    // Map other properties as needed
                };
            }
            catch (InvalidMappingException)
            {

                throw new ArgumentException("Invalid source type for mapping.");
            }

        }
    }
}
