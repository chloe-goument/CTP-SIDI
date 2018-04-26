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
    public int numgroups;

    public List<int>groupings;
    public List<int> NPCIDS;

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
        // Build the list of possible social groups the NPCs can belong to.
        for (int i = 0; i < numGroupings; i++)
        {
            groupings.Add(i);
        }

        // This builds each of the NPCs, at the moment it just generates whether
        // each npc is an expert randomly. It wont take long to make the adjustment
        // so that the user can select how many expert or layment agents should be
        // generated into the system.
        for (int i = 0; i < numNPCS; i++)
        {
            // Assigning a location for each
            float x = Random.Range(-(zonewidth / 2), zonewidth / 2);
            float y = 1.2f;
            float z = Random.Range(-(zoneheight / 2), zoneheight / 2);
            currentNPC = Instantiate(NPC, new Vector3(x, y, z), Quaternion.identity);

            // Small chance of spawning them as an expert
            if (Random.Range(0.0f,1.0f) > 0.8f)
            currentNPC.GetComponent<NPC>().expert = true;

            // Code to execute if they are an expert
            if (currentNPC.GetComponent<NPC>().expert == true)
            {
                currentNPC.transform.Find("Expert").gameObject.SetActive(true);

                // Setting their copetence level for communication, this will eventually
                // be a setable value too
                currentNPC.GetComponent<NPC>().competence = Random.Range(0.8f, 0.98f);
                int thisinfo = Random.Range(0, gameObject.GetComponent<Manager>().informationlist.Count);
                uint _id = gameObject.GetComponent<Manager>().informationlist[thisinfo].id;
                float _time = gameObject.GetComponent<Manager>().informationlist[thisinfo].time;
                float _importance = gameObject.GetComponent<Manager>().informationlist[thisinfo].worldimportance;
                List<int> _content = gameObject.GetComponent<Manager>().informationlist[thisinfo].content;
                currentNPC.GetComponent<NPC>().AddInfo(_id, _time, _importance, _content, 0.99f, true);
                currentNPC.name = "NPC Expert" + i;
            }

            // Code to execute if they are a laymen.
            else
            {
                currentNPC.transform.Find("Layman").gameObject.SetActive(true);

                // Setting their copetence level for communication, this will eventually
                // be a setable value too
                currentNPC.GetComponent<NPC>().competence = Random.Range(0.0f, 0.5f);
                if (Random.Range(0.0f, 1.0f) > 0.4f)
                {
                    // Building the piece of information they will hold.
                    int thisinfo = Random.Range(0, gameObject.GetComponent<Manager>().informationlist.Count);
                    uint _id = gameObject.GetComponent<Manager>().informationlist[thisinfo].id;
                    float _time = gameObject.GetComponent<Manager>().informationlist[thisinfo].time;
                    float _importance = gameObject.GetComponent<Manager>().informationlist[thisinfo].worldimportance;
                    List<int> _content = gameObject.GetComponent<Manager>().informationlist[thisinfo].content;

                    // This puts the previous info together, the value of 0.1f was chosen to represent
                    // the laymen being provided with very false information if they are spawned with it. 
                    currentNPC.GetComponent<NPC>().AddInfo(_id, _time, _importance, _content, 0.10f, true);
                }
                currentNPC.name = "NPC Layman " + i;
            }
            currentNPC.GetComponent<NPC>().NPCID = i;
            // Assigning groups.
            for (int j = 0; j < numgroups; j++)
            {
                assignGroup(currentNPC, j);
            }
        }
	}

    // Assign the supplied NPC into a group. If the group matches the previous group,
    // generate a new group. This should really check whether the grouping contains
    // the group at all rather than the previous value but that hasn't been fixed yet.
    void assignGroup(GameObject current, int group)
    {
        int grouping = groupings[Random.Range(0, groupings.Count)];
        if (group > 0 && current.GetComponent<NPC>().groupings[group - 1] == grouping)
            assignGroup(current, group);

        else
        current.GetComponent<NPC>().groupings.Add(grouping);
    }

    // Setters
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
