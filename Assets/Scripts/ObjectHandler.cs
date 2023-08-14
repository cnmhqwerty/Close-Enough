using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    private Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        Pos = transform.position;
    }

    public void resetter()
    {
        transform.position = Pos;
    }
}
