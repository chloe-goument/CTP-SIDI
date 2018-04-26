using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Information
{
	// Unique identifier
	public uint id;

    // Is this the master copy?
    public bool master;

	// Time of Creation
	public float time;

	// Importance to the world
	public float worldimportance;

    // What is this information
    public List<int> content;

    // How many know this?
    public int known;

    // Adds values into the information chunk
        public void Generate(uint _id, bool isMaster, float _time, float _importance, List<int> _content)
    {
            id = _id;
            master = isMaster;
            time = _time;
            worldimportance = _importance;
            content = _content;
    }
}
