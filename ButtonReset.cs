//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.Receivers;
using HoloToolkit.Unity.InputModule;
using System;

namespace HoloToolkit.Unity.Examples
{
    public class ButtonReset : InteractionReceiver
    {
        public GameObject textObjectState;
        private TextMesh txt;
        public List<GameObject> object_list;
        public Transform target;
        public GameObject reference;

        //private List<Transform> posReset;
        private List<Vector3> posReset;
        private List<Quaternion> rotReset;
        private Vector3 coordRef;

        //private VoiceManager voiceManager;

        void Start()
        {
            //voiceManager = FindObjectOfType(typeof(VoiceManager)) as VoiceManager;

            txt = textObjectState.GetComponentInChildren<TextMesh>();
            posReset = new List<Vector3>();
            rotReset = new List<Quaternion>();

            foreach (GameObject myObj in object_list)
            {
                myObj.SetActive(true);
                posReset.Add(myObj.transform.position);
                rotReset.Add(myObj.transform.rotation);
                myObj.SetActive(false);
            }

            coordRef = reference.transform.position;

            //voiceManager.SceneMoved += c_moved;
        }

        /*static void c_moved (object sender, EventArgs e)
        {
            posReset = new List<Vector3>();
            rotReset = new List<Quaternion>();

            foreach (GameObject myObj in object_list)
            {
                myObj.SetActive(true);
                posReset.Add(myObj.transform.position);
                rotReset.Add(myObj.transform.rotation);
                myObj.SetActive(false);
            }
        }*/    

        

        protected override void FocusEnter(GameObject obj, PointerSpecificEventData eventData)
        {
            txt.text = "";
            Debug.Log(obj.name + " : FocusEnter");
            foreach (GameObject myobj in object_list)
            {
                if (myobj.activeSelf)
                {
                    txt.text += myobj.name + " x= " + myobj.transform.position.x + "  y= " + myobj.transform.position.y + "  z= " + myobj.transform.position.z + "\n";
                }
            }
        }

        protected override void FocusExit(GameObject obj, PointerSpecificEventData eventData)
        {
            Debug.Log(obj.name + " : FocusExit");
            txt.text = obj.name + " : FocusExit";
        }

        protected override void InputDown(GameObject obj, InputEventData eventData)
        {
            Debug.Log(obj.name + " : InputDown");
          
            int k = 0;
            Vector3 translation = reference.transform.position;
            translation -= coordRef;

            foreach (GameObject myobj in object_list)
            {
                if (myobj.activeSelf)
                {
                    //myobj.transform.position = new Vector3(myobj.transform.position.x, 1.5f, myobj.transform.position.z);
                    //txt.text += myobj.name + " x= " + myobj.transform.position.x + "  y= " + myobj.transform.position.y + "  z= " + myobj.transform.position.z + "\n";

                    //myobj.transform.LookAt(target);
                    myobj.transform.rotation = rotReset[k];
                    myobj.transform.position = posReset[k] + translation;
                   
                    myobj.SetActive(false);
                    txt.text = obj.name + "  " + myobj.transform.position.y;
                    k++;
                }
            }
        }

        protected override void InputUp(GameObject obj, InputEventData eventData)
        {
            Debug.Log(obj.name + " : InputUp");
            txt.text = obj.name + " : InputUp";
        }
    }
}
