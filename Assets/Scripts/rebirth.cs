using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rebirth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f * Time.deltaTime, 0);
    }
}
