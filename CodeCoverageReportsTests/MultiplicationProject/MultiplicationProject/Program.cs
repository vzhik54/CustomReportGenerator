using SubProject;
namespace MultiplicationProject;

public class Program
{
    /// <summary>
    /// Главный метод с логикой
    /// </summary>
    public static void Main()
    {
        if (Multiply(2, 5) > 9)
            GetInfo.Print("Good");
        else
            Console.WriteLine("Not Good");
    }

    /// <summary>
    /// Метод Умножения
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int Multiply(int a, int b) => a * b;
}
