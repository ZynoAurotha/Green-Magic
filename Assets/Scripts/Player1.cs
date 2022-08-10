using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Character
{   
    protected override void Start()
    {
        //OnSetupCharracters();  
    }

    protected override void FixedUpdate()
    {
        //CharCollisionInfo.OnReset();
        //base.FixedUpdate();
        OnPlayerInput();
        //OnCharacterMove(); 
    }

    private void OnSetupCharracters()
    {
        GameObject leafy = GameObject.Find("Leafy");
        leafy.transform.position = transform.position;
        //Debug.Log(leafy);
    }
    private void OnPlayerInput()
    {
        Player1Input = new Vector2(Input.GetAxisRaw("Horizontal1"),Input.GetAxisRaw("Vertical1"));
        //Debug.Log(Player1Input);
    }

    // private void OnCharacterMove()
    // {     
    //     //stand state
    //     direction.x = 0; 
    //     if(CharCollisionInfo.Below)
    //     {
    //         direction.y = 0;
    //     }         
   
    //     if (PlayerInput.x == 1 || PlayerInput.x == -1)   //walk state
    //     {
    //         if(PlayerInput.x == 1)
    //         {
    //             direction.x = 1;
    //         }
    //         if(PlayerInput.x == -1)
    //         {
    //             direction.x = -1;
    //         }
    //     }
    //     if(PlayerInput.y == 1 && CharCollisionInfo.Below) // jump state
    //     {
    //         direction.y = 1;                           
    //     }
    //     OnVelocityX(direction);
    //     OnVelocityY(direction);
    //     transform.Translate(CharVelocity * Time.fixedDeltaTime);
    // }
    // private void OnVelocityX(Vector2 direction)
    // {
    //     CharVelocity.x = direction.x * CharMoveSpeed;
    // }

    // private void OnVelocityY(Vector2 direction)
    // {
    //     CharVelocity.y = direction.y * CharJumpHeight;
    // }
}
