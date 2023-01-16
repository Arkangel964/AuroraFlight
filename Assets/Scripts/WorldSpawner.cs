using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public GameObject[] tiles;
    private List<GameObject> tilesCurrent = new List<GameObject>();
    public float zLocation = 0;
    public float tileLength = 30;
    public int tilesToRender = 7;
    // Start is called before the first frame update
    void Start()
    {
        AddTile(0);
        for (int i = 1; i < tilesToRender; i++)
        {
            AddTile(Random.Range(1, tiles.Length));
        }
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
    }

    public void AddTile(int index)
    {
        GameObject tile = Instantiate(tiles[index], transform.forward * zLocation, transform.rotation);
        tilesCurrent.Add(tile);
        zLocation += tileLength;
    }
}
