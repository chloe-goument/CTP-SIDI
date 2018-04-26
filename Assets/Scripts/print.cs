using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class print : MonoBehaviour {
    GameObject _GM;
    GameObject[] npcs;

    float correctness;
    float percentageknown;
    int knowninfo;

	// Use this for initialization
    void Start()
    {
        _GM = GameObject.Find("GameManager");

    }

    // Begin printing a log of the correctness every second (Debugging)
	public void updating () 
    {
        InvokeRepeating("Log", 1.0f, 1.0f);
        npcs = GameObject.FindGameObjectsWithTag("NPC");
    }
	
	void Update () 
    {
        // If the simulation is running, create a printout of the measures of
        // how much of the information is known by how many and a measure of the
        // correctness of that information over the population.
        if (_GM.GetComponent<Manager>().running == true)
        {
            knowninfo = 0;
            correctness = 0;
            foreach (GameObject npc in npcs)
            {
                correctness += npc.GetComponent<NPC>().correctness;
                knowninfo += npc.GetComponent<NPC>().LocalInformation.Count;
            }
            correctness = correctness / npcs.Length;
            GameObject.Find("Correctness").GetComponent<Text>().text = "Correctness: " + Mathf.Round(correctness * 100f) / 100f;
            int numpcs = _GM.GetComponent<NPC_Builder>().numNPCS;
            percentageknown = (numpcs * knowninfo) / (numpcs * _GM.GetComponent<Manager>().informationlist.Count);
            GameObject.Find("InfoMeasure").GetComponent<Text>().text = "Information Known: " + Mathf.Round((percentageknown /numpcs) * 100f) / 100f;
        }
        
	}
    // Debugging.
    void Log()
    {
        Debug.Log(correctness);
    }
}
