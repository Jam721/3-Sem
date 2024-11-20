using Lab3.Interfaces;

namespace Lab3.Figures;

public class Circle : GeometricFigure, IPrint, IComparable
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double? Result() => Math.PI*Math.Pow(Radius, 2);
    
    public override string ToString() => $"Круг радиусом: {Radius}\n" +
                                         $"Круг площадью: {Result()}\n";
    
    public override void Print() => Console.WriteLine(this.ToString());
    
    public override int CompareTo(object? obj)
    {
        if (obj is GeometricFigure figure)
        {
            return Convert.ToDouble(Result()).CompareTo(Convert.ToDouble(figure.Result()));
        }
        throw new ArgumentException("Объект не является фигурой");
    }

}