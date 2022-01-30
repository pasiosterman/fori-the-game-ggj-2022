using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorModelChanger : MonoBehaviour
{
    public GameObject[] models;

    public void ChangeModel(int index){

        if(index >= models.Length){
            Debug.LogError("No model with index! " + index);
            return;
        }

        for (int i = 0; i < models.Length; i++)
        {
            models[i].gameObject.SetActive(i == index);
        }
    }
}
