using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
	private GameObject Manager;

	// Use this for initialization
	void Start () {
		Manager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		//Manager.GetComponent<Manager>().informationlist.Count;
	}
}
