using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class boss2 : MonoBehaviour
{
    GameObject p;
    public int oriention;//0为左 1为右
    float timer1;//控制移动的计时器 1s移动
    float movespeed = 0.8f;    
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
    public GameObject skill3;
    public Slider hpSlider;    
    public GameObject firepoint;
    //public GameObject skill31;
    //public GameObject skill21;   
    public GameObject canying;//控制残影
    void Start()
    {
        p = GameObject.FindWithTag("Player");       
        fullhp = 200;
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
                    idle();
                    timer1 += Time.deltaTime;
                    if (timer1 >= 0.7f)
                    {
                        isattack = 1;
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
                    else if (randatattack <= 70)
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
            if (anim.GetInteger("state") == 2)
            {
                this.GetComponent<CharacterController>().Move(attackway * movespeed * 3f * Time.deltaTime);
            }
        }
    }
    void idle()
    {
        anim.SetInteger("state", 1);
    }
    void attack1()
    {
        anim.SetInteger("state", 2);
        attackway = (p.transform.position - this.transform.position) / (Vector2.Distance(p.transform.position, this.transform.position));
        producecanying();
        Lock();
        skill1.SetActive(true);
        Invoke("unLock", 0.7f);
        Invoke("finishattack", 0.7f);
        int r = UnityEngine.Random.Range(1,101);
        if(r<=60)
        {
            Invoke("attack3", 0.8f);
        }        
    }
    void attack2()
    {
        anim.SetInteger("state", 3);
        if (p.transform.position.x >= this.transform.position.x)
        {
            firepoint.transform.localEulerAngles = new Vector3(0, 0, (float)(-180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI));            
        }
        else
        {
            firepoint.transform.localEulerAngles = new Vector3(0, 0, (float)(180 * Math.Atan((p.transform.position.y - this.transform.position.y) / (p.transform.position.x - this.transform.position.x)) / Math.PI));            
        }
        Invoke("produceskill2", 0.3f);
        Lock();
        Invoke("unLock", 0.8f);
        Invoke("finishattack", 0.8f);
    }
    void attack3()
    {
        anim.SetInteger("state", 4);
        skill3.SetActive(true);
        Lock();
        Invoke("unLock", 0.6f);
        Invoke("finishattack", 0.6f);
    }    
    void produceskill2()
    { 
            Instantiate(skill2, firepoint.transform.position, firepoint.transform.rotation);               
    }
    void produceskill3()
    {
        for (int i = 1; i <= 3; i++)
        {
            Instantiate(skill3, new Vector2(p.transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f), p.transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        }
    }
    void finishattack()
    {
        timer1 = 0;
        isattack = 0;
        skill1.SetActive(false);
        skill3.SetActive(false);        
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
   
    void producecanying()//生成残影
    {
        Instantiate(canying,this.transform.position,Quaternion.identity);
        if (anim.GetInteger("state") == 2)
        {
            Invoke("producecanying",0.1f);
        }
    }
    void reBirth()
    {
        ifdeath = 0;
        idle();        
        isattack = 0;
        this.transform.position = new Vector2(0.576f, 0f);
        hp = fullhp;
    }
    void death()
    {
        ifdeath = 1;
        anim.SetInteger("state", 9);
        sceneManager.SendMessage("finishround");
    }
}
