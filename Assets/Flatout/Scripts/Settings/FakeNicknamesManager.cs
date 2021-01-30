using Gamebase.Miscellaneous;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Раздает случайные ники из заданного файла
    /// </summary>
    [CreateAssetMenu(fileName = "FakeNickNamesManager", menuName = "Flatout/Static/FakeNickNamesManager")]
    public class FakeNicknamesManager : StaticScriptableObject<FakeNicknamesManager>
    {
        /// <summary>
        /// Файл с никами, разделенный символом переноса строки
        /// </summary>
        [SerializeField]
        private TextAsset NickNamesFile;
        /// <summary>
        /// Енумератор ников
        /// </summary>
        IEnumerator<string> NickNames;

        /// <summary>
        /// Достает ники из файла
        /// </summary>
        public void InitNickNames()
        {
            NickNames = NickNamesFile.text.Split('\n').Shuffle().GetEnumerator();
        }
        /// <summary>
        /// Освобождает хранимую память 
        /// Так как раздача ников применяется чаще всего один раз за матч, надобность хранить список в файле все оставшееся время, отпадает
        /// </summary>
        public void FLushMemory()
        {
            NickNames.Dispose();
            NickNames = default;
        }
        /// <summary>
        /// Возвращает очередной ник из списка
        /// </summary>
        /// <returns>Случайный ник</returns>
        public string GetNickName()
        {
            if (NickNames == default)
                InitNickNames();

            NickNames.MoveNext();
            return NickNames.Current;
        }
    }

}
