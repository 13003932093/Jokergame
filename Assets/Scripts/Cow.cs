using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    Animator anim;
    public GameObject action;
    float timer;
    GameObject aimenemy;
    int oriention = 0;
    Vector2 attackway;
    public GameObject shadow;
    public int ifattack;
    public GameObject explosion;
    float timer2;//控制声音播放
    void Start()
    {
        anim = action.GetComponent<Animator>();
        findenemy();
        attackway = (aimenemy.transform.position - this.transform.position) / (Vector2.Distance(aimenemy.transform.position, this.transform.position));
    }
    void Update()
    {        
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if (timer >= 2)
        {
            timer = 0;
            changestate();
        }
        if (timer2 >= 8)
        {
            this.GetComponent<AudioSource>().Play();
            timer2 = 0;
        }
        if (oriention == 0)
        {
            action.transform.localScale = new Vector2(0.5f, 0.5f);
        }
        else
        {
            action.transform.localScale = new Vector2(-0.5f, 0.5f);
        }
        if (anim.GetInteger("state") == 2)
        {
            this.GetComponent<CharacterController>().Move(attackway*1.5f* Time.deltaTime);
        }
    }
    void changestate()
    {
        if (anim.GetInteger("state") == 1)
        {
            anim.SetInteger("state", 2);
            ifattack = 1;
        }
        else
        {
            anim.SetInteger("state",1);
            ifattack = 0;
            if (aimenemy.transform.position.x <= this.transform.position.x)
            {
                oriention = 0;
            }
            else
            {
                oriention = 1;
            }
            findenemy();

            attackway = (aimenemy.transform.position - this.transform.position) / (Vector2.Distance(aimenemy.transform.position, this.transform.position));
        }
    }
    void findenemy()
    {
        float[] dis = new float[999999];//判断各个敌人与本体的距离
        float mindis = 9999;//选出最短距离
        GameObject[] e = new GameObject[100];//敌人列表
        int i = 0;//循环变量
        e = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject child in e)
        {
            if (child.tag == "Enemy")
            {
                i = 0;
                dis[i] = Vector2.Distance(child.transform.position, transform.position);
                e[i] = child.gameObject;
                if (dis[i] <= mindis)
                {
                    mindis = dis[i];
                }
                i++;
            }
        }
        i = 0;
        while (dis[i] != mindis)
        {
            i++;
        }
        aimenemy = e[i];
    }
    //查找最近敌人    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy" && ifattack==1)
        {
            Instantiate(explosion,hit.transform.position,Quaternion.identity);            
            hit.gameObject.SendMessage("hurted",10);
            ifattack = 0; 
        }
    }
}
