using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using System;

public class boss3skill2 : MonoBehaviour
{
    Animator anim;
    GameObject p;
    float timer;
    float timer2;
    int oriention;
    Vector2 attackway;
    public GameObject attackexplosion;
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        Invoke("idle", 0.6f);
        Invoke("death", 5.6f);
        Destroy(this.gameObject, 6.2f);
        p = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (anim.GetInteger("state") == 1)
        {
            if (oriention == 1)
            {
                this.transform.localScale = new Vector2(-0.3f, 0.3f);
            }
            else
            {
                this.transform.localScale = new Vector2(-0.3f, 0.3f);
            }
            this.transform.Translate(attackway * 0.8f * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= 0.8f)
            {
                track();
                timer = 0;
            }
        }
    }
    void idle()
    {
        anim.SetInteger("state", 1);
    }
    void death()
    {
        anim.SetInteger("state", 2);
    }
    void track()
    {
        attackway = (p.transform.position - this.transform.position) / (Vector2.Distance(p.transform.position, this.transform.position));
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().gameObject.tag == "Player")
        {
            collider.SendMessage("hurted", 6);
            GameObject ae = Instantiate(attackexplosion, collider.transform.position, Quaternion.identity);
            ae.GetComponent<AudioSource>().Play();
        }
    }
    private void OnTriggerStay(Collider collider)
    {
            if (collider.GetComponent<Collider>().gameObject.tag == "Player")
            {
                timer2 += Time.deltaTime;
                if (timer2 >= 0.6f)
                {
                    collider.SendMessage("hurted", 6);
                    GameObject ae = Instantiate(attackexplosion, collider.transform.position, Quaternion.identity);
                    ae.GetComponent<AudioSource>().Play();
                    timer2 = 0;
                }
            }
    }
}
