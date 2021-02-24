using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb1 : MonoBehaviour
{
    public GameObject purple_explosion;
    private void Start()
    {
        Destroy(this.gameObject, 5);
        producesmoke();
        Invoke("producesmoke", 4.8f);
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (collider.GetComponent<player>().ifattack1 < 1)
            {
                collider.gameObject.SendMessage("bombattack");
                Destroy(this.gameObject);
            }
            else if (collider.GetComponent<player>().cowboyskill.activeInHierarchy)
            {
                if (collider.GetComponent<player>().ifattack1 == 1 && collider.GetComponent<player>().ifattack2 == 0)
                {
                    collider.gameObject.SendMessage("bombattack2");
                    Destroy(this.gameObject);
                }
                else if (collider.GetComponent<player>().ifattack1 == 0 && collider.GetComponent<player>().ifattack2 == 1)
                {
                    collider.gameObject.SendMessage("bombattack");
                    Destroy(this.gameObject);
                }
            }
        }      
    }
    void producesmoke()
    {
        Instantiate(purple_explosion, this.transform.position, Quaternion.identity);
    }
}
