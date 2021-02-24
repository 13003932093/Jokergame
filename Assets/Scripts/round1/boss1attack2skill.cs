using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss1attack2skill : MonoBehaviour
{
    public GameObject attackexplosion;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            GameObject ae= Instantiate(attackexplosion, collider.transform.position, collider.transform.rotation);
            collider.SendMessage("hurted",15);
            ae.GetComponent<AudioSource>().Play();
            this.gameObject.SetActive(false);
        }
    }
}
