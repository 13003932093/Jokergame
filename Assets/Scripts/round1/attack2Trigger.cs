﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack2Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.SendMessage("hurted", 15);
        }
    }
}