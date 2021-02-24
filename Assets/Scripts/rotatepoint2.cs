using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatepoint2 : MonoBehaviour
{
    Animator anim;
    public GameObject knife;
    public GameObject ball;
    public GameObject firepoint;
    public GameObject bottle;
    public GameObject bloodknife;
    public GameObject sceneManager;
    public GameObject p;
    public GameObject bomb;
    public GameObject gun;
    public GameObject gunfire;
    public GameObject bullet;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    void knifeattack()
    {
        if (anim.GetInteger("state") == 0)
        {
            p.GetComponent<player>().ifattack2 ++;
            anim.SetInteger("state", 1);
            Invoke("finishattack", 0.7f);
            Invoke("noattack", 0.7f);
        }
    }
    void ballattack()
    {
        if (anim.GetInteger("state") == 0)
        {
            p.GetComponent<player>().ifattack2++;
            anim.SetInteger("state", 2);
            Invoke("finishattack", 3f);
            Invoke("noattack", 3f);
        }
    }
    void bottleattack()
    {
        if (anim.GetInteger("state") == 0)
        {
            p.GetComponent<player>().ifattack2++;
            anim.SetInteger("state", 3);
            Invoke("finishattack", 0.7f);
            Invoke("noattack", 0.7f);
        }
    }
    void bloodknifeattack()
    {
        if (anim.GetInteger("state") == 0)
        {
            p.GetComponent<player>().ifattack2++;
            anim.SetInteger("state", 4);
            Invoke("finishattack", 0.7f);
            Invoke("noattack", 0.7f);
            p.SendMessage("PropHurted", 3);
        }
    }
    void bombattack()
    {
        if (anim.GetInteger("state") == 0)
        {
            p.GetComponent<player>().ifattack2++;
            anim.SetInteger("state", 5);
            Invoke("finishattack", 1.5f);
            Invoke("noattack", 1.5f);
        }
    }
    void gunattack()
    {
        if (anim.GetInteger("state") == 0)
        {
            anim.SetInteger("state", 6);
            Invoke("finishattack", 0.4f);
            Instantiate(bullet, gunfire.transform.position, Quaternion.identity);
        }
    }
    void finishattack()
    {
        anim.SetInteger("state", 0);
    }
    void noattack()
    {
        p.GetComponent<player>().ifattack2 --;
    }
    void produceknife()
    {
        Instantiate(knife, firepoint.transform.position, Quaternion.identity);
    }
    void produceball()
    {
        Instantiate(ball, firepoint.transform.position, Quaternion.identity);
    }
    void producebottle()
    {
        Instantiate(bottle, firepoint.transform.position, Quaternion.identity);
    }
    void producebloodknife()
    {
        Instantiate(bloodknife, firepoint.transform.position, Quaternion.identity);
    }
    void producebomb()
    {
        Instantiate(bomb, firepoint.transform.position, Quaternion.identity);
    }
}
