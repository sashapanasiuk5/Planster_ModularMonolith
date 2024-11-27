namespace Infrastructure.Utils;

public static class LinqRecursiveHelper
{
    public static IEnumerable<T> Traverse<T>(this T item, Func<T, T> childSelector)
    {
        var stack = new Stack<T>(new T[] { item });

        while (stack.Any())
        {
            var next = stack.Pop();
            if (next != null)
            {
                yield return next;
                stack.Push(childSelector(next));
            }
        }
    }
    public static IEnumerable<T> Traverse<T>(this T item, Func<T, IEnumerable<T>> childSelector)
    {
        var stack = new Stack<T>(new T[] { item });

        while (stack.Any())
        {
            var next = stack.Pop();
            yield return next;
            foreach (var child in childSelector(next))
            {
                stack.Push(child);
            }
        }
    }
    public static IEnumerable<T> Traverse<T>(this IEnumerable<T> items,
        Func<T, IEnumerable<T>> childSelector)
    {
        var stack = new Stack<T>(items);
        while (stack.Any())
        {
            var next = stack.Pop();
            yield return next;
            foreach (var child in childSelector(next))
                stack.Push(child);
        }
    }
}