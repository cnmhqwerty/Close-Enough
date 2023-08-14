using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    private Vector3 Pos;
    private Quaternion Rot;
    int rotatez;
    // Start is called before the first frame update
    void Start()
    {
        rotatez = 2;
        Pos = transform.position;
        Rot = transform.rotation;
    }

    public void resetter()
    {
        transform.position = Pos;
        transform.rotation = Rot;
    }
}
