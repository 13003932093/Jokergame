using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prop1 : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate()
        {
            GameObject.Find("sceneManager").SendMessage("propconfirm", 1);
    });

    }
}
