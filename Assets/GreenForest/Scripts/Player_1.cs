//Original scripting by Aidan Lawrence
using UnityEngine;
using System.Collections;

public class Player_1 : MonoBehaviour 
{
    public float runningSpeedMod = 2.0f;
    CharacterMotor cm;
    float cmSpeedDefault;
	// Use this for initialization
	void Start () 
    {
        cm = GetComponent<CharacterMotor>();
        cmSpeedDefault = cm.movement.maxForwardSpeed;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Control();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}

    void Control()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            cm.movement.maxForwardSpeed = cmSpeedDefault * runningSpeedMod;
        else
            cm.movement.maxForwardSpeed = cmSpeedDefault;

    }

}
