using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject attackexplosion;
    GameObject p;
    int oriention;
    AudioClip ac;
    private void Start()
    {
        p = GameObject.Find("player");
        ac = this.GetComponent<AudioClip>();
        oriention = p.GetComponent<player>().oriention;
        Invoke("finish", 1.5f);
    }
    void Update()
    {
        if (oriention == 0)
        {
            this.transform.Translate(Vector3.left * 2f * Time.deltaTime);
            this.transform.localScale = new Vector3(-0.1f, 0.1f);
        }
        else
        {
            this.transform.Translate(Vector3.right * 2f * Time.deltaTime);            
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().gameObject.tag == "Enemy" || collider.GetComponent<Collider>().gameObject.tag == "Enviroment")
        {
            GameObject ae = Instantiate(attackexplosion, this.transform.position, this.transform.rotation);
            if (collider.GetComponent<Collider>().gameObject.tag == "Enemy")
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
