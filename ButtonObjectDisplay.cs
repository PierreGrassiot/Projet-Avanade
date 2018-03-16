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
    public class ButtonObjectDisplay : InteractionReceiver  //but : afficher l'objet lié au bouton
    {
        public GameObject myObj;                            //l'objet à afficher avec le bouton
        public GameObject preview;                          //le modèle à afficher en prévisualisation

        void Start() //au lancement
        {
            myObj.SetActive(false);                         //on désactive l'affichage de la pièce et de sa prévisualisation
            preview.SetActive(false);
        }

        protected override void FocusEnter(GameObject obj, PointerSpecificEventData eventData)  //lorsque l'on place le curseur sur le bouton
        {
            if (!preview.activeSelf)
            {
                preview.SetActive(true);           //si la prévisualisation n'est pas affichée, on l'affiche
            }
        }

        protected override void FocusExit(GameObject obj, PointerSpecificEventData eventData)  //quand le curseur quitte le bouton
        {
            if (preview.activeSelf)
            {
                preview.SetActive(false);           //si la prévisualisation est affichée, on la masque
            }
        }

        protected override void InputDown(GameObject obj, InputEventData eventData)         //sur un appui sur le bouton
        {
            if (myObj.activeSelf)
            {
                myObj.SetActive(false);             //si l'objet est déjà affiché, on le masque
            }
            else
            {
                myObj.SetActive(true);              //sinon, on l'affiche
            }
        }
    }
}
