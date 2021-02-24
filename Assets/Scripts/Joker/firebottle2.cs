﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

public class firebottle2 : MonoBehaviour
{
	float movespeed = 2f;
	GameObject aimenemy;
	public GameObject fireregion;
	void Start()
	{
		//初始时指向与自己最近的敌人
		findenemy();
		if (aimenemy.transform.position.x >= this.transform.position.x)
		{
			this.transform.Rotate(new Vector3(0, 0, (float)(180 * Math.Atan((aimenemy.transform.position.y - this.transform.position.y) / (aimenemy.transform.position.x - this.transform.position.x)) / Math.PI - 90)));
		}
		else
		{
			this.transform.Rotate(new Vector3(0, 0, (float)(180 * Math.Atan((aimenemy.transform.position.y - this.transform.position.y) / (aimenemy.transform.position.x - this.transform.position.x)) / Math.PI + 90)));
		}
		Destroy(this.gameObject,2.1f);
		Invoke("producefireregion",2f);
	}

	void Update()
	{
		transform.Translate(Vector3.up * movespeed * Time.deltaTime);
	}
	//查找最近敌人
	void findenemy()
	{
		float[] dis = new float[100];//判断各个敌人与本体的距离
		float mindis = 9999;//选出最短距离
		GameObject[] e = new GameObject[100];//敌人列表
		int i = 0;//循环变量
		e = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach (GameObject child in e)
		{
			if (child.tag == "Enemy")
			{
				i = 0;
				dis[i] = Vector2.Distance(child.transform.position, transform.position);
				e[i] = child.gameObject;
				if (dis[i] <= mindis)
				{
					mindis = dis[i];
				}
				i++;
			}
		}
		i = 0;
		while (dis[i] != mindis)
		{
			i++;
		}
		aimenemy = e[i];
	}
	//查找最近敌人
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Enemy"||collider.gameObject.tag=="Enviroment")
		{
			producefireregion();
			Destroy(this.gameObject);
		}
	}
	void producefireregion()
	{
		GameObject ae= Instantiate(fireregion, this.transform.position, Quaternion.identity);
		ae.GetComponent<AudioSource>().Play();
	}
}
