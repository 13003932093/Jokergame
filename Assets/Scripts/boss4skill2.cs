using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using System;

public class boss4skill2 : MonoBehaviour
{
    GameObject p;
    int ifattack;
    public GameObject explosion;
    
    void Start()
    {
        ifattack = 0;
        p = GameObject.FindWithTag("Player");
        Destroy(this.gameObject,4);
        Invoke("attack",1.5f);
    }

    void Update()
    {
        if (ifattack == 0)
        {
            this.transform.Rotate(new Vector3(0, 0, -1440f * Time.deltaTime), Space.Self);
        }
        else
        {
            transform.Translate(Vector3.up * 2f * Time.deltaTime);
        }
   
    }  
    void attack()
    {
        ifattack = 1;
        this.GetComponent<AudioSource>().Play();
        if (p != null)
        {
            if (p.transform.position.x >= this.transform.position.x)
            {
                this.transform.Rotate(new Vector3(0, 0, (float)(180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI - 90)));
            }
            else
            {
                this.transform.Rotate(new Vector3(0, 0, (float)(180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI + 90)));
            }
        }      
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (ifattack==1&&collider.GetComponent<Collider>().gameObject.tag == "Player" ||  collider.GetComponent<Collider>().gameObject.tag == "Playershield")
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            if (collider.GetComponent<Collider>().gameObject.tag == "Player")
            {
                collider.GetComponent<Collider>().gameObject.SendMessage("hurted", 15);
                collider.SendMessage("Slow");
            }
            Destroy(this.gameObject);
        }
    }
}
