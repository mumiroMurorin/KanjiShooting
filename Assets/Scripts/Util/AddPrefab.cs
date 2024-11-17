using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPrefab : MonoBehaviour
{
    [SerializeField] List<GameObject> parents;
    [SerializeField] List<GameObject> children;

    private void Start()
    {
        foreach (GameObject parent in parents)
        {
            foreach (GameObject child in children)
            {
                Instantiate(child).transform.SetParent(parent.transform);
            }
        }

        //foreach (GameObject parent in parents)
        //{
        //    foreach(Transform child in parent.transform)
        //    {
        //        Destroy(child.gameObject);
        //    }
        //}

        //for(int i = 0;i< parents.Count; i++)
        //{
        //    Instantiate(children[i]).transform.SetParent(parents[i].transform);
        //}
    }
}
