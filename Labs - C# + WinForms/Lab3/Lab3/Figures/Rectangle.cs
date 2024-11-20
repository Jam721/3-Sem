using Lab3.Interfaces;

namespace Lab3.Figures;

public class Rectangle : GeometricFigure, IPrint, IComparable
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override double? Result() => Width * Height;

    public override string ToString() => $"Прямоугольник шириной: {Width}\n" +
                                         $"Прямоугольник высотой: {Height}\n" +
                                         $"Прямоугольник площадью: {Result()}\n";
    
    public override void Print() => Console.WriteLine(ToString());
    
    
    public override int CompareTo(object? obj)
    {
        if (obj is GeometricFigure figure) 
            return Convert.ToDouble(Result()).CompareTo(Convert.ToDouble(figure.Result()));
        

        throw new ArgumentException("Объект не является фигурой");
    }

}