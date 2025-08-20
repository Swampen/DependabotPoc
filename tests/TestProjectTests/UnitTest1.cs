using DependabotPoc;
using Xunit;

namespace TestProjectTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var testModel = new TestModel("aaa", 2);

        testModel = testModel with { Foo = "bbb" };
        
        Assert.Equal("bbb", testModel.Foo);
    }
}