using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;


public class VoiceManager : MonoBehaviour   //but : gérer les déplacements de la scène grâce à la commande vocale
{
    public GameObject plane;                //objet que l'on affichera en mode déplacement

    private KeywordRecognizer keywordRecognizer = null;
    private Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();       //map qui lie une chaîne de caractères (prononcée par l'utilisateur) à une suite d'instructions
    private bool placing = false;           //booléen décrivant si on se trouve en mode déplacement

    void Start() //au lancement
    {
        /* on ajoute à la map la commande vocale 'Move' qui permet de passer en mode déplacement et d'affciher le plan */
        keywords.Add("Move", () =>
        {
            placing = true;
            plane.GetComponent<Renderer>().enabled = true;
        });

        /* on ajoute à la map la commande vocale 'Done' qui permet de quitter le mode déplacement et de masquer le plan */
        keywords.Add("Done", () =>
        {
            placing = false;
            plane.GetComponent<Renderer>().enabled = false;
        });

        //on notifie le KeywordRecognizer de nos mots-clés
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        //on lance le callback, c'est-à-dire la reconnaissance vocale
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }


    void Update() //à chaque frame
    {
        if (placing)            //si on est en mode déplacement
        {
            //on récupère la position et la direction du regard de l'utilisateur
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            //on lance un rayon dans l'environnement entre l'utilisateur et le maillage issu du Spatial Mapping
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, 30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                //on déplace l'objet parent (nos objets à afficher) à l'endroit où le rayon percute le maillage du Spatial Mapping
                this.transform.parent.position = hitInfo.point;

                //on déplace l'objet parent de façon à ce qu'il soit face à l'utilisateur
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;
            }
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)  //méthode callback appelée à chaque fois qu'un mot est prononcé
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))     //on essaye de récupérer le mot dans la map
        {
            keywordAction.Invoke();                //si on y arrive, on exécute les instrcutions correspondantes
        }
    }
}