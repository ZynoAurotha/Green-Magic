using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Character
{   
    protected override void Start(){}

    protected override void FixedUpdate()
    {
        Player1Input = new Vector2(Input.GetAxisRaw("Horizontal1"),Input.GetAxisRaw("Vertical1"));
    }
}
