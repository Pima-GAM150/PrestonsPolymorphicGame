using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager playerManager;
    public GameObject player;
    public new GameObject camera;
    private int playerLevel;

    // Use this for initialization
    void Start() {
        if (playerManager == null)
        {
            playerManager = this;
            DontDestroyOnLoad(playerManager.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Vector3 pTransform = new Vector3(player.transform.position.x, player.transform.position.y);
        Instantiate(camera, pTransform);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
