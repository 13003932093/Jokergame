using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb2 : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject,2.2f);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.SendMessage("hurted", 25);
        }
        else if (collider.gameObject.tag == "Player")
        {
            collider.SendMessage("hurted",15);
        }
    }
}
