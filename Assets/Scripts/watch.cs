using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watch: MonoBehaviour
{
    GameObject player;
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        this.gameObject.transform.position = player.transform.position+new Vector3(0,0,-10);
    }
}
