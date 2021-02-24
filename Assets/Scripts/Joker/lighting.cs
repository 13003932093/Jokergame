using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,1.6f);
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy"||collider.tag=="Player")
        {
            collider.SendMessage("hurted", 15);
        }
    }
}
