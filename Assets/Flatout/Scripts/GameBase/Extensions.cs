using System.Collections.Generic;
using System.Linq;
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
    /// <typeparam name="T">Тип случайного значения</typeparam>
    /// <param name="collection">Коллекция</param>
    /// <returns>Случайный елемент коллекции</returns>
    public static T GetRandomElement<T>(this IEnumerable<T> collection)
     => collection.ElementAt(Random.Range(0, collection.Count()));


    /// <summary>
    /// Переводит шанс выпадения елементов к промежутку 0-1
    /// </summary>
    /// <typeparam name="T">Тип случайного значения</typeparam>
    /// <param name="valuePairs">Словарь, в котором ключ - обьект, а значение - вероятность его выпадения</param>
    private static void NormalizeRandomValuesProbabilities<T>(this Dictionary<T, float> valuePairs)
    {
        var sum = valuePairs.Sum(x => x.Value);
        for (int i = 0; i < valuePairs.Count; i++)
            valuePairs[valuePairs.ElementAt(i).Key] /= sum;
    }
    /// <summary>
    /// Приводит вероятности штрезку вероятностей
    /// </summary>
    /// <typeparam name="T">Тип случайного значения</typeparam>
    /// <param name="valuePairs">Словарь, в котором ключ - обьект, а значение - вероятность его выпадения</param>
    public static void ConvertToRandomLineSegment<T>(this Dictionary<T, float> valuePairs)
    {
        valuePairs.NormalizeRandomValuesProbabilities();
        for (int i = 1; i < valuePairs.Count; i++)
            valuePairs[valuePairs.ElementAt(i).Key]
                += valuePairs[valuePairs.ElementAt(i - 1).Key];
    }

    /// <summary>
    /// Возвращает случайный елемент из коллекции с заданными вероятностями
    /// </summary> 
    /// <typeparam name="T">Тип случайного значения</typeparam>
    /// <param name="valuePairs">Словарь, в котором ключ - обьект, а значение - вероятность его выпадения</param>
    /// <returns>Случайное значение из коллекции</returns>
    public static T GetRandomWithProbabilities<T>(this Dictionary<T, float> valuePairs)
    {
        var valuePairsClone = new Dictionary<T, float>(valuePairs);
        valuePairsClone.ConvertToRandomLineSegment();
        return valuePairsClone.GetRandomFromLineSegment();
    }
    /// <summary>
    /// Возвращает случайное значение с отрезка вероятностей
    /// </summary>
    /// <typeparam name="T">Тип случайного значения</typeparam>
    /// <param name="valuePairs">Отрезок вероятностей, где Value - точка на отрезке</param>
    /// <returns>Случайное значение из отрезка</returns>
    public static T GetRandomFromLineSegment<T>(this Dictionary<T, float> valuePairs)
    {
        var seed = Random.value;
        return valuePairs.First(x => x.Value > seed).Key;
    }
}
