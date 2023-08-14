using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public Material Valid, Invalid;
    public bool valid, touch;
    public GameObject real;
    public RaycastHit hit;
    private float z = 0, x = 0;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.CompareTag("Placeable"))
            {
                this.GetComponent<Renderer>().material = Valid;
                valid = true;
            }
            else
            {
                this.GetComponent<Renderer>().material = Invalid;
                valid = false;
            }

            transform.position = hit.point;

            if (Physics.ComputePenetration(GetComponent<Collider>(), transform.position, transform.rotation,
                    hit.collider, hit.transform.position, hit.transform.rotation, out var direction, out var distance))
            {
                transform.position += direction * distance;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            z += -0.1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            z += 0.1f;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            x += 0.1f;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            x += -0.1f;
        }
        transform.eulerAngles += new Vector3(x, 0, z);
        z = 0;
        x = 0;
    }
}
