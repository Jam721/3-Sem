using Lab2.Interfaces;

namespace Lab2.Figures;

public class Rectangle : GeometricFigure, IPrint
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
    
    public override void Print() => Console.WriteLine(this.ToString());
}