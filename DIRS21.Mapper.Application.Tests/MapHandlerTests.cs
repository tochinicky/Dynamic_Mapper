using DIRS21.Mapper.Application.Helper;
using DIRS21.Mapper.Application.Mapping;
using DIRS21.Mapper.Domain.Exceptions;
using DIRS21.Mapper.Domain.Interfaces;
using DIRS21.Mapper.Domain.Models;
using Moq;

namespace DIRS21.Mapper.Application.Tests
{
    public class MapHandlerTests
    {
        private readonly MapHandler _mapHandler;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        public MapHandlerTests()
        {
            _serviceProviderMock = new Mock<IServiceProvider>();
            _mapHandler = new MapHandler(_serviceProviderMock.Object);
        }
        [Fact]
        public void MapNullDataThrowsArgumentNullException()
        {
            // Arrange
            string sourceType = "SourceType";
            string targetType = "TargetType";
            object? data = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _mapHandler.Map(data!, sourceType, targetType));
            Assert.Equal("data", exception.ParamName);
        }
        [Fact]
        public void MapEmptySourceTypeThrowsArgumentNullException()
        {
            // Arrange
            object data = new object();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _mapHandler.Map(data, "", "TargetType"));
            Assert.Equal("sourceType", exception.ParamName);
        }
        [Fact]
        public void MapEmptyTargetTypeThrowsArgumentNullException()
        {
            // Arrange
            object data = new object();
            string sourceType = "SourceType";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => _mapHandler.Map(data, sourceType, ""));
            Assert.Equal("targetType", exception.ParamName);
        }
        [Fact]
        public void MapTypeMismatchThrowsTypeMismatchException()
        {
            // Arrange
            var ex = Assert.Throws<TypeMismatchException>(() => _mapHandler.Map(new object(), "SourceType.Type", "Google.Reservation"));
            Assert.Contains("SourceType.Type", ex.Message, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("Supported types", ex.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}