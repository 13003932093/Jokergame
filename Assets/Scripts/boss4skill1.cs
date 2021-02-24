using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4skill1 : MonoBehaviour
{
    int hurt = 0;
    public GameObject explosion;
    void Start()
    {
        Destroy(this.gameObject, 1.6f);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.SendMessage("Slow");
        }
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && hurt == 1)
        {
            collider.SendMessage("hurted", 20);
            Instantiate(explosion, collider.transform.position, Quaternion.identity);
            hurt = 0;
        }
    }
    void clockbreak()
    {
        hurt = 1;
        this.GetComponent<AudioSource>().Play();
    }

}
