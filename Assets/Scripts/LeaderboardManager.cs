using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEditor.ShaderGraph.Serialization;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;


public class LeaderboardManager : MonoBehaviour
{
    string leaderboardText = "Nothing yet!";
    // Start is called before the first frame update
    void Start()
    {
        getLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getLeaderboard()
    {
        StartCoroutine(getRequest("http://localhost:3000/score"));
        return leaderboardText;
    }
    
    void addScore(string name, int score)
    {
        //string msg = JsonUtility.ToJson(data);
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("name", name);
        data.Add("score", score.ToString());
        StartCoroutine(postRequest("http://localhost:3000/score", data));
    }

    IEnumerator getRequest(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();
        if (req.isNetworkError)
        {
            Debug.Log("Error sending request: " + req.error);
        } else
        {
            Debug.Log("Response: " + req.downloadHandler.text);
            leaderboardText = req.downloadHandler.text;
        }
    }
    IEnumerator postRequest(string url, Dictionary<string, string> data)
    {
        UnityWebRequest req = UnityWebRequest.Post(url, data);
        yield return req.SendWebRequest();
        if (req.isNetworkError)
        {
            Debug.Log("Error sending request: " + req.error);
        }
        else
        {
            Debug.Log("Response: " + req.downloadHandler.text);
        }
    }
}
