using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{

    public static FloorManager floorManager;

    //The type of tile that will be held in a position.
    public enum TileType
    {
        Wall, Floor,
    }

    public int columns = 50;
    public int rows = 50;
    public IntRange numRooms = new IntRange(0, 0);
    public IntRange roomWidth = new IntRange(0, 0);
    public IntRange roomHeight = new IntRange(0, 0);
    public IntRange corridorLength = new IntRange(0, 0);
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    public GameObject player;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;

    // Use this for initialization
    private void Start()
    {
        //Ensures that only one manager can be loaded.
        if (floorManager == null)
        {
            floorManager = this;
            DontDestroyOnLoad(floorManager.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();

    }


    void SetupTilesArray()
    {
        tiles = new TileType[columns][];
        for (int i = 0; i < tiles.Length; i++)
        { tiles[i] = new TileType[rows]; }
    }

    void CreateRoomsAndCorridors()
    {
        //create rooms and corridors, ensuring that the number of corridors is never equal to the number of rooms.
        rooms = new Room[numRooms.Random];
        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        //setup the first room, then the first corridor.
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        //repeat setup for each room and corridor in the arrays.
        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }

            //spawns the player midway through creating rooms.
            if (i == (int) (rooms.Length * .5f))
            {
                Vector3 playerPos = new Vector3(rooms[i].xPos, rooms[i].yPos, 0);
                Instantiate(player, playerPos, Quaternion.identity);
            }
        }
    }

    void SetTilesValuesForRooms()
    {
        //loops through each room to set tiles
        for (int i = 0; i < rooms.Length; i++)
        {
            //loops through the width of each room
            Room currentRoom = rooms[i];
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;
                for (int k = 0; k < currentRoom.roomHeight; k++)
                //loops through each room's height as well.
                {
                    int yCoord = currentRoom.yPos + k;

                    // set the tile for each coordinate. 
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }

    //Loops through each corridor, and sets tiles to its floor.
    void SetTilesValuesForCorridors()
    {
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];

            //loops through each corridor's length as well.
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                // Start the coordinates at the start of the corridor.
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                // Depending on the direction, add or subtract from the appropriate
                // coordinate based on how far through the length the loop is.
                switch (currentCorridor.direction)
                {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }


    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                // ... and instantiate a floor tile for it.
                InstantiateFromArray(floorTiles, i, j);

                // If the tile type is Wall...
                if (tiles[i][j] == TileType.Wall)
                {
                    // ... instantiate a wall over the top.
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }

    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }

    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }
    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWallTiles, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = floorManager.transform;
    }

}
