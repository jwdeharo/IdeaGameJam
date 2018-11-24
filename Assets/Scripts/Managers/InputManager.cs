using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    //Returns if RT/LT or LEFT/RIGHt mouse have been pressed.
    public static bool FirstMechanicPressed()
    {
        return Input.GetButtonDown("FirstMechanic");
    }

    public static bool SecondMechanicPressed()
    {
		
        return Input.GetButtonDown("SecondMechanic");
    }

    public static Vector3 GetJoystickMovement()
    {
        return new Vector3(GetMainHorizontal(), GetMainVertical(), 0.0f);
    }

    public static float GetMainHorizontal()
    {
        float MainHorizontal = 0.0f;

        MainHorizontal += Input.GetAxis("J_Horizontal");
        MainHorizontal += Input.GetAxis("K_Horizontal");

        return Mathf.Clamp(MainHorizontal, -1.0f, 1.0f);
    }

    public static float GetMainVertical()
    {
        float MainVertical = 0.0f;

        MainVertical += (Input.GetAxis("J_Vertical") * -1.0f);
        MainVertical += Input.GetAxis("K_Vertical");
        
        return Mathf.Clamp(MainVertical, -1.0f, 1.0f);
    }

    public static bool ChangeMechanic(ref float aTriggerSensibility)
    {
        float ReturnValue = 0.0f;

        ReturnValue += Input.GetAxisRaw("ChangeMechanic");
        aTriggerSensibility = ReturnValue;

        return ReturnValue != 0.0f || Input.GetButtonDown("ChangeMechanic");
    }

    public static bool IsButtonPressed(string aButtonName)
    {
        return Input.GetButtonDown(aButtonName);
    }
}
