using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    Vector2 pos = new Vector2();
    Animator anim;
    GameObject action;
    public GameObject hurtedText;
    GameObject sceneManager;
    public GameObject shadow;
    void Start()
    {
        sceneManager = GameObject.Find("sceneManager");
        pos = this.transform.position;
        action = GameObject.Find("scarecrow_action");
        anim = action.GetComponent<Animator>();
    }

    void Update()
    {
        this.transform.position = pos;        
    }
    void hurted(float num)
    {
        anim.SetInteger("state",1);
        Instantiate(hurtedText,new Vector2(this.transform.position.x,this.transform.position.y+0.1f),Quaternion.identity).SendMessage("shownum", (int)num);
        Invoke("idle",0.2f);
        //判断是否有吸血面具
        sceneManager.SendMessage("recover",sceneManager.GetComponent<sceneManager>().ifprop[1]);
    }
    void idle()
    {
        anim.SetInteger("state",0);
    }
    void reBirth()
    {
        idle();
     }
}
