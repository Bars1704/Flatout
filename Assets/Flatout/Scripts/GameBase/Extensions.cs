using System.Linq;
using System.Collections.Generic;
using UnityEngine;
public static class Extensions
{
    /// <summary>
    /// Перемешивает значения в коллекции
    /// </summary>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        => collection.OrderBy(x => Random.value);

    /// <summary>
    /// Возвращает случайный елемент из коллекции
    /// </summary>
    /// <typeparam name="T">Тип елеметов коллекции</typeparam>
    /// <param name="collection">Коллекция</param>
    /// <returns>Случайный елемент коллекции</returns>
    public static T GetRandomElement<T>(this IEnumerable<T> collection)
     => collection.ElementAt(Random.Range(0, collection.Count()));

    /// <summary>
    /// Возвращает случайный елемент из коллекции с заданными вероятностями
    /// </summary> 
    /// <typeparam name="T">Тип возвращаемого обьекта</typeparam>
    /// <param name="valuePairs">Словарь, в котором ключ - обьект, а значение - вероятность его выпадения</param>
    /// <returns>Случайное значение из коллекции</returns>
    public static T GetRandomWithProbabilities<T>(this Dictionary<T, float> valuePairs)
    {
        var valuePairsClone = new Dictionary<T, float>(valuePairs);

        var sum = valuePairsClone.Sum(x => x.Value);
        for (int i = 0; i < valuePairsClone.Count; i++)
            valuePairsClone[valuePairsClone.ElementAt(i).Key] /= sum;

        for (int i = 1; i < valuePairsClone.Count; i++)
            valuePairsClone[valuePairsClone.ElementAt(i).Key] 
                += valuePairsClone[valuePairsClone.ElementAt(i - 1).Key];

        var seed = Random.value;

        return valuePairsClone.First(x => x.Value > seed).Key;
    }
}
