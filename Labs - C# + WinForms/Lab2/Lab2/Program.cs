using Lab2.Figures;

namespace Lab2;

public class Program
{
    public static void Main()
    {
        var figures = new List<GeometricFigure>
        {
            new Rectangle(15, 17),
            new Square(8),
            new Circle(14)
        };

        foreach (var figure in figures)
        {
            figure.Print();
        }
    }
}