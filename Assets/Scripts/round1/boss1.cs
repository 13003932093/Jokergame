using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class boss1 : MonoBehaviour
{
    GameObject p;
    public int oriention;//0为左 1为右
    float timer1;//控制移动的计时器 1s移动
    float movespeed=0.8f;
    float randway;//选择一个随机的方向进行移动(弧度)
    public GameObject action;
    public Animator anim;
    int isattack;//判断当前是否在攻击
    Vector2 attackway;//进行攻击的方向(坐标)
    int lockmove;//是否允许移动
    public GameObject hurtedText;
    public Slider hpSlider;
    public float hp;
    public float fullhp;
    public  GameObject sceneManager;
    int canbehurt=1;
    int ifdeath;
    float encitimer;
    public GameObject attack2skill;
    public GameObject shadow;

    void Start()
    {
        p = GameObject.FindWithTag("Player");
        randway = UnityEngine.Random.Range(-1f, 1f);
        fullhp = 150;
        hp = fullhp;
    }
    void Update()
    {        
        //血条显示
        hpSlider.value = hp / fullhp;
        if (ifdeath == 0)
        {                       
            //死亡
            if (hp <= 0)
            {
                death();
            }
            if (lockmove == 0)
            {
                if (isattack == 0)
                {
                    walk();
                    timer1 += Time.deltaTime;
                    if (timer1 >= 1)
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
                    if (randatattack <= 50)
                    {
                        attack1_prepare();
                    }
                    else
                    {
                        attack2_prepare();
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
            if (anim.GetInteger("state") == 21)
            {
                if (p.transform.position.x <= this.transform.position.x)
                {
                    oriention = 0;
                }
                else
                {
                    oriention = 1;
                }
                if (oriention == 1)
                {
                    this.transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector2(1, 1);
                }
            }
            if (anim.GetInteger("state") == 31)
            {
                this.GetComponent<CharacterController>().Move(attackway * movespeed * 2f * Time.deltaTime);
            }
        }       
    }
    void walk()
    {
        anim.SetInteger("state",1);
    }
    void attack1_prepare()
    {
        anim.SetInteger("state", 2);
        Invoke("attack1",1f);
        Lock();
        Invoke("unLock", 3f);
        Invoke("finishattack",3.5f);
    }
    void attack2_prepare()
    {
        anim.SetInteger("state", 3);
        Invoke("attack2", 1f);
        Lock();
        Invoke("unLock", 2f);
        Invoke("finishattack", 2.5f);
    }

    void attack1()
    {        
        anim.SetInteger("state",21);
    }
    void attack2()
    {
        if (p.transform.position.x <= this.transform.position.x)
        {
            oriention = 0;
        }
        else
        {
            oriention = 1;
        }
        if (oriention == 1)
        {
            this.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            this.transform.localScale = new Vector2(1, 1);
        }
        attackway = (p.transform.position - this.transform.position)/(Vector2.Distance(p.transform.position, this.transform.position));
        anim.SetInteger("state", 31);
        attack2skill.SetActive(true);
    }
    void finishattack()
    {
        timer1 = 0;
        isattack = 0;
        attack2skill.SetActive(false);
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
        if (canbehurt == 1&&ifdeath==0)
        {
            anim.SetInteger("state2", 1);
            Instantiate(hurtedText, new Vector2(this.transform.position.x, this.transform.position.y + 0.1f), Quaternion.identity).SendMessage("shownum", (int)num);
            hp -= (int)num;
            //判断是否有bloodmask
            if (sceneManager.GetComponent<sceneManager>().ifprop[1] >= 1)
            {
                for (int i=1; i <= sceneManager.GetComponent<sceneManager>().ifprop[1]; i++)
                {
                    p.SendMessage("recover", 1);
                }
            }
            Invoke("health", 0.1f);
        }
    }
    void health()
    {
        anim.SetInteger("state2",0);
        canbehurt = 1;
    } 
    void death()
    {        
        ifdeath = 1;        
        anim.SetInteger("state",9);
        sceneManager.SendMessage("finishround");
    }
    void reBirth()
    {
        ifdeath = 0;        
        walk();        
        randway = UnityEngine.Random.Range(-1f, 1f);
        isattack = 0;
        this.transform.position = new Vector2(0.576f,0f);
        hp = fullhp;        
    }

}
