using WebApplication1.Models;
using Xunit;

namespace WebApplication1.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_KT3120_True()
        {
            // Arrange (Подготовка)
            var testGroup = new Groups
            {
                GroupName = "КТ-31-20"
            };

            // Act (Действие)
            var result = testGroup.IsValidGroupName();

            // Assert (Проверка)
            Assert.True(result);
        }

        [Fact]
        public void IsValidGroupName_InvalidName_False()
        {
            // Arrange
            var testGroup = new Groups
            {
                GroupName = "Просто текст"
            };

            // Act
            var result = testGroup.IsValidGroupName();

            // Assert
            Assert.False(result);
        }
    }
}