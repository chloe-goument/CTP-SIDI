using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Responsible for running each NPC's information and information
    // exchanges.

    // Some groundwork for the later addition of type which could be
    // used for having agents behave in different ways.
    public enum PersonType { Guard, News, Citizen };
    public PersonType Person;

    public bool expert;

    public int NPCID;
    public int correctPieces = 0;
    public float competence;
    public float correctness = 0;

    public List<Information> LocalInformation;
    public List<int> groupings = new List<int>();

    // For adding Information to the NPC
	public void AddInfo(uint _id, float _time, float _importance, List<int> _content, float senderCompetence, bool _expert)
    {
        bool isnew = true;
        correctPieces = 0;
        correctness = 0;

        // Check if the information that's being recieved is new.
        foreach (Information item in LocalInformation)
        {
            if(item.id == _id)
            {
                isnew = false;
            }
        }

        // If the information is true, and the npc is not an Expert, or if they
        // are an expert and the other npc is also an expert, then they should 
        // take the information on board. This is mostly to prevent laymen
        // convincing Experts of false or heavily distorted information
        if (isnew == true && (expert == false || _expert == true))
        {
            List<int> myversion = new List<int>();
            for (int i = 0; i < _content.Count; i++)
            {
                myversion.Add(_content[i]);
            }
            for (int i = 0; i < _content.Count; i++)
            {
                // If the random number is larger than the sender's competence
                // Then degrade the information point. This should lead to a
                // system where the more competent a sender, the less the 
                // data distorts.
                if (Random.Range(0.0f, 1.0f) > senderCompetence)
                {
                    myversion[i] = degradeinfo(myversion[i]);
                }
            }

            Information info = new Information();
            info.Generate(_id, false, _time, _importance, myversion);
            LocalInformation.Add(info);

            // Counting up how much information is known by the NPC.
            foreach (Information item in GameObject.Find("GameManager").GetComponent<Manager>().informationlist)
            {
                if (item.id == _id)
                {
                    item.known++;
                }
            }

            
        }
        // If the information recieved is not new, and the npc isn't an expert
        // but the npc communicating with them is, then they should look at the 
        // information provided to them by the expert and attempt to correct 
        // their own version of the information based on what the expert is
        // telling them
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

        // This helps gather information for the overall correctness level of
        // the population by storing how right each npc is about each piece
        // of information versus the information held in the central repository.
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

        // Setting the colour and intensity of the light based on how much info
        // the npc has and how right they are about it.
        GetComponent<Light>().intensity = LocalInformation.Count * 2.0f / GameObject.Find("GameManager").GetComponent<Manager>().informationlist.Count * 2.0f;

        GetComponent<Light>().color = new Color(1.0f - correctness, 1.0f, correctness);
    }

    // Short function to degrade the info by generating a random number
    int degradeinfo(int current)
    {
        int degraded = current;
        degraded = Random.Range(1,500);
        return degraded;
    }

    // When the NPC bumps into another NPC they check to see if they are a part
    // of the same grouping as the other and if they are they attempt to share
    // what they know. There might be a more efficient way to do this.
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