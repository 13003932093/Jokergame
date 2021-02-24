using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recovercircle : MonoBehaviour
{
    public GameObject shadow;
    public GameObject upcross;
    float timer;
    private void Start()
    {
        Destroy(this.gameObject,3.8f);
    }
    void Update()
    {
        upcross.GetComponent<Renderer>().sortingOrder = (int)(shadow.transform.position.y * -100);//确定图层顺序
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                this.GetComponent<AudioSource>().Play();
                collider.SendMessage("recover",10);
            }
        }

    }
}
