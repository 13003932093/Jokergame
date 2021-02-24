using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeattack : MonoBehaviour
{
    GameObject p;
    private void Start()
    {
        p= GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.SendMessage("hurted",16);
            p.SendMessage("PropHurted",5);
        }
    }
}
