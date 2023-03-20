using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafyBOT : Character
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

    [SerializeField] protected int RandMoveX;
    [SerializeField] protected int RandMoveY;
    [SerializeField] private float _startWaitTimeX;
    [SerializeField] private float _startWaitTimeY;
    [SerializeField] private float _startWaitTimeRangeAttack;
    [SerializeField] private float _startWaitTimeMeeleAttack;
    [SerializeField] private int _randNum;
    private float _waitTimeX;
    private float _waitTimeY;
    private float _waitTimeRangeAttack;
    private float _waitTimeMeeleAttack;

    [SerializeField] private LayerMask _projectileLayerMask;
    protected override void Start()
    {
        _waitTimeX =_startWaitTimeX;
        _waitTimeY =_startWaitTimeY;
        _waitTimeMeeleAttack = _startWaitTimeMeeleAttack;
        _waitTimeRangeAttack = _startWaitTimeRangeAttack;
        //Physics2D.IgnoreLayerCollision(9,9);
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
        OnRandomDirection();
        OnCharacterMove(); 
        OnCharacterAnimation();
    }

    protected override void OnRaycastInitialization()
    {
        base.OnRaycastInitialization();
    }

    private void OnRandomDirection()
    {
        if(_waitTimeX <= 0)
        {
            _randNum = Random.Range(1,4);
            if(_randNum == 1)
            {
                RandMoveX = 0;
            }else if(_randNum == 2)
            {
                RandMoveX = -1;
            }else if(_randNum == 3)
            {
                RandMoveX = 1;
            }
           _waitTimeX = _startWaitTimeX;
        }else
        {
            _waitTimeX -= Time.deltaTime;
        }  

        // if(_waitTimeY <= 0)
        // {
        //     _randNum = Random.Range(1,20);
        //     if(_randNum == 1)
        //     {
        //         RandMoveY = 0;
        //     }else if(_randNum == 2)
        //     {
        //         RandMoveY = 1;
        //     }
        //    _waitTimeY = _startWaitTimeY;
        // }else
        // {
        //    _waitTimeY -= Time.deltaTime;
        // }                  
    }

    private void OnCharacterMove()
    {     
        //stand state
        //direction.x = 0; 
        if(RandMoveX == 0)
        {
            direction.x = 0; 
        }
        if(CharCollisionInfo.Below)
        {
            direction.y = 0;
        }         
   
        if (RandMoveX == 1 || RandMoveX == -1)   //walk state
        {
            _walkSound.Play();
            if(RandMoveX == 1)
            {
                direction.x = 1;
            }
            else if(RandMoveX == -1)
            {
                direction.x = -1;
            }
        }
        if(CharCollisionInfo.Below) // jump state
        {
            if(GameObject.FindGameObjectWithTag("Projectile1") != null)
            {      
                Vector2 rayCastDirection = (_facingLeft2) ? Vector2.left : Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast(transform.position,rayCastDirection,5,_projectileLayerMask);
                Debug.DrawRay(transform.position,rayCastDirection *5,Color.red);
                if(hit)
                {
                    _jumpSound.Play();
                    direction.y = 1;
                    //Debug.Log("exe");    
                }
                
                // //Debug.DrawRay(transform.position,Vector2.left * Vector2.Distance(GameObject.Find("WindStrike1(Clone)").transform.position, transform.position),Color.red);
                // if(Vector2.Distance(GameObject.FindGameObjectWithTag("Projectile1").transform.position, transform.position) <= 5f)
                // {
                //     _jumpSound.Play();
                //     direction.y = 1;
                //     Debug.Log("exe");       
                // }       
            }                           
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

        if(_waitTimeRangeAttack <= 0)
        {
            _randNum = Random.Range(1,3);
            if(_randNum == 1)
            {         
                OnSummonWindStrike();  
            }
           _waitTimeRangeAttack = _startWaitTimeRangeAttack;
        }else
        {
            _waitTimeRangeAttack -= Time.deltaTime;
        }  


        if(_waitTimeMeeleAttack <= 0)
        {
            _randNum = Random.Range(1,3);
            if(_randNum == 1)
            {         
                OnSlash();
            }
           _waitTimeMeeleAttack = _startWaitTimeMeeleAttack;
        }else
        {
            _waitTimeMeeleAttack -= Time.deltaTime;
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
        GameObject leafy1 = GameObject.FindGameObjectWithTag("Character1");
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
        if(coll.gameObject.tag == "Character1")
        {
            coll.GetComponent<Leafy1>().OnTakeDamage(10);
        }
    }
}

