//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.Receivers;
using HoloToolkit.Unity.InputModule;

namespace HoloToolkit.Unity.Examples
{
    public class ButtonObjectDisplay : InteractionReceiver
    {
        public GameObject textObjectState;
        private TextMesh txt;
        public GameObject myObj, preview;

        private float xInit, zInit;

        void Start()
        {
            txt = textObjectState.GetComponentInChildren<TextMesh>();

            xInit = myObj.transform.position.x;
            zInit = myObj.transform.position.z;

            myObj.SetActive(false);
            preview.SetActive(false);
        }

        protected override void FocusEnter(GameObject obj, PointerSpecificEventData eventData)
        {
            Debug.Log(obj.name + " : FocusEnter");
            txt.text = obj.name + " : FocusEnter";
            if (!preview.activeSelf)
            {
                preview.SetActive(true);
            }
        }

        protected override void FocusExit(GameObject obj, PointerSpecificEventData eventData)
        {
            Debug.Log(obj.name + " : FocusExit");
            txt.text = obj.name + " : FocusExit";
            if (preview.activeSelf)
            {
                preview.SetActive(false);
            }
        }

        protected override void InputDown(GameObject obj, InputEventData eventData)
        {
            Debug.Log(obj.name + " : InputDown");
            txt.text = obj.name + " : InputDown";
            if (myObj.activeSelf)
            {
                myObj.SetActive(false);
            }
            else
            {
                myObj.SetActive(true);
            }
        }

        protected override void InputUp(GameObject obj, InputEventData eventData)
        {
            Debug.Log(obj.name + " : InputUp");
            txt.text = obj.name + " : InputUp";
        }
    }
}
