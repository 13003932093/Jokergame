using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    public Slider playerhpSlider;
    public Slider playermpSlider;
    public float playerfullhp;
    public float playerhp;
    public float playerfullmp;
    public float playermp;
    public Text playerhpnow;
    public Text playerhpmax;
    public Text playermpmax;
    public Text playermpnow;
    public float atk;//定义攻击力
    public GameObject[] bossstate = new GameObject[11];
    public int roundnum;
    public GameObject[] round = new GameObject[11];
    public GameObject grounddoor;
    public GameObject[] prop = new GameObject[12];
    public GameObject propchooseshow;
    public GameObject watch;
    public GameObject[] stateprop = new GameObject[12];
    int propnum = 0;
    float propx, propy;
    public GameObject can;
    public int[] ifprop = new int[12];//判断是否拥有该道具
    float encitimer;//enci计数器
    public float recoverHpNum;//用来与主角通信用的
    public int state;//控制角色移动
    public int skill;//控制技能
    public int characternum;//控制当前选择的角色
                            //1 Joker
                            //2 Angle
                            //3 Cowboy
                            //4 Demon
                            //5 Knight
                            //6 Ninja
    public GameObject[] playerstate = new GameObject[7];//定义6个角色的状态栏
    //1 Joker
    //2 Angle
    //3 Cowboy
    //4 Demon
    //5 Knight
    //6 Ninja
    public GameObject Cow;
    public GameObject[] howtoplay = new GameObject[4];//定义帮助说明文字
    int n;//定义当前为第几段说明文字
    public GameObject win;
    public GameObject lose;    
    void Start()
    {
        n = 1;
        characternum = 1;
        playerfullhp = 100;
        playerfullmp = 100;
        playerhp = playerfullhp;
        playermp = playerfullmp;
        roundnum = 0;
        atk = 10;
        Screen.SetResolution(
        1024,
        768,
        false);//固定分辨率
        for (int i = 1; i <= 6; i++)
        {
            if (i == characternum)
            {
                playerstate[i].SetActive(true);
            }
            else playerstate[i].SetActive(false);
        }
        Beginhelp();
        for (int i = 1; i <= 4; i++)
        {
            round[i].SetActive(false);
            bossstate[i].SetActive(false);
        }
        round[0].SetActive(true);
        grounddoor.SetActive(true);
    }

    void Update()
    {
        //控制角色移动
        if (Input.GetKey(KeyCode.A)) state = 1;
        if (Input.GetKey(KeyCode.D)) state = 2;
        if (Input.GetKey(KeyCode.W)) state = 3;
        if (Input.GetKey(KeyCode.S)) state = 4;
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) state = 0;
        if (Input.GetKeyDown(KeyCode.Space) && playermp >= playerfullmp)
        {
            skill = 1;
            state = 5;
            playermp = 0;
        }
        //控制角色切换
        if (roundnum == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                characternum--;
                if (characternum <= 0) characternum = 6;
                for (int i = 1; i <= 6; i++)
                {
                    if (i == characternum)
                    {
                        playerstate[i].SetActive(true);
                    }
                    else playerstate[i].SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                characternum++;
                if (characternum >= 7) characternum = 1;
                for (int i = 1; i <= 6; i++)
                {
                    if (i == characternum)
                    {
                        playerstate[i].SetActive(true);
                    }
                    else playerstate[i].SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                propchoose();
            }
        }        
        //恶魔血量越低攻击越高
        if (characternum == 4)
        {
            atk = (int)20-(playerhp/playerfullhp)*10;
        }
        //召唤牛
        if (characternum == 3)
        {
            Cow.SetActive(true);
        }
        else
        {
            Cow.SetActive(false);
        }
        //血条与蓝条的同步显示
        playerhpmax.text = playerfullhp.ToString();
        playerhpnow.text = playerhp.ToString();
        //不同角色能量的恢复速度
        if (playermp < playerfullmp)
        {
            if (characternum == 1)//Joker 5s
            {
                playermp += 20f * Time.deltaTime;
            }
            if (characternum == 2)//Angle 16s
            {
                playermp += 6.25f * Time.deltaTime;
            }
            if (characternum == 3)//Cowboy 16s
            {
                playermp += 6.25f * Time.deltaTime;
            }
            if (characternum == 4)//Demon 10s
            {
                playermp += 10f * Time.deltaTime;
            }
            if (characternum == 5)//Knight 16s
            {
                playermp += 6.25f * Time.deltaTime;
            }
            if (characternum == 6)//Ninja 8s
            {
                playermp += 12.5f * Time.deltaTime;
            }
        }     
        playerhpSlider.value = playerhp / playerfullhp;
        playermpSlider.value = playermp / playerfullmp;
        if (playerhp > playerfullhp)
        {
            playerhp = playerfullhp;
        }
        //判断死亡
        if (playerhp <= 0)
        {
            Lose();
        }        
        //恩赐
        if (ifprop[3] >= 1)
        {
            encitimer += Time.deltaTime;
            if (encitimer >= 5)
            {
                recover((int)playerfullhp * 0.03f);
                encitimer = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Win();  
          }
    }
    void hurted(float num)
    {
        if (playerhp >= 0) playerhp -= num;
    }
    void recover(float num)
    {
        if (recoverHpNum == 0)
        {
            recoverHpNum = num;
            playerhp += num;
        }
    }
    void passround()
    {
        watch.SetActive(true);
        //上一个场景隐形
        round[roundnum].SendMessage("End");
        bossstate[roundnum].SetActive(false);
        round[roundnum].SetActive(false);
        //技能选择
        //propchoose();
        //判断是否通关
        if(roundnum<4) nextround();
        else
        {
            Win();
        }

    }
    void finishround()
    {
        grounddoor.gameObject.SetActive(true);
        grounddoor.GetComponent<AudioSource>().Play();
    }
    void nextround()
    {
        //新的场景出现
        //foreach (Transform child in propchooseshow.transform)
        //{
        //    if (child.gameObject.name != "Text")
        //    {
        //        Destroy(child.gameObject);
        //    }
        //}
        //propchooseshow.SetActive(false);
        //Time.timeScale = 1;
        roundnum++;
        round[roundnum].SetActive(true);
        if (roundnum == 1)
        {
            for (int i = 1; i <= 3; i++)
            {
                howtoplay[i].SetActive(false);
            }
        }
        watch.SetActive(false);
        bossstate[roundnum].SetActive(true);
        round[roundnum].SendMessage("Origin");
    }
    void propchoose()
    {
        Time.timeScale = 0;
        propchooseshow.SetActive(true);
        foreach (Transform child in propchooseshow.transform)
        {
            if (child.gameObject.name != "Text")
            {
                Destroy(child.gameObject);
            }
        }
        //清除道具选择
        for (int n = 1; n <= 3; n++)
        {
            int i = Random.Range(0, 12);
            GameObject p = Instantiate(prop[i], new Vector3(0, 0, 0), Quaternion.identity);
            p.transform.SetParent(propchooseshow.transform);
            p.GetComponent<RectTransform>().anchoredPosition = new Vector2(-110 * (n - 2), 70);
        }
    }
    public void propconfirm(int i)
    {

        if (i == -1)
        {
            i = 0;
        }
        GameObject s = Instantiate(stateprop[i], new Vector3(0, 0, 0), Quaternion.identity);
        s.transform.SetParent(can.transform);
        s.GetComponent<RectTransform>().anchoredPosition = new Vector2(-160 + (40 * (propnum % 4)), 335 - (propnum / 4) * 35);
        ifprop[i]++;
        propnum++;
        //邪神之眼
        if (i == 4)
        {
            round[roundnum].SendMessage("ProduceEyes");
        }
        //跳到下一关
        //nextround();
        //吸血面具
        if (i == 1)
        {
            playerfullhp -= 20;
        }
        foreach (Transform child in propchooseshow.transform)
        {
            if (child.gameObject.name != "Text")
            {
                Destroy(child.gameObject);
            }
        }
        //炸弹
        if (i == 2)
        {
            playerfullhp += 10;
        }
        //回满血
        if (i == 6)
        {
            playerfullhp += 20;
        }
        //重生
        if (i == 9)
        {
            playerfullhp -= 20;
        }
        //壁垒
        if (i == 10)
        {
            playerfullhp += 10;
        }
        //清除道具选择
        Time.timeScale = 1;
        propchooseshow.SetActive(false);
    }
    public void Beginhelp()//开始时的游戏教学
    {
        if (roundnum == 0)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (i != n)
                {
                    howtoplay[i].SetActive(false);
                }
                else
                {
                    howtoplay[i].SetActive(true);
                }
            }
            n++;
            if (n <= 3)
            {
                Invoke("Beginhelp", 5);
            }
        }            
    }
    public void Lose()
    {        
        Time.timeScale = 0;
        lose.SetActive(true);
    }
    public void Win()
    {        
        Time.timeScale = 0;
        win.SetActive(true);
    }
    public void ReBirth()
    {

    }
    public void Replay()//重新开始
    {
        Time.timeScale = 1;
        roundnum = 0;
        characternum = 1;
        playerfullhp = 100;
        playerfullmp = 100;
        playerhp = playerfullhp;
        playermp = playerfullmp;
        atk = 10;
        n = 1;
        Beginhelp();
        for (int i = 0; i <= 11; i++)
        {
            ifprop[i] = 0;
        }
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.tag== "Propshow")
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 1; i <= 4; i++)
        {
            round[i].SetActive(false);
            bossstate[i].SetActive(false);
        }
        round[0].SetActive(true);
        round[0].SendMessage("Origin");
        grounddoor.SetActive(false);
        Invoke("Dooractive",0.1f);        
    }
    public void Dooractive()
    {
        grounddoor.SetActive(true);
    }
}
