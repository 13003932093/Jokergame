using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2skill2 : MonoBehaviour
{
    public GameObject attackexplosion;
    GameObject boss2;
    int oriention;
    AudioClip ac;
    private void Start()
    {
        boss2 = GameObject.Find("boss2");
        ac = this.GetComponent<AudioClip>();
        oriention = boss2.GetComponent<boss2>().oriention;
        Invoke("finish", 2);
    }
    void Update()
    {
        if (oriention == 0)
        {
            this.transform.Translate(Vector3.left * 3f * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector3.right * 3f * Time.deltaTime);
            this.transform.localScale = new Vector3(-0.3f, 0.3f);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().gameObject.tag == "Player" || collider.GetComponent<Collider>().gameObject.tag == "Playershield")
        {
            GameObject ae = Instantiate(attackexplosion, this.transform.position, this.transform.rotation);
            if (collider.GetComponent<Collider>().gameObject.tag == "Player")
            {
                collider.GetComponent<Collider>().gameObject.SendMessage("hurted", 12);
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
