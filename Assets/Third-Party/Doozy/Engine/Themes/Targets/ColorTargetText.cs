// Copyright (c) 2015 - 2020 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using Doozy.Engine.Utils;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Doozy.Engine.Themes
{
    /// <inheritdoc />
    /// <summary>
    /// Used by the Theme Manager to update the color value of a Text component
    /// </summary>
    [AddComponentMenu(MenuUtils.ColorTargetText_AddComponentMenu_MenuName, MenuUtils.ColorTargetText_AddComponentMenu_Order)]
    [DefaultExecutionOrder(DoozyExecutionOrder.COLOR_TARGET_TEXT)]
    public class ColorTargetText : ThemeTarget
    {
        #region UNITY_EDITOR

#if UNITY_EDITOR
        [MenuItem(MenuUtils.ColorTargetText_MenuItem_ItemName, false, MenuUtils.ColorTargetText_MenuItem_Priority)]
        private static void CreateComponent(MenuCommand menuCommand) { DoozyUtils.AddToScene<ColorTargetText>(MenuUtils.ColorTargetText_GameObject_Name, false, true); }
#endif

        #endregion

        #region Public Variables

        /// <summary> Target Text component </summary>
        public Text Text;

        /// <summary> Determines if the target color preserves its alpha value when the theme variant changes </summary>
        public bool OverrideAlpha;

        /// <summary> Alpha value for the target color when the theme variant changes (when OverrideAlpha is true) </summary>
        public float Alpha;

        #endregion

        #region Private Variables

        private float m_previousAlphaValue = -1;

        #endregion

        #region Unity Methods

        private void Update()
        {
            if (!OverrideAlpha) return;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Alpha == m_previousAlphaValue) return;
            SetAlpha(Alpha);
            m_previousAlphaValue = Alpha;
        }

        #endregion

        #region Public Methods

        /// <summary> Method used by the ThemeManager when the active variant or selected theme have changed </summary>
        /// <param name="theme"> Target theme </param>
        public override void UpdateTarget(ThemeData theme)
        {
            if (Text == null) return;
            if (theme == null) return;
            base.UpdateTarget(theme);
            if (ThemeId == Guid.Empty) return;
            if (PropertyId == Guid.Empty) return;
            if (theme.ActiveVariant == null) return;
            Text.color = theme.ActiveVariant.GetColor(PropertyId);
            if (!OverrideAlpha) return;
            SetAlpha(Alpha);
        }

        /// <summary> Sets the Alpha value for the target component </summary>
        /// <param name="value"> Alpha value </param>
        public void SetAlpha(float value)
        {
            if (Text == null) return;
            Alpha = value;
            Color color = Text.color;
            Text.color = new Color()
            {
                r = color.r,
                g = color.g,
                b = color.b,
                a = Alpha
            }; ;
        }

        #endregion

        #region Private Methods

        private void Reset()
        {
            ThemeId = Guid.Empty;
            VariantId = Guid.Empty;
            PropertyId = Guid.Empty;

            UpdateReference();
        }

        private void UpdateReference()
        {
            if (Text == null)
                Text = GetComponent<Text>();
        }

        #endregion
    }
}