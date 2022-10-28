using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float MoveVertical
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }
    public float MoveHorizontal
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float MouseX
    {
        get
        {
            return Input.GetAxis("MouseX");
        }
    }
    public float MouseY
    {
        get
        {
            return Input.GetAxis("MouseY");
        }
    }
}
