using UnityEngine;
using UnityEngine.UI;

public class BoosterBar : MonoBehaviour
{
    Image boosterBarFillerSprite;
    private void Start()
    {
        boosterBarFillerSprite = transform.GetChild(0).GetComponent<Image>();
    }
    public void ShowBooster(float booster, float maxBooster)
    {
        boosterBarFillerSprite.fillAmount = booster / maxBooster;
    }
}
