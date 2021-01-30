﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flatout
{
    [RequireComponent(typeof(Sprite))]
    public class HealhBar : UIFollower
    {
        Image healtBarFillerSprite;
        private void Start()
        {
            InitFollower();
            healtBarFillerSprite = transform.GetChild(0).GetComponent<Image>();
        }
        public void ShowHealth(int health, int maxHealth)
        {
            healtBarFillerSprite.fillAmount = (float)health / maxHealth;
        }
    }
}