using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class WorldSpawner : MonoBehaviour
{
    public GameObject[] tiles;
    private List<GameObject> tilesCurrent = new List<GameObject>();
    private GameObject player;
    public float zLocation = 0;
    public float tileLength = 30;
    public int tilesToRender = 5;
    public int score = 0;
    public Text scoreTextBox;
    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("Game Over");
    }
}
