using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public GameObject white_smoke;
    void Start()
    {
        Invoke("producesmoke",4.8f);
        Destroy(this.gameObject,5);
    }
    void producesmoke()
    {
        Instantiate(white_smoke, this.transform.position, Quaternion.identity);
    }
}
