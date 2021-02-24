using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class boss3 : MonoBehaviour
{
    GameObject p;
    public int oriention;//0为左 1为右
    float timer1;//控制移动的计时器 1s移动
    float movespeed = 0.8f;
    float randway;//选择一个随机的方向进行移动(弧度)
    public GameObject action;
    public Animator anim;
    int isattack;//判断当前是否在攻击
    Vector2 attackway;//进行攻击的方向(坐标)
    int lockmove;//是否允许移动
    public GameObject hurtedText;
    public float hp;
    public float fullhp;
    public GameObject sceneManager;
    int canbehurt = 1;
    int ifdeath;
    float encitimer;
    public GameObject skill1;
    public GameObject skill2;
    public Slider hpSlider;
    public GameObject skill3;
    public GameObject firepoint1;
    public GameObject firepoint2;
    public GameObject firepoint3;
    public GameObject skill31;
    public GameObject skill21;
    void Start()
    {
        p = GameObject.FindWithTag("Player");
        randway = UnityEngine.Random.Range(-1f, 1f);
        fullhp = 300;
        hp = fullhp;
    }
    void Update()
    {
        if (ifdeath == 0)
        {            
            //血条显示
            hpSlider.value = hp / fullhp;
            if (hp <= 0 && ifdeath == 0)
            {
                death();
            }
            if (lockmove == 0)
            {
                if (isattack == 0)
                {
                    walk();
                    timer1 += Time.deltaTime;
                    if (timer1 >= 0.8f)
                    {
                        isattack = 1;
                    }
                    else
                    {
                        this.GetComponent<CharacterController>().Move(new Vector2((float)(Math.Cos(randway * Math.PI)), (float)(Math.Sin(randway * Math.PI))) * movespeed * Time.deltaTime);
                    }
                    if (p.transform.position.x <= this.transform.position.x)
                    {
                        oriention = 0;
                    }
                    else
                    {
                        oriention = 1;
                    }
                }
                else if (isattack == 1)
                {
                    //攻击选择
                    int randatattack;
                    randatattack = UnityEngine.Random.Range(1, 101);
                    if (randatattack <= 35)
                    {
                        attack1();
                    }
                    else if(randatattack<=70)
                    {
                        attack2();
                    }
                    else
                    {
                        attack3();
                    }
                    isattack = 2;
                }
                //使该角色始终面对player
                if (oriention == 1)
                {
                    this.transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector2(1, 1);
                }
            }
        }
    }
    void walk()
    {
        anim.SetInteger("state", 1);
    }
    void attack1()
    {
        anim.SetInteger("state", 2);
        Invoke("produceskill1", 0.5f);
        Lock();
        Invoke("unLock", 0.7f);
        Invoke("finishattack", 0.7f);
    }
    void attack2()
    {
        anim.SetInteger("state", 3);
        skill21.SetActive(true);
        Invoke("produceskill2", 0.5f);
        Lock();
        Invoke("unLock", 2f);
        Invoke("finishattack", 2f);
    }
    void attack3()
    {
        anim.SetInteger("state", 4);
        skill31.SetActive(true);
        Invoke("produceskill3", 0.5f);
        Invoke("produceskill3", 2f);
        Invoke("produceskill3", 3.5f);
        Lock();
        Invoke("unLock", 4f);
        Invoke("finishattack", 4f);
    }
    void produceskill1()
    {
        if (p.transform.position.x >= this.transform.position.x)
        {
            firepoint1.transform.localEulerAngles = new Vector3(0, 0, (float)(-180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI));
            firepoint2.transform.localEulerAngles = new Vector3(0, 0, (float)(-180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI) + 30);
            firepoint3.transform.localEulerAngles = new Vector3(0, 0, (float)(-180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI) - 30);
        }
        else
        {
            firepoint1.transform.localEulerAngles = new Vector3(0, 0, (float)(180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI));
            firepoint2.transform.localEulerAngles = new Vector3(0, 0, (float)(180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI) + 30);
            firepoint3.transform.localEulerAngles = new Vector3(0, 0, (float)(180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI) - 30);
        }
        Instantiate(skill1, firepoint1.transform.position, firepoint1.transform.rotation);
        Instantiate(skill1, firepoint2.transform.position, firepoint2.transform.rotation);
        Instantiate(skill1, firepoint3.transform.position, firepoint3.transform.rotation);
    }
    void produceskill2()
    {
        for (int i = 1; i <= 2; i++)
        {
            Instantiate(skill2, new Vector2(this.transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f), this.transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        }
    }
    void produceskill3()
    {
        for (int i = 1; i <= 3; i++)
        {
            Instantiate(skill3, new Vector2(p.transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f),p.transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        }
    }    
    void finishattack()
    {
        timer1 = 0;
        isattack = 0;
        skill21.SetActive(false);
        skill31.SetActive(false);
        randway = UnityEngine.Random.Range(-1f, 1f);
    }
    void Lock()
    {
        lockmove = 1;
    }
    void unLock()
    {
        lockmove = 0;
    }
    void hurted(float num)
    {
        if (canbehurt == 1 && ifdeath == 0)
        {
            anim.SetInteger("state2", 1);
            Instantiate(hurtedText, new Vector2(this.transform.position.x, this.transform.position.y + 0.1f), Quaternion.identity).SendMessage("shownum", (int)num);
            hp -= (int)num;
            //判断是否有bloodmask
            if (sceneManager.GetComponent<sceneManager>().ifprop[1] >= 1)
            {
                for (int i = 1; i <= sceneManager.GetComponent<sceneManager>().ifprop[1]; i++)
                {
                    p.SendMessage("recover", 1);
                }
            }
            Invoke("health", 0.1f);
        }
    }
    void health()
    {
        anim.SetInteger("state2", 0);
        canbehurt = 1;
    }
    void death()
    {
        ifdeath = 1;
        anim.SetInteger("state", 9);
        sceneManager.SendMessage("finishround");
    }
    void reBirth()
    {
        ifdeath = 0;
        walk();
        randway = UnityEngine.Random.Range(-1f, 1f);
        isattack = 0;
        this.transform.position = new Vector2(0.576f, 0f);
        hp = fullhp;
    }
}
