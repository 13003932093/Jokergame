using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireregion : MonoBehaviour
{
    float timer = 0;
    void Start()
    {
        Destroy(this.gameObject,2);
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                collider.SendMessage("hurted", GameObject.Find("sceneManager").GetComponent<sceneManager>().atk * 0.5f);
                timer = 0;
            }
        }
    }
}
