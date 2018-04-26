using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour 
{
    public List<Information> informationlist;
    public float overallcorrectness;
    public int numberOfInfo = 3;
    public bool running = false;

    void Awake ()
	{
        informationlist = new List<Information>();
        GameObject.Find("InfoNum").GetComponent<Text>().text = numberOfInfo.ToString();
    }


    public void GenerateInfo()
    {
        for (uint i = 0; i < numberOfInfo; i++)
        {
            Information info = new Information();
            info.Generate(i, true, Random.Range(0.1f, 100.0f), Random.Range(1, 10), new List<int> { Random.Range(1, 500), Random.Range(1, 500), Random.Range(1, 500), Random.Range(1, 500) });
            informationlist.Add(info);
        }
        running = true;
    }
    public void setInfo(float info)
    {
        numberOfInfo = Mathf.FloorToInt(info);
        GameObject.Find("InfoNum").GetComponent<Text>().text = numberOfInfo.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}