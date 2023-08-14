using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    private GameObject curPreview;
    
    public void OnMaterialClick(Item mat)
    {
        if (curPreview != null)
        {
            Destroy(curPreview);
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            curPreview = Instantiate(mat.preview,hit.point, Quaternion.identity);
        }
    }
    void Update()
    {
        if (curPreview != null)
        {
            if (Input.GetMouseButtonDown(0) && curPreview.GetComponent<Preview>().valid)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
                {
                    Collider objectToPlace = Instantiate(curPreview.GetComponent<Preview>().real, curPreview.GetComponent<Preview>().hit.point,
                        Quaternion.identity).GetComponent<Collider>();
                    Debug.Log("placed");
                    
                    if(Physics.ComputePenetration(objectToPlace, objectToPlace.transform.position, objectToPlace.transform.rotation, hit.collider, hit.transform.position, hit.transform.rotation, out var direction, out var distance))
                    {
                        objectToPlace.transform.position += direction * distance;
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
    }

    IEnumerator Coroutine(GameObject t)
    {
        t.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Destroy(t);
    }
}
