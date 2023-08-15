using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Glue : MonoBehaviour
{
    private GameObject temp;
    private Color cTemp;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            transform.position = hit.point;
            if (Input.GetMouseButtonDown(0) && hit.transform.GameObject().layer == 3)
            {
                if (temp == null)
                {
                    temp = hit.transform.GameObject();
                    StartCoroutine(Coroutine (hit.transform.GameObject()));
                }
                else if(Vector3.Distance(temp.transform.position, hit.transform.position) <= 2)
                {
                    StartCoroutine(Coroutine (hit.transform.GameObject()));
                    hit.transform.AddComponent<FixedJoint>().connectedBody = temp.GetComponent<Rigidbody>();
                    temp = null;
                }
                else
                {
                    Debug.Log("not close enough");    
                }
            }
            else if (Input.GetMouseButton(1))
            {
                temp = null;
            }
        }
    }
    IEnumerator Coroutine(GameObject t)
    {
        cTemp = t.GetComponent<Renderer>().material.color;
        t.GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        t.GetComponent<Renderer>().material.color = cTemp;
    }
}
