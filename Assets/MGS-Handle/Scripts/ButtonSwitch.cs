﻿/*************************************************************************
 *  Copyright (C), 2016-2017, Mogoson Tech. Co., Ltd.
 *------------------------------------------------------------------------
 *  File         :  ButtonSwitch.cs
 *  Description  :  Define button switch.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  3/31/2016
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace Developer.Handle
{
    [AddComponentMenu("Developer/Handle/ButtonSwitch")]
    [RequireComponent(typeof(Collider))]
    public class ButtonSwitch : MonoBehaviour
    {
        #region Property and Field
        /// <summary>
        /// Is enable to control.
        /// </summary>
        public bool isEnable = true;

        /// <summary>
        /// Switch down offset.
        /// </summary>
        public float downOffset = 1;

        /// <summary>
        /// Self lock on switch down.
        /// </summary>
        public bool selfLock = false;

        /// <summary>
        /// Self lock offset percent.
        /// </summary>
        [Range(0, 1)]
        public float lockPercent = 0.5f;

        /// <summary>
        /// High light on switch down.
        /// </summary>
        public bool highLight = false;

        /// <summary>
        /// High light target renderer.
        /// </summary>
        public Renderer lightRender;

        /// <summary>
        /// High light material.
        /// </summary>
        public Material lightMaterial;

        /// <summary>
        /// Button switch is down state.
        /// </summary>
        public bool IsDown { protected set; get; }

        /// <summary>
        /// Current offset base start position.
        /// </summary>
        public float CurrentOffset { protected set; get; }

        /// <summary>
        /// Start position.
        /// </summary>
        public Vector3 StartPosition { private set; get; }

        /// <summary>
        /// Local move axis.
        /// </summary>
        protected Vector3 MoveAxis
        {
            get
            {
                var forward = transform.forward;
                if (transform.parent)
                    forward = transform.parent.InverseTransformDirection(forward);
                return forward;
            }
        }

        /// <summary>
        /// Button switch up event.
        /// </summary>
        public HandleEvent OnSwitchUp;

        /// <summary>
        /// Button switch down event.
        /// </summary>
        public HandleEvent OnSwitchDown;

        /// <summary>
        /// Button switch lock event.
        /// </summary>
        public HandleEvent OnSwitchLock;

        /// <summary>
        /// Default material of lightRender.
        /// </summary>
        protected Material defaultMat;

        /// <summary>
        /// Current self lock state.
        /// </summary>
        protected bool isLock;
        #endregion

        #region Protected Method
        protected virtual void Awake()
        {
            StartPosition = transform.localPosition;

            if (lightRender)
                defaultMat = lightRender.material;
        }

        /// <summary>
        /// Response mouse left button down.
        /// </summary>
        protected virtual void OnMouseDown()
        {
            if (!isEnable)
                return;

            IsDown = true;
            CurrentOffset = downOffset;
            TranslateButton(CurrentOffset);

            if (highLight)
                lightRender.material = lightMaterial;

            if (OnSwitchDown != null)
                OnSwitchDown();
        }

        /// <summary>
        /// Response mouse left button up.
        /// </summary>
        protected virtual void OnMouseUp()
        {
            if (!isEnable)
                return;

            if (selfLock)
                isLock = !isLock;

            if (isLock)
            {
                CurrentOffset = downOffset * lockPercent;

                if (OnSwitchLock != null)
                    OnSwitchLock();
            }
            else
            {
                IsDown = false;
                CurrentOffset = 0;

                if (OnSwitchUp != null)
                    OnSwitchUp();
            }
            TranslateButton(CurrentOffset);

            if (highLight && !isLock)
                lightRender.material = defaultMat;
        }

        /// <summary>
        /// Translate button switch to target position.
        /// </summary>
        /// <param name="offset">Offset of z axis.</param>
        protected virtual void TranslateButton(float offset)
        {
            transform.localPosition = StartPosition + MoveAxis.normalized * offset;
        }
        #endregion
    }
}