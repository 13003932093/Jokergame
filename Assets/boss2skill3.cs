using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2skill3 : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosion2;  
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" || collider.tag == "Playerskill"||collider.tag=="Playerprop")
        {            
            if (collider.tag == "Player")
            {
                GameObject ae = Instantiate(explosion, collider.transform.position, Quaternion.identity);
                ae.GetComponent<AudioSource>().Play();
                collider.SendMessage("hurted", 12);
            }
            else
            {
                GameObject ae = Instantiate(explosion2, collider.transform.position, Quaternion.identity);
                ae.GetComponent<AudioSource>().Play();
                Destroy(collider.gameObject);
            }
        }
    }
}
