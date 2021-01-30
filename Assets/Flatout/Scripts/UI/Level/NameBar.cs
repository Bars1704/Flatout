using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flatout
{
    [RequireComponent(typeof(Text))]
    public class NameBar : UIFollower
    {
        public string NickName
        {
            get => thisText.text ?? string.Empty;
            set
            {
                if (thisText == default)
                    thisText = GetComponent<Text>();

                thisText.text = value;
            }
        }

        Text thisText;
        private new void Start()
        {
            thisText = GetComponent<Text>();
            base.Start();
        }
    }
}

