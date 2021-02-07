using Gamebase.Miscellaneous;
using System;
using System.Linq;
using UnityEngine;


namespace Flatout
{
    /// <summary>
    /// Игровые данные игрока
    /// </summary>
    public class PlayerAvatar
    {
        public static PlayerAvatar Instance;
        //TODO: сделать синглтон отдельным классом, от которого можно будет унаследоваться
        static PlayerAvatar()
        {
            Instance = new PlayerAvatar();
            Instance.Initialize();
        }
        /// <summary>
        /// Событие изменения количества опыта
        /// </summary>
        public Action<int> onExperienceChanged;
        /// <summary>
        /// Количество опыта
        /// </summary>
        public int Experience { get; private set; }
        /// <summary>
        /// Уровень
        /// </summary>
        public int Level { get; private set; }
        /// <summary>
        /// Никнейм
        /// </summary>
        public string Nickname { get; private set; }

        /// <summary>
        /// Текущий уровень сложности, выбранный игроком
        /// </summary>
        public HardnessLevel hardnessLevel { get; set; }
        /// <summary>
        /// Установка никнейма
        /// </summary>
        /// <param name="newName"></param>
        public void SetNickName(string newName)
        {
            Nickname = newName;
            PlayerPrefs.SetString(StringLiterals.NickNamePref, newName);
        }
        /// <summary>
        /// Получение нового уровня
        /// </summary>
        public void GetNewLevel()
        {
            Level++;
        }

        /// <summary>
        /// Максимально доступная машинка игрока
        /// </summary>
        public CarTier MaxAvailableCar => GlobalSettings.Instance.LevelsSettings.GetActualCar(Instance.Level);
        /// <summary>
        /// Текущая машинка игрока
        /// </summary>
        public CarTier ActualCar
            => GlobalSettings.Instance.LevelsSettings.LevelForOpeningCar
                    .FirstOrDefault(x => x.Value.name == PlayerPrefs.GetString("CarTier")).Value
            ?? MaxAvailableCar;

        /// <summary>
        /// Сохраняет выбор машинки
        /// </summary>
        /// <param name="car"></param>
        public void SetSelectedCarTer(CarTier car)
        {
            PlayerPrefs.SetString("CarTier", car.name);
        }
        /// <summary>
        /// Добавление опыта игроку
        /// </summary>
        /// <param name="xpAmount">Количество добавляемого опыта</param>
        public void AddXP(int xpAmount)
        {
            Experience += xpAmount;
            PlayerPrefs.SetInt(StringLiterals.ExperiencePref, Experience);
            var actualLevel = GlobalSettings.Instance.LevelsSettings.CheckLevel(Experience);
            while (actualLevel > Level)
            {
                GetNewLevel();
            }
            onExperienceChanged?.Invoke(Experience);
        }
        /// <summary>
        /// Инициализация
        /// </summary>
        void Initialize()
        {
            Experience = PlayerPrefs.GetInt(StringLiterals.ExperiencePref, 0);
            Level = PlayerPrefs.GetInt(StringLiterals.LevelPref, 0);
            Nickname = PlayerPrefs.GetString(StringLiterals.NickNamePref, GlobalSettings.Instance.DefaultNickName);


            var hardnessLevelName = PlayerPrefs.GetString("HardnessLevel", string.Empty);

            hardnessLevel = hardnessLevelName == string.Empty ?
                GlobalSettings.Instance.hardnessLevels.First() :
                GlobalSettings.Instance.hardnessLevels.FirstOrDefault(x => x.LevelName == hardnessLevelName);
        }
    }
}