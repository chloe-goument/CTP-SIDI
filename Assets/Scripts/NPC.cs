using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public enum PersonType { Guard, News, Citizen };
    public bool expert;
    public PersonType Person;
    public int NPCID;
    public float competence;
    public int correctPieces = 0;
    public float correctness = 0;
    public List<Information> LocalInformation;
    public List<int> groupings = new List<int>();

	public void AddInfo(uint _id, float _time, float _importance, List<int> _content, float senderCompetence, bool _expert)
    {
        bool isnew = true;
        correctPieces = 0;
        correctness = 0;
        foreach (Information item in LocalInformation)
        {
            if(item.id == _id)
            {
                isnew = false;
            }
        }
        if (isnew == true && (expert == false || _expert == true))
        {
            List<int> myversion = new List<int>();
            for (int i = 0; i < _content.Count; i++)
            {
                myversion.Add(_content[i]);
            }
            for (int i = 0; i < _content.Count; i++)
            {
                if (Random.Range(0.0f, 1.0f) > senderCompetence)
                {
                    myversion[i] = degradeinfo(myversion[i]);
                }
            }

            Information info = new Information();
            info.Generate(_id, false, _time, _importance, myversion);
            LocalInformation.Add(info);
            foreach (Information item in GameObject.Find("GameManager").GetComponent<Manager>().informationlist)
            {
                if (item.id == _id)
                {
                    item.known++;
                }
            }

            
        }
        // Next step is expert checking
        // Determine if other agent is expert
        // If they are an expert, change version of events
        // to match theirs closely
        if (_expert == true && isnew == false && expert == false)
        {
            foreach (Information item in LocalInformation)
            {
                bool correct = true;
                if (item.id == _id)
                {
                    for (int j = 0; j < item.content.Count; j++)
                    {
                        if (item.content[j] != _content[j])
                        {
                            //Debug.Log("I Guess I was mistaken!");
                            correct = false;
                        }
                    }
                    if (correct == false)
                    {
                        for (int k = 0; k < item.content.Count; k++)
                        {
                            int change = _content[k];
                            item.content[k] = change;
                        }
                        //Debug.Log("I'll change my views");
                    }
                 }
             }
        }
        foreach (Information item in GameObject.Find("GameManager").GetComponent<Manager>().informationlist)
        {
            // for each _id in localinformation
            foreach (Information ID in LocalInformation)
            {
                if (ID.id == item.id)
                {
                    for (int i = 0; i < item.content.Count; i++)
                    {
                        if (item.content[i] == ID.content[i])
                        {
                            correctPieces++;
                        }
                    }
                }
            }
        }
        float test = LocalInformation.Count;
        float secondtest = correctPieces;
        test = test * 4;
        correctness = secondtest / test;
        GetComponent<Light>().intensity = LocalInformation.Count * 2.0f / GameObject.Find("GameManager").GetComponent<Manager>().informationlist.Count * 2.0f;

        GetComponent<Light>().color = new Color(1.0f - correctness, 1.0f, correctness);
    }


    int degradeinfo(int current)
    {
        int degraded = current;
        degraded = Random.Range(1,500);
        return degraded;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            for (int i = 0; i < groupings.Count; i++)
            {
                if (other.GetComponent<NPC>().groupings.Contains(groupings[i]))
                {
                    for (int q = 0; q < LocalInformation.Count; q++)
                    {
                        uint _id = LocalInformation[q].id;
                        float _time = LocalInformation[q].time;
                        float _importance = LocalInformation[q].worldimportance;
                        List<int> _content = LocalInformation[q].content;
                        other.GetComponent<NPC>().AddInfo(_id, _time, _importance, _content, competence, expert);
                    }
                }
            }
        }
    }
}