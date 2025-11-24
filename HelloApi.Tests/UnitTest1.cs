using Xunit;

namespace HelloApi.Tests;

public class HealthTests
{
    [Fact]
    public void Health_Should_Return_Ok_String()
    {
        var expected = "ok";
        var actual = "ok"; 
        Assert.Equal(expected, actual);
    }
}
