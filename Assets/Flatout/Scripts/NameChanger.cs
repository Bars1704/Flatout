using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(InputField))]
public class NameChanger : MonoBehaviour
{
    private InputField inputField;
    private PlayerAvatar playerAvatar;
    private void Start()
    {
        playerAvatar = PlayerAvatar.Instance;
        inputField = GetComponent<InputField>();
        inputField.text = playerAvatar.Nickname;
    }
    public void ChangeName(string newName)
    => playerAvatar.SetNickName(newName);
}
