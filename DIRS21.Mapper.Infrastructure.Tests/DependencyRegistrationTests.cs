using DIRS21.Mapper.Domain.Interfaces;
using DIRS21.Mapper.Domain.Models;
using DIRS21.Mapper.Infrastructure.Google.Models;
using DIRS21.Mapper.Infrastructure.RestaurantPartner.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIRS21.Mapper.Infrastructure.Tests
{
    public class DependencyRegistrationTests
    {
        [Fact]
        public void ScannerRegistersMappersFromAllAssemblies()
        {
            var services = new ServiceCollection();

            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IMappingProvider<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            var provider = services.BuildServiceProvider();

            //Assert
            Assert.NotNull(provider.GetService<IMappingProvider<Reservation, GoogleReservation>>());
            Assert.NotNull(provider.GetService<IMappingProvider<RestaurantBooking, RestaurantReservation>>());

            //verify the mapping works
            var testReservation = new Reservation
            {
                Id = "R-123",
                GuestName = "John Doe",
                CheckIn = DateTime.Now
            };
            var googleMapper = provider.GetService<IMappingProvider<Reservation, GoogleReservation>>() ?? throw new InvalidOperationException("GoogleReservation mapper not registered");
            var googleResult = googleMapper.Map(testReservation);
            Assert.Equal(testReservation.Id, googleResult.Id);
        }
    }
}
