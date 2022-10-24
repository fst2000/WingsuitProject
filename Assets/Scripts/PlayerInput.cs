using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float moveVertical
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }
    public float moveHorizontal
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float mouseX
    {
        get
        {
            return Input.GetAxis("MouseX");
        }
    }
    public float mouseY
    {
        get
        {
            return Input.GetAxis("MouseY");
        }
    }
}
