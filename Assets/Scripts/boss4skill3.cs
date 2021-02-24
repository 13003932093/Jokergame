using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4skill3 : MonoBehaviour
{
    int ifgo;
    public GameObject boss4;
    int oriention;
    public GameObject pic;
    public GameObject explosion;
    void Start()
    {
        boss4 = GameObject.Find("boss4");
        oriention = boss4.GetComponent<boss4>().oriention;
        Invoke("begingo",0.8f);
        Destroy(this.gameObject,2f);
    }
    void Update()
    {
        pic.transform.Rotate(new Vector3(0, 0, -1500f * Time.deltaTime), Space.Self);
        if (ifgo == 1)
        {
            if (oriention == 0)
            {
                this.transform.Translate(Vector3.left * 1.5f * Time.deltaTime);
            }
            else
            {
                this.transform.Translate(Vector3.right * 1.5f * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (ifgo == 1&& collider.GetComponent<Collider>().gameObject.tag == "Player" || collider.GetComponent<Collider>().gameObject.tag == "Playershield")
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            if (collider.GetComponent<Collider>().gameObject.tag == "Player")
            {
                collider.GetComponent<Collider>().gameObject.SendMessage("hurted", 20);
            }
            Destroy(this.gameObject);
        }
    }
    void begingo()
    {
        ifgo = 1;
    }
}
