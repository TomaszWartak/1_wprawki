using ConsoleApp;
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Calculator calculator = new();
        int result = calculator.Add(1,2);
        Assert.Equal( 3, result );
    }
}