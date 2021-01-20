using Gamebase.Miscellaneous;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAvatar
{
    public static PlayerAvatar Instance;
    //TODO: сделать синглтон отдельным классом, от которого можно будет унаследоваться
    static PlayerAvatar()
    {
        Instance = new PlayerAvatar();
        Instance.Initialize();
    }
    public Action<int> onExperienceChanged;
    public int Experience { get; private set; }
    public int Level { get; private set; }
    public string Nickname { get; private set; }
    public void SetNickName(string newName)
    {
        Nickname = newName;
        PlayerPrefs.SetString(StringLiterals.NickNamePref, newName);
    }
    public void GetNewLevel()
    {
        Level++;
    }
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
    void Initialize()
    {
        Experience = PlayerPrefs.GetInt(StringLiterals.ExperiencePref, 0);
        Level = PlayerPrefs.GetInt(StringLiterals.LevelPref, 0);
        Nickname = PlayerPrefs.GetString(StringLiterals.NickNamePref, GlobalSettings.Instance.DefaultNickName);
    }
}
