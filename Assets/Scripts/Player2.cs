using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Character
{
    protected override void Start()
    {
        //OnSetupCharracters();  
    }

    protected override void FixedUpdate()
    {
        OnPlayerInput();
    }

    private void OnSetupCharracters()
    {
        GameObject leafy = GameObject.Find("Leafy");
        leafy.transform.position = transform.position;
        //Debug.Log(leafy1);
    }
    private void OnPlayerInput()
    {
        Player2Input = new Vector2(Input.GetAxisRaw("Horizontal2"),Input.GetAxisRaw("Vertical2"));
        //Debug.Log(Player2Input);
    }
}
