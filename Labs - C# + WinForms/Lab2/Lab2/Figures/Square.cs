using Lab2.Interfaces;

namespace Lab2.Figures;

public class Square : GeometricFigure, IPrint
{
    public double Side { get; set; }

    public Square(double side)
    {
        Side = side;
    }

    public override double? Result() => Math.Pow(Side,2);
    

    public override string ToString() => $"Квадрат стороной: {Side}\n" +
                                         $"Квадрат площадью: {Result()}\n";
    
    
    public override void Print() => Console.WriteLine(ToString());
}