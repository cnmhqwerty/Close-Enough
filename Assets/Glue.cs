using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Glue : MonoBehaviour
{
    public GameObject glueparent, temp;
    private CombineInstance[] comb = new CombineInstance[1];
    private int count = 0;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            transform.position = hit.point;
            if (Input.GetMouseButtonDown(0) && hit.transform.GameObject().layer == 3)
            {
                if (count == 1)
                {
                    hit.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(comb);
                    count = 0;
                }
                else
                {
                    comb[0].mesh = hit.transform.GetComponent<MeshFilter>().sharedMesh;
                    comb[0].transform = hit.transform.localToWorldMatrix;
                    count += 1;
                }
            }
        }
    }
}
