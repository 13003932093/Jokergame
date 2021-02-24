using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_action : MonoBehaviour
{
    public GameObject boss1;
    public GameObject attack1shell;
    public GameObject firepoint;
    void produceattack1shell()
    {
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation);
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, 45);
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, -45);
    }
    void produceattack1shell_two()
    {
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation);
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, 45);
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, -45);
    }
    void produceattack1shell_three()
    {
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation);     
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, 45);
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, -45);
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, 100);
        Instantiate(attack1shell, firepoint.transform.position, firepoint.transform.rotation).transform.localEulerAngles = new Vector3(0, 0, -100);
    }
}
