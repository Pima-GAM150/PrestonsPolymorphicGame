using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {

    public int level;
    public int exp;
    public float rangedAttack;
    private float xInput;
    private float yInput;

    void Start()
    {
        
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        PlayerControl();
    }

    public void PlayerControl()
    {
        if(xInput != 0)
        {
            transform.position = new Vector3(transform.position.x + (speed * xInput * Time.deltaTime), transform.position.y);
        }
        if(yInput != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * yInput* Time.deltaTime));
        }
    }

    public void RangedAttack()
    {

    }
    
}
