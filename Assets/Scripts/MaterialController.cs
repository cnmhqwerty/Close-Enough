using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    private GameObject curPreview;
    public List<GameObject> inScene;
    public GameObject nail;

    public void OnMaterialClick(Item mat)
    {
        if (curPreview != null)
        {
            Destroy(curPreview);
        }if (nail != null)
        {
            Destroy(nail);
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100) && !GetComponent<GameplayController>().playing)
        {
            curPreview = Instantiate(mat.preview,hit.point, Quaternion.identity);
        }
    }
    void Update()
    {
        if (curPreview != null && !GetComponent<GameplayController>().playing)
        {
            if (Input.GetMouseButtonDown(0) && curPreview.GetComponent<Preview>().valid)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
                {
                    GameObject objectToPlace = Instantiate(curPreview.GetComponent<Preview>().real, curPreview.GetComponent<Preview>().hit.point, curPreview.GetComponent<Transform>().rotation);
                    Debug.Log("placed");
                    
                    if(Physics.ComputePenetration(objectToPlace.GetComponent<Collider>(), objectToPlace.transform.position, objectToPlace.transform.rotation, hit.collider, hit.transform.position, hit.transform.rotation, out var direction, out var distance))
                    {
                        objectToPlace.transform.position += direction * distance;
                        inScene.Add(objectToPlace);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, 1<<3))
                {
                    StartCoroutine(Coroutine (hit.transform.GameObject()));
                }
            }
        }
        else if (GetComponent<GameplayController>().playing)
        {
            Destroy(curPreview);
        }
    }

    IEnumerator Coroutine(GameObject t)
    {
        t.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        inScene.Remove(t);
        Destroy(t);
    }
    
    public void NailClick(Item mat)
    {
        if (curPreview != null)
        {
            Destroy(curPreview);
        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100) && !GetComponent<GameplayController>().playing)
        {
            nail = Instantiate(mat.preview, hit.point, Quaternion.identity);
        }
    }
}
