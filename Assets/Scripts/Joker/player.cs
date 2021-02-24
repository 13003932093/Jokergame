using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    float moveSpeed = 1.2f;
    public Animator[] anim = new Animator[7];//控制角色职业
    //1 Joker
    //2 Angle
    //3 Cowboy
    //4 Demon
    //5 Knight
    //6 Ninja
    public GameObject[] action = new GameObject[7];
    public int characternum;//定义目前选择的角色职业
    public  int oriention = 0;//控制朝向，0为左，1为右
    public GameObject rotatepoint;
    public int skill = 0;//判断技能键是否摁下
    public GameObject white_explosion;
    int lockmove;
    GameObject sceneManager;
    public GameObject hurtedText;
    public int canbehurt = 1;
    public GameObject gun;    
    public int ifattack1;
    public int ifattack2;//Cowboy专用
    public int ifdeath;//判断是否死亡
    public GameObject recoverText;
    public int ifPoison;
    public int ifSlow;
    public GameObject poisonShow;
    public GameObject slowShow;
    float vNow;
    float poisonTimer;
    public GameObject poisonHurtedShow;
    public Slider gunSlider;
    int gunNum = 5;
    public int state = 0;//通过检测sceneManager的state来控制移动
    //0 idle
    //1 left
    //2 right
    //3 up
    //4 down
    public GameObject shadow;
    public GameObject recovercircle;
    public GameObject knightshield;
    public GameObject angleshield;
    float angletimer;
    public GameObject ninjaattack;
    public GameObject rotatepoint2;//牛仔被动
    public GameObject gun2;   
    public Slider gunSlider2;
    int gunNum2 = 5;
    public Slider angleshieldslider;
    public Slider knightshieldslider;
    public GameObject rebirthshow;
    public GameObject skillshow;
    public GameObject cowboyskill;
    public Slider cowboyslider;    
    void Start()
    {
        sceneManager = GameObject.Find("sceneManager");
        slowShow.GetComponent<Renderer>().sortingLayerName = "UI";
        characternum = 1;
        CharacterChange();
    }
    void Update()
    {                
        //判断状态字体方向 
        if (poisonShow.activeInHierarchy == true)
        {
            if (oriention == 0)
            {
                poisonShow.transform.localScale = new Vector2(0.5f, 0.5f);
            }
            else
            {
                poisonShow.transform.localScale = new Vector2(-0.5f, 0.5f);
            }
        }
        if (slowShow.activeInHierarchy == true)
        {
            if (oriention == 0)
            {
                slowShow.transform.localScale = new Vector2(0.002f, 0.002f);
            }
            else
            {
                slowShow.transform.localScale = new Vector2(-0.002f, 0.002f);
            }
        }
        if (ifdeath == 0)
        {
            state = sceneManager.GetComponent<sceneManager>().state;
            skill = sceneManager.GetComponent<sceneManager>().skill;//与sceneManager保持一致            
            if (lockmove == 0)
            {
                //释放技能
                if (skill == 1)
                {
                    useskill();
                }                
                //移动
                if (state == 0) idle();
                if (state == 1) goleft();
                if (state == 2) goright();
                if (state == 3) goup();
                if (state == 4) godown();
                //Ninja释放拔刀斩
                if (Input.GetKeyDown(KeyCode.J) && characternum == 6 && rotatepoint.GetComponent<rotatepoint>().ninjacanskill == 1)
                {
                    useskill();
                    //rotatepoint.SendMessage("ninjafinishskill");
                }
            }
            //切换角色
            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))&&sceneManager.GetComponent<sceneManager>().roundnum==0) CharacterChange();                                 
            //判断其他方向
            if (gun.activeInHierarchy == true)
            {
                if (oriention == 0)
                {
                    gunSlider.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    gunSlider.transform.localScale = new Vector2(-1, 1);
                }
            }
            if (gun2.activeInHierarchy)
            {
                if (oriention == 0)
                {
                    gunSlider2.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    gunSlider2.transform.localScale = new Vector2(-1, 1);
                }
            }
            if (angleshieldslider.IsActive()==true)
            {
                if (oriention == 0)
                {
                    angleshieldslider.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    angleshieldslider.transform.localScale = new Vector2(-1, 1);
                }
            }
            if (angleshield.activeInHierarchy)
            {
                if (oriention == 0)
                {
                    angleshield.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    angleshield.transform.localScale = new Vector2(-1, 1);
                }
            }
            if (knightshieldslider.IsActive() == true)
            {
                if (oriention == 0)
                {
                    knightshieldslider.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    knightshieldslider.transform.localScale = new Vector2(-1, 1);
                }
            }
            if (cowboyslider.IsActive() == true)
            {
                if (oriention == 0)
                {
                    cowboyslider.transform.localScale = new Vector2(1, 1);
                }
                else
                {
                    cowboyslider.transform.localScale = new Vector2(-1, 1);
                }
            }
            if (cowboyskill.activeInHierarchy)
            {
                if (oriention == 0)
                {
                    cowboyskill.transform.localScale = new Vector2(0.5f, 0.5f);
                }
                else
                {
                    cowboyskill.transform.localScale = new Vector2(-0.5f, 0.5f);
                }
            }

            //持枪射击
            if (gun.activeInHierarchy == true)
            {                
                gunSlider.value += Time.deltaTime / 1f;
                if (gunSlider.value >= 1)
                {
                    rotatepoint.SendMessage("gunattack");
                    gunNum--;
                    gunSlider.value = 0;                    
                }
                if (gunNum == 0)
                {
                    Invoke("cancelgun", 0.5f);
                    gunNum = 5;
                }
            }
            //Cowboy额外射击
            if (gun2.activeInHierarchy == true)
            {                
                gunSlider2.value += Time.deltaTime / 1.5f;
                if (gunSlider2.value >= 1)
                {
                    rotatepoint2.SendMessage("gunattack");
                    gunNum2--;
                    gunSlider2.value = 0;
                    
                }
                if (gunNum2 == 0)
                {
                    Invoke("cancelgun2", 0.5f);
                    gunNum2 = 5;
                }
            }
            //中毒伤害
            if (ifPoison > 0)
            {
                poisonShow.SetActive(true);
                poisonTimer += Time.deltaTime;
                if (poisonTimer >= 1)
                {
                    poisonTimer = 0;
                    PoisonHurted((int)(sceneManager.GetComponent<sceneManager>().playerhp * 0.05f * 10) / 10);
                }
            }
            else
            {
                poisonShow.SetActive(false);
            }
            //判断恢复
            if (sceneManager.GetComponent<sceneManager>().recoverHpNum != 0)
            {
                recover(sceneManager.GetComponent<sceneManager>().recoverHpNum);
                sceneManager.GetComponent<sceneManager>().recoverHpNum = 0;
            }
            //判断死亡
            if (sceneManager.GetComponent<sceneManager>().playerhp <= 0)
            {
                Death();
            }
            //天使被动
            if (characternum == 2 &&!angleshield.activeInHierarchy)
            {
                if (angleshieldslider.GetComponent<CanvasGroup>().alpha == 0)
                {
                    angleshieldslider.GetComponent<CanvasGroup>().alpha = 1;
                    angleshieldslider.GetComponent<CanvasGroup>().interactable =true;
                    angleshieldslider.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
                angleshieldslider.value += Time.deltaTime / 10;
                if (angleshieldslider.value>=1)
                {
                    angleshield.SetActive(true);
                    angleshieldslider.value = 0;
                    angleshieldslider.GetComponent<CanvasGroup>().alpha = 0;
                    angleshieldslider.GetComponent<CanvasGroup>().interactable = false;
                    angleshieldslider.GetComponent<CanvasGroup>().blocksRaycasts = false;                    
                }
            }
            //Knight盾牌时间显示
            if (knightshield.activeInHierarchy)
            {
                knightshieldslider.value -= Time.deltaTime / 8;
                if (knightshieldslider.value <= 0.1f)
                {
                    knightshield.SetActive(false);
                    knightshieldslider.value = 1;
                    knightshieldslider.GetComponent<CanvasGroup>().alpha = 0;
                    knightshieldslider.GetComponent<CanvasGroup>().interactable = false;
                    knightshieldslider.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
            //Cowboy技能图标显示
            if (cowboyskill.activeInHierarchy)
            {
                cowboyslider.value -= Time.deltaTime / 8;
                if (cowboyslider.value <= 0.1f)
                {
                    cowboyskill.SetActive(false);
                    cowboyslider.value = 1;
                    cowboyslider.GetComponent<CanvasGroup>().alpha = 0;
                    cowboyslider.GetComponent<CanvasGroup>().interactable = false;
                    cowboyslider.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
        }
    }
    public void goup()
    {
        if (lockmove == 0)
        {
            move();
            this.GetComponent<CharacterController>().Move(Vector2.up * moveSpeed * Time.deltaTime);
        }
    }
    public void godown()
    {
        if (lockmove == 0)
        {
            move();
            this.GetComponent<CharacterController>().Move(Vector2.down * moveSpeed * Time.deltaTime);
        }
    }
    public void goleft()
    {
        if (lockmove == 0)
        {
            move();
            this.GetComponent<CharacterController>().Move(Vector2.left * moveSpeed * Time.deltaTime);
            if (oriention != 0)
            {
                oriention = 0;
                this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
            }//更改朝向
        }
    }
    public void goright()
    {
        if (lockmove == 0)
        {
            move();
            this.GetComponent<CharacterController>().Move(Vector2.right * moveSpeed * Time.deltaTime);
            if (oriention != 1)
            {
                oriention = 1;
                this.transform.localScale = new Vector2(-this.transform.localScale.x, this.transform.localScale.y);
            }//更改朝向
        }
    }
    public void idle()
    {
        if (action[characternum].activeInHierarchy)
        {
            anim[characternum].SetInteger("state", 0);
        }

    }
    public void move()
    {
        if (anim[characternum].GetInteger("state") == 0 && action[characternum].activeInHierarchy)
        {
            anim[characternum].SetInteger("state", 1);
        }
    }
    public void skillaction()
    {
        anim[characternum].SetInteger("state", 2);
    }
    //从拾取道具发出的函数 需传递到roundpoint(三种)
    void knifeattack()
    {
        rotatepoint.SendMessage("knifeattack");
    }
    void ballattack()
    {
        rotatepoint.SendMessage("ballattack");
    }
    void bottleattack()
    {
        rotatepoint.SendMessage("bottleattack");
    }
    void bloodknifeattack()
    {
        rotatepoint.SendMessage("bloodknifeattack");
    }
    void bombattack()
    {
        rotatepoint.SendMessage("bombattack");
    }
    void havegun()
    {
        gun.gameObject.SetActive(true);
        ifattack1++;
    }
    void cancelgun()
    {
        gun.gameObject.SetActive(false);
        ifattack1--;
    }
    //Cowboy额外的拾取道具
    void knifeattack2()
    {
        if(cowboyskill.activeInHierarchy)
        rotatepoint2.SendMessage("knifeattack");
    }
    void ballattack2()
    {
        if (cowboyskill.activeInHierarchy)
            rotatepoint2.SendMessage("ballattack");

    }
    void bottleattack2()
    {
        if (cowboyskill.activeInHierarchy)
            rotatepoint2.SendMessage("bottleattack");
    }
    void bloodknifeattack2()
    {
        if (cowboyskill.activeInHierarchy)
            rotatepoint2.SendMessage("bloodknifeattack");
    }
    void bombattack2()
    {
        if (cowboyskill.activeInHierarchy)
            rotatepoint2.SendMessage("bombattack");
    }
    void havegun2()
    {
        if (cowboyskill.activeInHierarchy)
            gun2.gameObject.SetActive(true);
        ifattack2++;
    }
    void cancelgun2()
    {
        if (cowboyskill.activeInHierarchy)
            gun2.gameObject.SetActive(false);
        ifattack2--;
    }
    //不同角色的使用技能
    public void useskill()
    {
        if (characternum == 1)//Joker
        {
            if (oriention == 1)
            {
                Invoke("skillTransright", 0.2f);
            }
            else
            {
                Invoke("skillTransleft", 0.2f);
            }
            canbehurt = 0;
            Instantiate(white_explosion, this.transform.position, Quaternion.identity);
            Lock();
            skillaction();
            Invoke("unLock", 0.3f);
            Invoke("health", 0.3f);
            Invoke("idle", 0.3f);
            action[1].GetComponent<AudioSource>().Play();
            sceneManager.GetComponent<sceneManager>().skill = 0;//skill清0
        }
        if (characternum == 2)//Angle
        {
            Instantiate(recovercircle, this.transform.position, Quaternion.identity);
            sceneManager.GetComponent<sceneManager>().skill = 0;//skill清0
        }
        if (characternum == 3)//Cowboy
        {
            cowboyskill.SetActive(true);
            cowboyslider.GetComponent<CanvasGroup>().alpha = 1;
            cowboyslider.GetComponent<CanvasGroup>().interactable = true;
            cowboyslider.GetComponent<CanvasGroup>().blocksRaycasts = true;            
            sceneManager.GetComponent<sceneManager>().skill = 0;//skill清0
        }
        if (characternum == 4)//Demon
        {
            Lock();
            skillaction();
            Invoke("unLock", 0.6f);
            Invoke("idle", 0.6f);
            action[4].GetComponent<AudioSource>().Play();
            sceneManager.GetComponent<sceneManager>().skill = 0;
        }
        if (characternum == 5)//Knight
        {
            knightshield.SetActive(true);
            knightshieldslider.GetComponent<CanvasGroup>().alpha = 1;
            knightshieldslider.GetComponent<CanvasGroup>().interactable = true;
            knightshieldslider.GetComponent<CanvasGroup>().blocksRaycasts = true;
            sceneManager.GetComponent<sceneManager>().skill = 0;
        }
        if (characternum == 6)//Ninja
        {
            ninjaattack.SetActive(true);
            action[6].GetComponent<AudioSource>().Play();           
            if (oriention == 1)
            {
                Invoke("skillTransright", 0.1f);
            }
            else
            {
                Invoke("skillTransleft", 0.1f);
            }
            canbehurt = 0;
            Lock();
            skillaction();
            Invoke("unLock", 0.4f);
            Invoke("health", 0.4f);
            Invoke("idle", 0.4f);
            Invoke("ninjaattattackfinish", 0.4f);
            sceneManager.GetComponent<sceneManager>().skill = 0;//skill清0
        }

    }
    //限制移动函数
    void Lock()
    {
        lockmove = 1;
    }
    void unLock()
    {
        lockmove = 0;
    }
    //技能的传送
    void skillTransup()
    {
        this.GetComponent<CharacterController>().Move(Vector2.up * 0.7f);
    }
    void skillTransleft()
    {
        this.GetComponent<CharacterController>().Move(Vector2.left * 0.7f);
    }
    void skillTransright()
    {
        this.GetComponent<CharacterController>().Move(Vector2.right * 0.7f);
    }
    void skillTransdown()
    {
        this.GetComponent<CharacterController>().Move(Vector2.down * 0.7f);
    }
    //三种受伤方式
    void hurted(float num)
    {
        if (canbehurt == 1 && ifdeath == 0)
        {
            if (angleshield.activeInHierarchy)
            {
                angleshield.SetActive(false);
                num = 0;
            }
            if (sceneManager.GetComponent<sceneManager>().ifprop[8] >= 1)
            {
                num =(int)((num - num * 0.3f) * 10) / 10;
                Slow();
            }
            num = (int)((num + 0.1 * num * sceneManager.GetComponent<sceneManager>().ifprop[11]) * 10) / 10;
            canbehurt = 0;
            anim[characternum].SetInteger("state2", 1);
            Instantiate(hurtedText, new Vector2(this.transform.position.x, this.transform.position.y + 0.1f), Quaternion.identity).SendMessage("shownum", (int)num);
            sceneManager.SendMessage("hurted", (int)num);
            Invoke("health", 0.5f);
        }
    }
    void PoisonHurted(float num)
    {
        anim[characternum].SetInteger("state2", 1);
        Instantiate(poisonHurtedShow, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f), Quaternion.identity).SendMessage("shownum", num);
        sceneManager.SendMessage("hurted", (int)num);
    }
    void PropHurted(float num)
    {
        anim[characternum].SetInteger("state2", 1);
        Instantiate(hurtedText, new Vector2(this.transform.position.x, this.transform.position.y + 0.1f), Quaternion.identity).SendMessage("shownum", num);
        sceneManager.SendMessage("hurted", (int)num);
    }
    //三种受伤方式

    void recover(float num)
    {        
        Instantiate(recoverText, new Vector2(this.transform.position.x, this.transform.position.y + 0.1f), Quaternion.identity).SendMessage("shownum", (int)num);
        sceneManager.SendMessage("recover", (int)num);
    }
    void health()
    {
        anim[characternum].SetInteger("state2", 0);
        canbehurt = 1;
    }
    public void Poison()
    {
        ifPoison++;
        if (characternum != 5)
        {
            Invoke("DePoison", 5f);
        }
        else Invoke("DePoison",2.5f);
        
    }
    public void DePoison()
    {
        if (ifPoison > 0)
        {
            ifPoison--;
        }
    }
    public void Slow()
    {
        ifSlow++;
        if(characternum!=5) Invoke("DeSlow", 3.3f);
        else Invoke("DeSlow", 1.6f);
        if (ifSlow == 1)//初次受到减速
        {
            vNow = moveSpeed;
            moveSpeed = (float)((moveSpeed - 0.3 * moveSpeed) * 10) / 10;
            slowShow.SetActive(true);
        }
    }
    public void DeSlow()
    {
        ifSlow--;
        if (ifSlow == 0)//减速效果结束
        {
            moveSpeed = vNow;
            slowShow.SetActive(false);
        }
    }
    public void Death()
    {
        anim[characternum].SetInteger("state", 9);
        ifdeath = 1;
        rotatepoint.SendMessage("finishattack");
        if (sceneManager.GetComponent<sceneManager>().ifprop[9] >= 1)//重生
        {
            Instantiate(rebirthshow,this.transform.position+new Vector3(0,0.3f,0),Quaternion.identity);
            sceneManager.GetComponent<sceneManager>().ifprop[9]--;
            sceneManager.GetComponent<sceneManager>().playerhp = sceneManager.GetComponent<sceneManager>().playerfullhp;
            Invoke("Rebirth",3f);
        }
    }
    public void Rebirth()
    {
        ifdeath = 0;
        Instantiate(white_explosion,this.transform.position,Quaternion.identity);
        rotatepoint.SetActive(true);
        idle();
    }
    public void CharacterChange()
    {
        characternum = sceneManager.GetComponent<sceneManager>().characternum;
        Instantiate(white_explosion, this.transform.position, Quaternion.identity);
        Lock();
        Invoke("unLock",0.3f);
        for (int i = 1; i <= 6; i++)
        {
            if (i == characternum)
            {
                action[i].SetActive(true);
            }
            else
            {
                action[i].SetActive(false);
            }
        }
    }
    public void ninjaattattackfinish()
    {
        ninjaattack.SetActive(false);
    }
}
