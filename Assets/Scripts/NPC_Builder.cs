using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Builder : MonoBehaviour {
    public GameObject NPC;
    public float zonewidth;
    public float zoneheight;
    public int numNPCS;
    public int numGroupings;
    public List<int>groupings;
    public int numgroups;
    public List<int> NPCIDS;
    // Simplify to 3 


    // Person state machine 
    // Is person strssed, etc?
    // If info his heard multiple times is confirmed
    // breaking down info as A B C D, A A A A Is correct etc.
    // Reliable Expert or Unreliable Layman 

    private GameObject currentNPC;
    public void Start()
    {
        GameObject.Find("AgentNum").GetComponent<Text>().text = numNPCS.ToString();
        GameObject.Find("ComNum").GetComponent<Text>().text = numGroupings.ToString();
        GameObject.Find("RelNum").GetComponent<Text>().text = numgroups.ToString();
    }

	// Use this for initialization
	public void build () 
    {
        for (int i = 0; i < numGroupings; i++)
        {
            groupings.Add(i);
        }
        for (int i = 0; i < numNPCS; i++)
        {
            float x = Random.Range(-(zonewidth / 2), zonewidth / 2);
            float y = 1.2f;
            float z = Random.Range(-(zoneheight / 2), zoneheight / 2);
            currentNPC = Instantiate(NPC, new Vector3(x, y, z), Quaternion.identity);

            if (Random.Range(0.0f,1.0f) > 0.8f)
            currentNPC.GetComponent<NPC>().expert = true;

            if (currentNPC.GetComponent<NPC>().expert == true)
            {
                currentNPC.transform.Find("Expert").gameObject.SetActive(true);
                currentNPC.GetComponent<NPC>().competence = Random.Range(0.8f, 0.98f);
                int thisinfo = Random.Range(0, gameObject.GetComponent<Manager>().informationlist.Count);
                uint _id = gameObject.GetComponent<Manager>().informationlist[thisinfo].id;
                float _time = gameObject.GetComponent<Manager>().informationlist[thisinfo].time;
                float _importance = gameObject.GetComponent<Manager>().informationlist[thisinfo].worldimportance;
                List<int> _content = gameObject.GetComponent<Manager>().informationlist[thisinfo].content;
                currentNPC.GetComponent<NPC>().AddInfo(_id, _time, _importance, _content, 0.99f, true);
                currentNPC.name = "NPC Expert" + i;
            }

            else
            {
                currentNPC.transform.Find("Layman").gameObject.SetActive(true);
                if (Random.Range(0.0f, 1.0f) > 0.4f)
                {
                    currentNPC.GetComponent<NPC>().competence = Random.Range(0.0f, 0.5f);
                    int thisinfo = Random.Range(0, gameObject.GetComponent<Manager>().informationlist.Count);
                    uint _id = gameObject.GetComponent<Manager>().informationlist[thisinfo].id;
                    float _time = gameObject.GetComponent<Manager>().informationlist[thisinfo].time;
                    float _importance = gameObject.GetComponent<Manager>().informationlist[thisinfo].worldimportance;
                    List<int> _content = gameObject.GetComponent<Manager>().informationlist[thisinfo].content;
                    currentNPC.GetComponent<NPC>().AddInfo(_id, _time, _importance, _content, 0.10f, true);
                }
                currentNPC.name = "NPC Layman " + i;
            }
            currentNPC.GetComponent<NPC>().NPCID = i;
            for (int j = 0; j < numgroups; j++)
            {
                assignGroup(currentNPC, j);
            }
        }
	}

    void assignGroup(GameObject current, int group)
    {
        int grouping = groupings[Random.Range(0, groupings.Count)];
        if (group > 0 && current.GetComponent<NPC>().groupings[group - 1] == grouping)
            assignGroup(current, group);

        else
        current.GetComponent<NPC>().groupings.Add(grouping);
    }
    public void setAgents(float agents)
    {
        numNPCS = Mathf.RoundToInt(agents);
        GameObject.Find("AgentNum").GetComponent<Text>().text = numNPCS.ToString();
    }
    public void setCommunities(float communities)
    {
        numGroupings = Mathf.RoundToInt(communities);
        GameObject.Find("ComNum").GetComponent<Text>().text = numGroupings.ToString();
    }

    public void setRelationships(float relationships)
    {
        numgroups = Mathf.RoundToInt(relationships);
        GameObject.Find("RelNum").GetComponent<Text>().text = numgroups.ToString();
    }
}
