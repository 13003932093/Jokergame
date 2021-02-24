using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounddoor : MonoBehaviour
{
    public GameObject sceneManager;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            sceneManager.SendMessage("passround");
            this.gameObject.SetActive(false);
        }
    }
}
