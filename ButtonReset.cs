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
    public class ButtonReset : InteractionReceiver          //but : réinitialiser la scène
    {
        public List<GameObject> object_list;                //liste des objets qui peuvent être reset
        public GameObject reference;                        //objet permettant d'obtenir le vecteur de translation appliqué à l'ensemble de la scène quand elle est déplacée

        private Vector3 coordRef;                           //coordonnées de l'objet reference

        private List<Vector3> posReset;                     //liste des positions initiales des objets
        private List<Quaternion> rotReset;                  //liste des orientations initiales des objets

        void Start() //au lancement
        {
            posReset = new List<Vector3>();
            rotReset = new List<Quaternion>();

            foreach (GameObject myObj in object_list)       //on parcourt les objets à traiter
            {
                posReset.Add(myObj.transform.position);     //on ajoute les vecteurs de position de chaque objet à la liste des positions
                rotReset.Add(myObj.transform.rotation);     //on ajoute les vecteurs d'orientation de chaque objet à la liste des orientations
            }
            coordRef = reference.transform.position;        //on récupère les coordonnées de l'objet de référence
        }
   
        protected override void InputDown(GameObject obj, InputEventData eventData)  //à l'appui sur le bouton
        {
            int k = 0;
            Vector3 translation = reference.transform.position;     //on récupère la position actuelle de l'objet de référence
            translation -= coordRef;                                /*on calcule le vecteur de translation en faisant la différence 
                                                                    entre les positions actuelle et initiale de l'objet de référence */

            foreach (GameObject myobj in object_list)       //on parcourt les objets à traiter
            {
                    myobj.transform.rotation = rotReset[k];                     //chaque objet est reset à son orientation initialle
                    myobj.transform.position = posReset[k] + translation;       /*chaque objet est reset à sa position initialle,
                                                                                à laquelle on ajoute le vecteur de translation qu'a subi l'ensemble de la scène*/
                    myobj.SetActive(false);          //on masque tous les objets
                    k++;
            }
        }
    }
}
