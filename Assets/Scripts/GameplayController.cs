using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public bool playing = false;
    
    public void OnPlay(){
        foreach (var obj in GetComponent<MaterialController>().inScene)
        {
            Debug.Log("Play");
            playing = true;
            obj.GetComponent<Rigidbody>().freezeRotation = false;
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void OnReset()
    {
        foreach (var obj in GetComponent<MaterialController>().inScene)
        {
            Debug.Log("Reset");
            playing = false;
            obj.GetComponent<Rigidbody>().freezeRotation = true;
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            obj.GetComponent<ObjectHandler>().resetter();
        }
    }

}