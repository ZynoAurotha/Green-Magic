using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leafy : Player1
{
    protected override void Start(){}
 
    protected override void FixedUpdate()
    {    
        CharCollisionInfo.OnReset();          
        OnRaycastInitialization();
        base.FixedUpdate();
        // OnCharacterMove(); 
        // OnCharacterAnimation();
    }

    protected override void OnRaycastInitialization()
    {
        base.OnRaycastInitialization();
    }

    // private void OnCharacterAction()
    // {     
    //     Vector2 direction = PlayerInput;
        
    //     if(PlayerInput.x == 1 || PlayerInput.x == -1 || PlayerInput.x == 0)   //walking action
    //     {
    //         OnWalk(direction);
    //         // if(PlayerInput.x == 0 && CharCollisionInfo.Below)
    //         // {
    //         //     Anim.SetInteger("animationState",(int)CharStatus.Idle);
    //         // }
    //         // if(PlayerInput.x == 1 && CharCollisionInfo.Below)
    //         // {
    //         //     Anim.SetInteger("animationState",(int)CharStatus.WalkForward);
    //         // }
    //         Anim.SetInteger("animationState",(int)CharStatus.WalkForward);
    //     }
    //     if (PlayerInput.y == 1 || PlayerInput.y == 0)   //jumping action
    //     {
    //         if(CharCollisionInfo.Below && PlayerInput.y == 0)   //problem is here
    //         {     
    //             direction.y = 0;
    //             OnJump(direction);
    //             Anim.SetInteger("animationState",(int)CharStatus.Idle);
    //         }        
    //         if(PlayerInput.y == 1)
    //         {    
    //             direction.y = 1;
    //             OnJump(direction);   
    //             Anim.SetInteger("animationState",(int)CharStatus.JumpForward);         
    //             if(CharCollisionInfo.Below)
    //             {
    //                 Anim.SetInteger("animationState",(int)CharStatus.Idle); 
    //             }                        
    //         }  
    //     }
    //     transform.Translate(CharVelocity * Time.fixedDeltaTime);
    // }

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

    // private void OnCharacterAnimation()
    // {
    //     if(direction.x == 0 && direction.y == 0 && CharCollisionInfo.Below)
    //     {
    //         Anim.SetInteger("animationState",(int)CharStatus.Idle);  
    //     }
    //     if(direction.x == 1 && direction.y == 0 && CharCollisionInfo.Below)
    //     {
    //         Anim.SetInteger("animationState",(int)CharStatus.WalkForward);  
    //     }
    //     if(direction.x == -1 && direction.y == 0 && CharCollisionInfo.Below)
    //     {
    //         Anim.SetInteger("animationState",(int)CharStatus.WarkBackward);  
    //     }
    //     if(direction.y == 1)
    //     {
    //         Anim.SetInteger("animationState",(int)CharStatus.Jump);  
    //     }
    //     if(direction.x == 1 && direction.y == 1)
    //     {
    //         Anim.SetInteger("animationState",(int)CharStatus.JumpForward);  
    //     }
    //     if(direction.x == -1 && direction.y == 1)
    //     {
    //         Anim.SetInteger("animationState",(int)CharStatus.JumpBackward);  
    //     }
    //     if(Input.GetKeyDown(KeyCode.J))
    //     {
    //        // Anim.Play("Leafy-MeleeAttack");  
    //        Anim.SetTrigger("meleeAttack"); 
    //     }
    // }

    private void OnVelocityX(Vector2 direction)
    {
        CharVelocity.x = direction.x * CharMoveSpeed;
    }

    private void OnVelocityY(Vector2 direction)
    {
        CharVelocity.y = direction.y * CharJumpHeight;
    }
}
