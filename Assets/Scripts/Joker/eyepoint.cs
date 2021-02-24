using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyepoint : MonoBehaviour
{
    GameObject p;
    private void Start()
    {
        p = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, -150f*Time.deltaTime), Space.Self);
        if (p.activeInHierarchy == false)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.position = p.transform.position;
        }
    }
}
