using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leafy1 : Player1
{
    private bool _facingRight1 = true;
    private int _maxHealth = 100;
    private int _currentHealth;

    [SerializeField] private float _secondBetweenShot;
    [SerializeField] private float _secondBetweenSlash;
    [SerializeField] private WindStrikeProjectile1 _windstrike;
    [SerializeField] private Transform _summonZone;
    private float _nextShotTime;
    private float _nextSlashTime;
    private Slider slider;

    [SerializeField] private AudioSource _slashSound;
    [SerializeField] private AudioSource _takeDamageSound;
    [SerializeField] private AudioSource _windStrikeSound;
    [SerializeField] private AudioSource _walkSound;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _deadSound;
    protected override void Start()
    {
        //Physics2D.IgnoreLayerCollision(9,9);
        GameObject healthBar = GameObject.FindGameObjectWithTag("HealthBar1");
        healthBar.GetComponent<Slider>().maxValue = _maxHealth;
        _currentHealth = _maxHealth;
        healthBar.GetComponent<Slider>().value = _currentHealth;
        // Debug.Log("player1" + PlayerInfoTable.getPlayer1());
        // Debug.Log("player1icon" + IconTable.getCharIconArray()[0]);
    }
 
    protected override void FixedUpdate()
    {    
        CharCollisionInfo.OnReset();          
        OnRaycastInitialization();
        //OnStartCharacterState();
        OnFlip();
        base.FixedUpdate();
        OnCharacterMove(); 
        OnCharacterAnimation();
    }

    protected override void OnRaycastInitialization()
    {
        base.OnRaycastInitialization();
    }

    private void OnCharacterMove()
    {     
        //stand state
        direction.x = 0; 
        if(CharCollisionInfo.Below)
        {
            direction.y = 0;
        }         
   
        if (Player1Input.x == 1 || Player1Input.x == -1)   //walk state
        {
            _walkSound.Play();
            if(Player1Input.x == 1)
            {
                direction.x = 1;
            }
            if(Player1Input.x == -1)
            {
                direction.x = -1;
            }
        }
        if(Player1Input.y == 1 && CharCollisionInfo.Below) // jump state
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
            if(_facingRight1)
            {
                //_jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpForward);
            }
            if(!_facingRight1)
            {
               // _jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpBackward);
            }        
        }
        if(direction.x == -1 && direction.y == 1)
        {
            if(_facingRight1)
            {
                //_jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpBackward);
            }
            if(!_facingRight1)
            {
                //_jumpSound.Play();
                Anim.SetInteger("animationState",(int)CharStatus.JumpForward);
            }             
        }
        if(Input.GetKeyDown(KeyCode.J) && CharCollisionInfo.Below)
        {         
           //Anim.Play("Leafy-MeleeAttack");  
            OnSlash();
        }
        if(Input.GetKeyDown(KeyCode.K) && CharCollisionInfo.Below)
        {         
           OnSummonWindStrike();
           //Anim.Play("Leafy-MeleeAttack");   
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
        GameObject leafy2 = GameObject.FindGameObjectWithTag("Character2");
        Vector3 leafy1Scale = transform.localScale;
        //leafy1Scale.x = ((leafy2.transform.position.x - transform.position.x) < 0 && !_toTheRightLeafy2) ? (leafy1Scale.x * -1) : (leafy1Scale.x * 1);
        if ((leafy2.transform.position.x - transform.position.x) < 0 && _facingRight1)
        {
            leafy1Scale.x *= -1;
            transform.localScale = leafy1Scale;
            _facingRight1 = !_facingRight1;
        }
        if ((leafy2.transform.position.x - transform.position.x) > 0 && !_facingRight1)
        {
            leafy1Scale.x *= -1;
            transform.localScale = leafy1Scale;
            _facingRight1 = !_facingRight1;
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
            WindStrikeProjectile1 newWindStrike = (WindStrikeProjectile1) Instantiate(_windstrike, _summonZone.position, _summonZone.rotation);
            _windStrikeSound.Play();
            Anim.SetTrigger("rangeAttack");
        }     
    }
    public void OnTakeDamage(int slashDamage)
    {
        _currentHealth -= slashDamage;
        GameObject healthBar = GameObject.FindGameObjectWithTag("HealthBar1");
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
        if(coll.gameObject.tag == "Character2")
        {
            if(GameObject.Find("Leafy2") != null)
            {
                coll.GetComponent<Leafy2>().OnTakeDamage(10);
            }   
            else if(GameObject.Find("LeafyBOT") != null)
            {
                coll.GetComponent<LeafyBOT>().OnTakeDamage(10);
            }              
        }
    }
}
