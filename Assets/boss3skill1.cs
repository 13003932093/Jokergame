using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3skill1 : MonoBehaviour
{
    public GameObject attackexplosion;
    GameObject boss3;
    int oriention;
    AudioClip ac;
    private void Start()
    {
        boss3 = GameObject.Find("boss3");
        ac = this.GetComponent<AudioClip>();
        oriention = boss3.GetComponent<boss3>().oriention;
        Invoke("finish", 2);
    }
    void Update()
    {
        if (oriention == 0)
        {
            this.transform.Translate(Vector3.left * 1.5f * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector3.right * 1.5f * Time.deltaTime);
            this.transform.localScale = new Vector3(-0.7f, 0.5f);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().gameObject.tag == "Player" ||collider.GetComponent<Collider>().gameObject.tag == "Playershield")
        {
            GameObject ae = Instantiate(attackexplosion, this.transform.position, this.transform.rotation);
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
