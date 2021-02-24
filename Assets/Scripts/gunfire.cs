using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunfire : MonoBehaviour
{
    public GameObject explosion;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.SendMessage("hurted", GameObject.Find("sceneManager").GetComponent<sceneManager>().atk * 1.2f);
            Instantiate(explosion,collider.transform.position,Quaternion.identity);
        }
    }
}
