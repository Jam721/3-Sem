using Lab3.Interfaces;

namespace Lab3.Figures;

public abstract class GeometricFigure : IPrint, IComparable
{
    public abstract double? Result();

    public abstract void Print();


    public abstract int CompareTo(object? obj);
}