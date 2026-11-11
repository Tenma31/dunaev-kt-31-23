using WebApplication1.Models;
using Xunit;

namespace WebApplication1.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_True()
        {
            var testGroup = new Groups
            {
                GroupName = "09.01.٢٢"
            };
            var result = testGroup.IsValidGroupName();
            Assert.True(result);
        }

        [Fact]
        public void IsValidGroupName_False()
        {
            var testGroup = new Groups
            {
                GroupName = "Просто текст"
            };
            var result = testGroup.IsValidGroupName();
            Assert.True(result);
        }
    }
}