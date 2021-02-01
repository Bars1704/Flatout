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
        private void Start()
        {
            offset = offset = new Vector2(0, 130);
            thisText = GetComponent<Text>();
            InitFollower();
        }
    }
}

