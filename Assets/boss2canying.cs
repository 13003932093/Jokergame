using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2canying : MonoBehaviour
{
    GameObject boss2;
    int oriention;
    void Start()
    {
        boss2 = GameObject.Find("boss2");
        Destroy(this.gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        oriention = boss2.GetComponent<boss2>().oriention;
        if (oriention == 1)
        {
            this.transform.localScale = new Vector2(-0.4f, 0.4f);
        }
        else
        {
            this.transform.localScale = new Vector2(0.4f, 0.4f);
        }
    }
}
