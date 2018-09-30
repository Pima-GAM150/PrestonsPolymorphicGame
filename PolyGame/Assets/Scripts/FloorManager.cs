using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

    private FloorManager manager;
    
    //The type of tile that will be held in a position.
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 50;
    public int rows = 50;
    public IntRange numRooms = new IntRange(0, 0);
    public IntRange roomWidth = new IntRange(0, 0);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject player;
     

	// Use this for initialization
	void Start () {
        //Ensures that only one manager can be loaded.
		if(manager == null)
        {
            manager = this;
            DontDestroyOnLoad(manager);
        }
        else
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
