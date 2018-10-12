using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager playerManager;
    public GameObject player;

    private int playerLevel;

	// Use this for initialization
	void Start () {
        if (playerManager == null)
        {
            playerManager = this;
            DontDestroyOnLoad(playerManager.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
