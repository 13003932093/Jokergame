using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class replay : MonoBehaviour
{
    public GameObject state;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            GameObject.Find("sceneManager").SendMessage("Replay");
            state.gameObject.SetActive(false);
        });
    }    
}
