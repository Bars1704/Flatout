namespace Gamebase.Miscellaneous
{
    /// <summary>
    /// Класс который хранит в себе строковые литералы, раскиданные по всему проекту в одном месте - позволяет избежать ошибки опечатки и надобности менять одно и то же в нескольких местах при изменении в одном
    /// </summary>
    public static class StringLiterals
    {
        public const string ExperiencePref = "PlayerScore";
        public const string LevelPref = "PlayerLevel";
        public const string NickNamePref = "PlayerName";
        //TODO: придумать, как сделать расширяемым из редактора (кодогенерация?)
        //TODO: сделать разделы (префсы\материалы\пути\ивенты)
    }
}
