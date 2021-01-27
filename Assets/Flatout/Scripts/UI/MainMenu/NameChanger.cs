using UnityEngine;
using UnityEngine.UI;

namespace Flatout
{
    /// <summary>
    /// Компонент смены никнейма игрока
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class NameChanger : MonoBehaviour
    {
        /// <summary>
        /// Поле ввода имени
        /// </summary>
        private InputField inputField;
        private void Start()
        {
            inputField = GetComponent<InputField>();
            inputField.text = PlayerAvatar.Instance.Nickname;
        }
        /// <summary>
        /// Смена никнейма
        /// </summary>
        /// <param name="newName">Новый никнейм</param>
        public void ChangeName(string newName)
        => PlayerAvatar.Instance.SetNickName(newName);
    }
}