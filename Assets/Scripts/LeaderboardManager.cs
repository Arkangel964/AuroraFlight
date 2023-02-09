using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEditor.ShaderGraph.Serialization;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;


public class LeaderboardManager : MonoBehaviour
{
    string leaderboardText = "";

    public string getLeaderboard()
    {
        StartCoroutine(getRequest("http://localhost:3000/score"));
        return leaderboardText;
    }
    
    void addScore(string name, int score)
    {
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
            leaderboardText = "";
            string[] entries = req.downloadHandler.text.Split('}');
            foreach (string entry in entries)
            {
                string fixedEntry = entry;
                try {
                    if (entry.StartsWith(","))
                    {
                        fixedEntry = entry.Substring(1);
                    }
                    Debug.Log("Current Entry: " + fixedEntry);
                    if (fixedEntry.Length > 0)
                    {
                        string[] fields = fixedEntry.Split(',');
                        string name = fields[1].Split(':')[1];
                        string score = fields[2].Split(':')[1];
                        leaderboardText += name + ": " + score + "\n";
                    }
                } catch (Exception e){
                    Debug.Log("Error parsing: " + entry);
                }
            }
            Debug.Log("Leaderboard: " + leaderboardText);
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
