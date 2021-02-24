using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3skill3 : MonoBehaviour
{
    public GameObject explosion;
    float timer;
    void Start()
    {
        Destroy(this.gameObject,2);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().gameObject.tag == "Player")
        {
            float r = Random.Range(1,101);
            if (r <= 50)
            {
                collider.SendMessage("Poison");
            }
            collider.SendMessage("hurted", 5);
            GameObject ae = Instantiate(explosion, collider.transform.position, Quaternion.identity);
            ae.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<Collider>().gameObject.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer >= 0.6f)
            {
                float r = Random.Range(1, 101);
                if (r <= 50)
                {
                    collider.SendMessage("Poison");
                }
                collider.SendMessage("hurted", 5);
                GameObject ae = Instantiate(explosion, collider.transform.position, Quaternion.identity);
                ae.GetComponent<AudioSource>().Play();
                timer = 0;
            }
        }
    }
}
