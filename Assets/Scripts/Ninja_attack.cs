using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_attack : MonoBehaviour
{
    public GameObject attackexplosion;
    float atknum;
    public GameObject s;
    private void OnTriggerEnter(Collider collider)
    {
        atknum = s.GetComponent<sceneManager>().atk;
        if (collider.tag == "Enemy")
        {
            Instantiate(attackexplosion,collider.transform.position,Quaternion.identity);
            collider.SendMessage("hurted",atknum);
            this.gameObject.SetActive(false);
        }
    }
}
