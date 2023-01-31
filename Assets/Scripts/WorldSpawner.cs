using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public struct Score
{
    public string name;
    public string score;
}
public class WorldSpawner : MonoBehaviour
{
    public GameObject[] tiles;
    private List<GameObject> tilesCurrent = new List<GameObject>();
    private GameObject player;
    private LeaderboardManager leaderboardManager;
    public float zLocation = 0;
    public float tileLength = 30;
    public int tilesToRender = 5;
    public int score = 0;
    public Text scoreTextBox;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject leaderboard;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardManager = gameObject.AddComponent<LeaderboardManager>();
        Debug.Log(leaderboardManager.getLeaderboard());
        AddTile(0);
        for (int i = 1; i < tilesToRender; i++)
        {
            AddTile(Random.Range(1, tiles.Length));
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject tile in tilesCurrent.ToArray())
        {
            if (tile == null)
            {
                tilesCurrent.Remove(tile);
                AddTile(Random.Range(1, tiles.Length));
            }
        }
        if(player == null)
        {
            GameOver();
        } else
        {
            score = (int)player.transform.position.z;
            scoreTextBox.text = score + "m";
        }
        
    }

    public void AddTile(int index)
    {
        GameObject tile = Instantiate(tiles[index], transform.forward * zLocation, transform.rotation);
        tilesCurrent.Add(tile);
        zLocation += tileLength;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        //string leaderboardText = leaderboardManager.getLeaderboard();
        //dynamic stuff = JsonConvert.DeserializeObject(leaderboardText);
        //foreach (var s in stuff)
        //{
        //    Console.WriteLine(s);
        //}
    }
    
    public void togglePause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            paused = true;
        } else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            paused = false;
        }
    }
    
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
