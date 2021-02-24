using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class round : MonoBehaviour
{
    public GameObject knife;
    public GameObject ball;
    public GameObject bottle;
    public GameObject bloodknife;
    public GameObject bomb;
    public GameObject gun;
    public GameObject sceneManager;
    public GameObject eyepoint;
    public GameObject lighting;
    public GameObject wall;
    int basicchance;//控制每一波的概率
    public  GameObject p;
    float timer1;
    GameObject playerpos;
    public GameObject white_explosion;
    void Start()
    {
        Origin();
    }

    void Update()
    {
        timer1 += Time.deltaTime;
        if (timer1 >= 5)
        {
            for (int i = 1; i <= 7; i++)
            {
                basicchance = Random.Range(1, 101);
                if (basicchance <= 20)
                {
                    Instantiate(ball, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
                }
                else if (basicchance > 20 && basicchance <= 40)
                {
                    Instantiate(bottle, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
                }
                else
                {
                    Instantiate(knife, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
                }
                timer1 = 0;
            }
            //判断是否有炸弹
            for (int i = 1; i <= sceneManager.GetComponent<sceneManager>().ifprop[2]; i++)
            {
                Instantiate(bomb, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
            }
            //判断是否有枪
            for (int i = 1; i <= sceneManager.GetComponent<sceneManager>().ifprop[5]; i++)
            {
                Instantiate(gun, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
            }
            //判断是否有墙
            for (int i = 1; i <= sceneManager.GetComponent<sceneManager>().ifprop[10]; i++)
            {
                Instantiate(wall, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
            }
            //lighting
            for (int i = 1; i <= sceneManager.GetComponent<sceneManager>().ifprop[7] * 5; i++)
            {
                Instantiate(lighting, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
            }
            //判断血刀
            for (int i = 1; i <= sceneManager.GetComponent<sceneManager>().ifprop[0] * 2; i++)
            {
                Instantiate(bloodknife, new Vector2(Random.Range(-1.7f, 1.7f), Random.Range(-1f, 0.1f)), Quaternion.identity).transform.SetParent(this.transform);
            }
        }
    }
    void Origin()
    {        
        playerpos = GameObject.Find("playerpos");
        p.transform.position = playerpos.transform.position;
        p.GetComponent<player>().ifdeath = 0;
        Instantiate(white_explosion, p.transform.position, p.transform.rotation);
        foreach (Transform child in this.transform)
        {
            if (child.tag == "Enemy")
            {
                child.SendMessage("reBirth");
            }
        }
        if (sceneManager.GetComponent<sceneManager>().ifprop[4] >= 1)
        {
            for (int i = 1; i <= sceneManager.GetComponent<sceneManager>().ifprop[4]; i++)
            {
                Instantiate(eyepoint, p.transform.position, Quaternion.Euler(0, 0, i * 360 / sceneManager.GetComponent<sceneManager>().ifprop[4])).transform.SetParent(this.transform);
            }
        }
    }    
    void End()
    {
        //foreach (Transform child in this.transform)
        //{
        //    if (child.tag == "Enemy")
        //    {
        //        child.SendMessage("reBirth");
        //    }
        //}
    }
    public void ProduceEyes()
    {
        Instantiate(eyepoint, p.transform.position, Quaternion.Euler(0, 0, 360 / sceneManager.GetComponent<sceneManager>().ifprop[4])).transform.SetParent(this.transform);
    }
}
