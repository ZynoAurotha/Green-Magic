using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Character
{
    protected override void Start(){}

    protected override void FixedUpdate()
    {
        Player2Input = new Vector2(Input.GetAxisRaw("Horizontal2"),Input.GetAxisRaw("Vertical2"));
    }
}
