using Lab3.Interfaces;

namespace Lab3.Figures;

public class Square : GeometricFigure, IPrint, IComparable
{
    public double Side { get; set; }

    public Square(double side)
    {
        Side = side;
    }

    public override double? Result() => Side*Side;
    
    public override string ToString() => $"Квадрат стороной: {Side}\n" +
                                         $"Квадрат площадью: {Result()}\n";
    
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