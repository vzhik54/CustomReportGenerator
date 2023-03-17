namespace SumProject;

public class Program
{
    /// <summary>
    /// Главный метод
    /// </summary>
    public static void Main()
    {
        Console.WriteLine(Sum(2, 5));
    }
    /// <summary>
    /// Метод сложения
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Sum(int a, int b) => a + b;
}
