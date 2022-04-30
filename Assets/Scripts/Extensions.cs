using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Extension methods.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Executes an <see cref="Action{T}"/> on a collection of <see cref="{T}"/>.
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    /// <param name="self">The collection to act upon.</param>
    /// <param name="action">The action to execute.</param>
    public static void Execute<T>(this IEnumerable<T> self, Action<T> action)
    {
        foreach (T item in self)
        {
            action(item);
        }
    }
}
