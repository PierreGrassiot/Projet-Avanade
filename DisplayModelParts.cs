using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayModelParts : MonoBehaviour {

    public List<GameObject> objList;                //liste d'objets à traiter
    public int[] ordre;                             //ordre d'affichage à respecter

    //map qui associe un ordre de placement à l'objet correspondant
    private SortedDictionary<int, GameObject> objMap = new SortedDictionary<int, GameObject>();          

    void Start () {

        int i = 0;
        foreach (GameObject obj in objList)
        {
            obj.SetActive(false);               //on masque chaque objet de la liste
            objMap.Add(ordre[i], obj);          //on ajoute à la map le numéro d'apparition et l'objet de la liste
            i++;
        }

        //on lance la routine Actions, définie ci-dessous
        StartCoroutine(Actions());
    }

IEnumerator Actions()
    {  
        GameObject currentObj = new GameObject();

        for (int i = 1; i < objList.Count + 1; ++i)         //autant de fois qu'il y a d'objets à traiter
        {
            objMap.TryGetValue(i, out currentObj);          //on récupère l'objet correspondant au numéro d'apparition suivant
            currentObj.SetActive(true);                     //on l'affiche
            yield return new WaitForSecondsRealtime(3);     //on attend 3 secondes
        }
    }
}