using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball2_Sprites : MonoBehaviour
{   
    void Update()
    {
        this.transform.Rotate(new Vector3(0,0,6),Space.Self);
    }
}
