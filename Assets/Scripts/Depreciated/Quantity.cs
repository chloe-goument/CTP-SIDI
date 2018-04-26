using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quantity : MonoBehaviour {

	private GameObject[] NPCs;
	public int numberofinfo = 1;
	public Text texter;

	void Start ()
	{
		texter = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		texter.text = numberofinfo.ToString();
	}
}
