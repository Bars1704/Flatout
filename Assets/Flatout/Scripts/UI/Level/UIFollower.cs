using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flatout
{
    /// <summary>
    /// Двигается за игроком
    /// </summary>
    public class UIFollower : MonoBehaviour
    {
        /// <summary>
        /// Обьект, за которым нужно следовать
        /// </summary>
        [HideInInspector]
        public Transform Target;
        /// <summary>
        /// Смещение от точки за которой нужно следовать
        /// </summary>
        protected Vector2 offset;
        /// <summary>
        /// Основная камера, с помощью ее производится расчет позиции обьекта на канвасе
        /// </summary>
        private Camera mainCamera;
        /// <summary>
        /// Канвас, на котором необходимо разместить компонент
        /// </summary>
        private Canvas canvas;
        /// <summary>
        ///  <see cref="RectTransform"/> компонент основного канваса
        /// </summary>
        private RectTransform canvasRect;
        private void Start()
        {
            InitFollower();
        }
        /// <summary>
        /// Инициализация
        /// </summary>
        protected void InitFollower()
        {
            mainCamera = Camera.main;
            canvas = UIFollowersCanvas.Instance.FollowersCancas;
            transform.SetParent(canvas.transform);
            canvasRect = canvas.GetComponent<RectTransform>();
        }
        void Update()
        {
            SetPosition();
        }
        /// <summary>
        /// Устанавливает обьект в нужную позицию
        /// </summary>
        private void SetPosition()
        {
            transform.position = WorldToCanvasPosition(Target.position) + offset;
        }
        /// <summary>
        /// Вычисляет точку на канвасе, на которую нужно разместить обьект
        /// </summary>
        /// <param name="position">Мировые координаты обьекта, за которым нужно следовать</param>
        /// <returns>Координаты холста, на которые нужно разместить обьект</returns>
        private Vector2 WorldToCanvasPosition(Vector3 position)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(mainCamera, position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out Vector2 result);
            return canvas.transform.TransformPoint(result);
        }
    }

}
