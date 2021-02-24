using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon_attack : MonoBehaviour
{
    public GameObject p;
    public float atk;
    public GameObject s;
    public GameObject explosion;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            atk = s.GetComponent<sceneManager>().atk;
            collider.SendMessage("hurted", atk* 2);
            Instantiate(explosion,collider.transform.position,Quaternion.identity);
            p.SendMessage("recover", atk);
        }
    }
}
