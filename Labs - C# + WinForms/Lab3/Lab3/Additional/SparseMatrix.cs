using Lab3.Interfaces;

namespace Lab3.Additional;

public class SparseMatrix<T> : IPrint
{
    public readonly Dictionary<(int, int, int), T> Matrix 
        = new Dictionary<(int, int, int), T>();

    public void Set(int x, int y, int z, T value)
    {
        Matrix[(x, y, z)] = value;
    }

    public T Get(int x, int y, int z)
    {
        return Matrix[(x, y, z)];
    }

    public override string ToString()
    {
        string result = "";

        foreach (var item in Matrix)
        {
            result += $"Точка ({item.Key.Item1}, {item.Key.Item2}, {item.Key.Item3}) -> {item.Value?.ToString()}\n";
        }

        return result;
    }


    public void Print() => Console.WriteLine(ToString());
}