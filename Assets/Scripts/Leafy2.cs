using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leafy2 : Player2
{
    private bool _facingLeft2 = true;
    private int _maxHealth = 100;
    private int _currentHealth;
    [SerializeField] private float _secondBetweenShot;
    [SerializeField] private float _secondBetweenSlash;
    [SerializeField] private WindStrikeProjectile2 _windstrike;
    [SerializeField] private Transform _summonZone;
    private float _nextShotTime;
    private float _nextSlashTime;
    [SerializeField] private AudioSource _slashSound;
    [SerializeField] private AudioSource _takeDamageSound;
    [SerializeField] private AudioSource _windStrikeSound;
    [SerializeField] private AudioSource _walkSound;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _deadSound;
    protected override void Start()
    {
        Physics2D.IgnoreLayerCollision(9,9);
        GameObject healthBar = GameObject.FindGameObjectWithTag("HealthBar2");
        healthBar.GetComponent<Slider>().maxValue = _maxHealth;
        _currentHealth = _maxHealth;
        healthBar.GetComponent<Slider>().value = _currentHealth;
    }
 
    protected override void FixedUpdate()
    {    
        CharCollisionInfo.OnReset();          
        OnRaycastInitialization();
        OnFlip();
        base.FixedUpdate();
        OnCharacterMove(); 
        OnCharacterAnimation();
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

    private void OnCharacterMove()
    {     
        //stand state
        direction.x = 0; 
        if(CharCollisionInfo.Below)
        {
            direction.y = 0;
        }         
   
        if (Player2Input.x == 1 || Player2Input.x == -1)   //walk state
        {
            _walkSound.Play();
            if(Player2Input.x == 1)
            {
                direction.x = 1;
            }
            if(Player2Input.x == -1)
            {
                direction.x = -1;
            }
        }
        if(Player2Input.y == 1 && CharCollisionInfo.Below) // jump state
        {
            _jumpSound.Play();
            direction.y = 1;                           
        }
        OnVelocityX(direction);
        OnVelocityY(direction);
        transform.Translate(CharVelocity * Time.fixedDeltaTime);
    }

    private void OnCharacterAnimation()
    {
        if(direction.x == 0 && direction.y == 0 && CharCollisionInfo.Below)
        {
            Anim.SetInteger("animationState",(int)CharStatus.Idle);  
        }
        if(direction.x == 1 && direction.y == 0 && CharCollisionInfo.Below)
        {
             //_walkSound.Play();
            Anim.SetInteger("animationState",(int)CharStatus.WalkForward);  
        }
        if(direction.x == -1 && direction.y == 0 && CharCollisionInfo.Below)
        {
             //_walkSound.Play();
            Anim.SetInteger("animationState",(int)CharStatus.WarkBackward);  
        }
        if(direction.y == 1)
        {
            //_jumpSound.Play();
            Anim.SetInteger("animationState",(int)CharStatus.Jump);  
        }
        if(direction.x == 1 && direction.y == 1)
        {
            if(!_facingLeft2)
            {
                //_jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpForward);
            }
            if(_facingLeft2)
            {
                //_jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpBackward);
            }   
        }
        if(direction.x == -1 && direction.y == 1)
        {
            if(!_facingLeft2)
            {
                //_jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpBackward);
            }
            if(_facingLeft2)
            {
                //_jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpForward);
            }     
        }
        if(Input.GetKeyDown(KeyCode.Keypad4) && CharCollisionInfo.Below)
        {
            OnSlash();
           // Anim.Play("Leafy-MeleeAttack");   
        }
        if(Input.GetKeyDown(KeyCode.Keypad5) && CharCollisionInfo.Below)
        {
           OnSummonWindStrike();
           // Anim.Play("Leafy-MeleeAttack");   
        }
    }

    private void OnVelocityX(Vector2 direction)
    {
        CharVelocity.x = direction.x * CharMoveSpeed;
    }

    private void OnVelocityY(Vector2 direction)
    {
        CharVelocity.y = direction.y * CharJumpHeight;
    }

    private void OnFlip()
    {
        //Debug.Log(transform.position);
        GameObject leafy1 = GameObject.FindGameObjectWithTag("Leafy1");
        Vector3 leafy1Scale = transform.localScale;
        //leafy1Scale.x = ((leafy2.transform.position.x - transform.position.x) < 0 && !_toTheLeftLeafy2) ? (leafy1Scale.x * -1) : (leafy1Scale.x * 1);
        if ((leafy1.transform.position.x - transform.position.x) > 0 && _facingLeft2)
        {
            leafy1Scale.x *= -1;
            transform.localScale = leafy1Scale;
            _facingLeft2 = !_facingLeft2;
        }
        if ((leafy1.transform.position.x - transform.position.x) < 0 && !_facingLeft2)
        {
            leafy1Scale.x *= -1;
            transform.localScale = leafy1Scale;
            _facingLeft2 = !_facingLeft2;
        }   
    }

    private void OnSlash()
    {
        if(Time.time > _nextSlashTime)
        {
            _nextSlashTime = Time.time + _secondBetweenSlash;
            _slashSound.Play();
            Anim.SetTrigger("meleeAttack");
        }    
    }

    private void OnSummonWindStrike()
    {
        if(Time.time > _nextShotTime)
        {
            _nextShotTime = Time.time + _secondBetweenShot;
            WindStrikeProjectile2 newWindStrike = (WindStrikeProjectile2) Instantiate(_windstrike, _summonZone.position, _summonZone.rotation);
            _windStrikeSound.Play();
            Anim.SetTrigger("rangeAttack");
        }     
    }

    public void OnTakeDamage(int slashDamage)
    {
        _currentHealth -= slashDamage;
        GameObject healthBar = GameObject.FindGameObjectWithTag("HealthBar2");
        healthBar.GetComponent<Slider>().value = _currentHealth;
        _takeDamageSound.Play();
        Anim.SetTrigger("takeDamage");

        if(_currentHealth <= 0)
        {
            OnDie();
            FindObjectOfType<GameManager>().OnDelayBeforeGameOver();   
        }
    }

    private void OnDie()
    {     
        _deadSound.Play();
        GetComponent<Rigidbody2D>().isKinematic = true; 
        GetComponent<BoxCollider2D>().enabled = false;       
        Anim.Play("Leafy-Dead");  
        enabled = false;   
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Leafy1")
        {
            coll.GetComponent<Leafy1>().OnTakeDamage(10);
        }
    }
}
