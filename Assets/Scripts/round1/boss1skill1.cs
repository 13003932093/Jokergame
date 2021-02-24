using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss1skill1: MonoBehaviour
{
    public GameObject attackexplosion;
    GameObject boss1;
    int oriention;
    AudioClip ac;
    private void Start()
    {
        boss1 = GameObject.Find("boss1");
        ac = this.GetComponent<AudioClip>();
        oriention = boss1.GetComponent<boss1>().oriention;
        Invoke("finish",2);
    }
    void Update()
    {
        if (oriention == 0)
        {
            this.transform.Translate(Vector3.left * 2f * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector3.right * 2f * Time.deltaTime);
            this.transform.localScale = new Vector3(-0.7f, 0.5f);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().gameObject.tag == "Player" || collider.GetComponent<Collider>().gameObject.tag == "Enviroment" || collider.GetComponent<Collider>().gameObject.tag == "Playershield")
        {
            GameObject ae= Instantiate(attackexplosion, this.transform.position, this.transform.rotation);
            if (collider.GetComponent<Collider>().gameObject.tag == "Player")
            {
                collider.GetComponent<Collider>().gameObject.SendMessage("hurted", 10);
                ae.GetComponent<AudioSource>().Play();
            }
            Destroy(this.gameObject);
        }
    }
    void finish()
    {
        Instantiate(attackexplosion, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

}
