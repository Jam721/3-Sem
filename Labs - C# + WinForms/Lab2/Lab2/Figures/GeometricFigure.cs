using Lab2.Interfaces;

namespace Lab2.Figures;

public abstract class GeometricFigure : IPrint
{
    public abstract double? Result();

    public abstract void Print();
}