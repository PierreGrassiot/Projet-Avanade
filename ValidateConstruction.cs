using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ValidateConstruction : MonoBehaviour {  //script de validation de la constrution en comparant les coordonnées

    public enum axes { x, y, z, rho, theta, phi };

    public List<GameObject> objList;        //liste d'objets à traiter
    public GameObject[] reference;          //tableau d'objets de référence

    public Text result;                     //résultat écrit dans une chaîne de caractères
    public float precision;                 //précision sur la comparaison
    public axes axeChoisi;                  //axe à traiter

    private string str;

	void Update () {
        str = "Running...\n";
        int i = 0;
        foreach (GameObject obj in objList)
        {
            ComparerObjets(obj, reference[i], axeChoisi);     //on compare chaque objet à traiter avec se référence
            ++i;
        }
        result.text = str;
    }

    private void ComparerObjets(GameObject objCree, GameObject objRef, axes axeChoisi)
    {
        float compareCree = 0;
        float compareRef = precision + 1;

        switch(axeChoisi)             //selon l'axe choisi pour le traitement, on récupère les valeurs pour l'objet et sa référence
        {
            case axes.x :
                compareCree = objCree.transform.position.x;
                compareRef = objRef.transform.position.x;
                break;
            case axes.y :
                compareCree = objCree.transform.position.y;
                compareRef = objRef.transform.position.y;
                break;
            case axes.z :
                compareCree = objCree.transform.position.z;
                compareRef = objRef.transform.position.z;
                break;
            case axes.rho :
                compareCree = objCree.transform.rotation.x;
                compareRef = objRef.transform.rotation.x;
                break;
            case axes.theta :
                compareCree = objCree.transform.rotation.y;
                compareRef = objRef.transform.rotation.y;
                break;
            case axes.phi :
                compareCree = objCree.transform.rotation.z;
                compareRef = objRef.transform.rotation.z;
                break;
            default:
                compareCree = precision + 1;
                compareRef = 0;
                break;
        }

        if (Math.Abs(compareCree - compareRef) < precision)
        {
            str += objCree.name + " " + " OK\n"; 
            //on affiche le nom de l'objet s'il est bien construit
        }
    }
}
