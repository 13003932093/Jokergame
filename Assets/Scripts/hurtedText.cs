using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hurtedText : MonoBehaviour
{
	public TextMesh hurtednum;
	void Start()
	{
		Destroy(this.gameObject, 0.5f);
		this.GetComponent<Renderer>().sortingLayerName="UI";
	}
	void Update()
	{
		transform.Translate(0, 0.05f * Time.deltaTime, 0);
	}
	void shownum(float num)
	{
		hurtednum.text = num.ToString();
	}
}
