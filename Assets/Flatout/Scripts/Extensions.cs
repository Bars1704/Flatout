using System.Linq;
using System.Collections.Generic;
public static class Extensions
{
    /// <summary>
    /// Перемешивает значения в коллекции
    /// </summary>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        => list.OrderBy(x => UnityEngine.Random.value);
}
