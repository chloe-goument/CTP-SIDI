using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject NPC;
    public int npcquantity;
    public float zonewidth;
    public float zoneheight;
	public string[] groupingsFamily = new string[4];
	public string[] groupingsProfessions = new string[4];
	public List<int> NPCs = new List<int>();

	private GameObject currentNPC;

	// Depreciated code that used to spawn in the NPCs,
    // butchered and pulled apart to build the new NPC_Builder class.
	void Start ()
	{
   //     for (int q = 0; q < npcquantity; q++)
   //     {
			//// Spawning NPCs within the arena area
   //         float x = Random.Range(-(zonewidth / 2), zonewidth / 2);
   //         float y = 1.2f;
   //         float z = Random.Range(-(zoneheight / 2), zoneheight / 2);
   //         currentNPC = Instantiate(NPC, new Vector3(x, y, z), Quaternion.identity);

			////Setting up the NPC's knowledgebase
			//currentNPC.GetComponent<InformationSharing>().ID = q;


			////Giving the NPC its tags
			//currentNPC.GetComponent<InformationSharing> ().groupings.Add (groupingsFamily[Random.Range(0, 3)]);
			//currentNPC.GetComponent<InformationSharing> ().groupings.Add (groupingsProfessions [Random.Range (0, 3)]);
        //}
    }
	
}
