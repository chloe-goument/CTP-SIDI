using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationSharing : MonoBehaviour {
    //Public
    public int ID;
    public bool degrades;
    public float recency;
	public float importance;
    public float broadcastrange;

	public List<string> groupings = new List<string>();
	public List<int> informationheld = new List<int>();

    public Material newMat;

    public Renderer rend;

    // Depreciated class showing how information sharing used to occur in the
    // system during the demo phase. 
    void Start () {

		rend = GetComponent<Renderer>();

		//If you start with info....
		if (informationheld.Contains(2) || informationheld.Contains(1) || informationheld.Contains(3))
		{
			rend.material = newMat;
			GetComponent<Light> ().intensity= GetComponent<Light>().intensity + 0.5f;
		}


        if (broadcastrange <= 0)
        {
            broadcastrange = Random.Range(1.5f, 4.0f);
        }
        GetComponent<SphereCollider>().radius = broadcastrange;
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "NPC")
		{
			//Search other character for their common groupings
			for (int q = 0; q < groupings.Count; q++)
			{
				if (other.GetComponent<InformationSharing>().groupings.Contains(groupings[q]))
				{
					// Does the other person Know the info?
					for (int i = 0; i < informationheld.Count; i++)
					{
						if (!other.GetComponent<InformationSharing>().informationheld.Contains(informationheld[i]))
						{
							float num = Random.Range (0, 100);
							if (importance > num) 
							{
								//For some reason the easier version doesn't work.
								int inforr = informationheld[i];
								other.GetComponent<InformationSharing>().AddInfo(inforr);
								Debug.Log ("Shared Info");
							}
						}
					}
				}
            }
        }
    }

	/// Adds the info to the entity, pass in the ID
	void AddInfo(int infoid)
	{
		informationheld.Add(infoid);
		//GameObject.Find("Quant").GetComponent<Quantity>().numberofinfo++;
		rend.material = newMat;
		GetComponent<Light> ().intensity= GetComponent<Light>().intensity + 0.5f;
		Debug.Log ("Information has been got");
	}
}