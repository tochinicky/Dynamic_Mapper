using DIRS21.Mapper.Domain.Interfaces;
using DIRS21.Mapper.Domain.Models;
using DIRS21.Mapper.Infrastructure.Google.Mapping;
using DIRS21.Mapper.Infrastructure.Google.Models;

namespace DIRS21.Mapper.Infrastructure.Tests.Google
{
    public class GoogleReservationProviderTests
    {

        [Fact]
        public void MapValidGoogleReservationConvertsToDIRS21Model()
        {
            //Arrange
            var provider = new GoogleToReservationProvider();

            var googleReservation = new GoogleReservation
            {
                Id = "G-123",
                CustomerName = "Tochukwu",
                CheckInDate = new DateTime(2025, 10, 15, 0, 0, 0, DateTimeKind.Utc),
            };

            //Act
            var result = provider.Map(googleReservation);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("G-123", result.Id);
            Assert.Equal("Tochukwu", result.GuestName);
            Assert.Equal(new DateTime(2025, 10, 15, 0, 0, 0, DateTimeKind.Utc), result.CheckIn);
        }
        [Fact]
        public void MapValidDIRS21ReservationConvertsToGoogleModel()
        {
            //Arrange
            var provider = new GoogleToReservationProvider();

            var googleReservation = new Reservation
            {
                Id = "G-123",
                GuestName = "Tochukwu",
                CheckIn = new DateTime(2025, 10, 15, 0, 0, 0, DateTimeKind.Utc),
            };

            //Act
            var result = provider.Map(googleReservation);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("G-123", result.Id);
            Assert.Equal("Tochukwu", result.CustomerName);
            Assert.Equal(new DateTime(2025, 10, 15, 0, 0, 0, DateTimeKind.Utc), result.CheckInDate);
        }

    }
}
