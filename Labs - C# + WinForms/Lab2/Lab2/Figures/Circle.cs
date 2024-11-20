using Lab2.Interfaces;

namespace Lab2.Figures;

public class Circle : GeometricFigure, IPrint
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double? Result() => Math.PI*Math.Pow(Radius, 2);

    public override string ToString() => $"Круг радиусом: {Radius}\n" +
                                         $"Круг площадью: {Result()}\n";
    
    public override void Print() => Console.WriteLine(ToString());
}