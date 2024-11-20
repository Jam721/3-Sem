using System.Collections;
using Lab3.Additional;
using Lab3.Figures;

namespace Lab3;

class Program
{
    public static void Main()
    {
        ArrayList arrayList = new ArrayList();
        arrayList.Add(new Rectangle(12, 2));
        arrayList.Add(new Square(7));
        arrayList.Add(new Circle(3));
        
        foreach (var el in arrayList)
        {
            Console.WriteLine(el);
        }
        
        arrayList.Sort();
        foreach (var el in arrayList)
        {
            Console.WriteLine(el);
        }

        var figures = new List<GeometricFigure>
        {
            new Rectangle(12, 2),
            new Square(7),
            new Circle(3)
        };
        
        figures.Sort();
        foreach (var figure in figures)
        {
            figure.Print();
        }

        var matrix = new SparseMatrix<GeometricFigure>();
        
        matrix.Set(0,0,0, new Circle(8));
        matrix.Set(1,2,3, new Rectangle(4,2));
        matrix.Set(2,4,5, new Square(4));

        matrix.Get(0,0,0).Print();
        
        matrix.Print();

        var simpleStack = new SimpleStack<GeometricFigure>();
        simpleStack.Add(new Circle(2));
        simpleStack.Add(new Rectangle(12,3));
        simpleStack.Add(new Square(7));
        simpleStack.Pop();
        foreach (var eFigure in simpleStack)
        {
            Console.WriteLine(eFigure);
        }
    }
}