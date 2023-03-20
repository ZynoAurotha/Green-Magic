using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{   
    private const float SKIN_WIDTH = .015f;
    private float _horizontalRaySpacing;
    [SerializeField] private int _horizontalRayCount;
    [SerializeField] private float _rayLength;
    private RayCastOrigins _rayCastOrigin;
    protected CollisionInfo CharCollisionInfo;
    protected BoxCollider2D CharBoxCollider2D;
    [SerializeField] private LayerMask _layer;
    protected Rigidbody2D CharRidgidbody2D;   
    [SerializeField] protected float CharMoveSpeed;
    [SerializeField] protected float CharJumpHeight;
    protected Vector2 Player1Input;
    protected Vector2 Player2Input;
    protected Vector2 direction;
    protected Vector2 CharVelocity;
    protected Animator Anim;
    protected virtual void Awake()
    {
        CharRidgidbody2D = GetComponent<Rigidbody2D>();
        CharBoxCollider2D = GetComponent<BoxCollider2D>();   
        Anim = GetComponent<Animator>();   
    }

    protected virtual void Start(){}

    protected virtual void FixedUpdate(){}

    protected virtual void OnRaycastInitialization()
    {
        OnUpdateRayCastOrigins ();
        OnCalculateRaySpacing ();
        for (int i = 0; i< _horizontalRayCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rayCastOrigin.BottomLeft + Vector2.right * _horizontalRaySpacing * i, Vector2.down, _rayLength, _layer);
            Debug.DrawRay(_rayCastOrigin.BottomLeft + Vector2.right * _horizontalRaySpacing * i, Vector2.down * _rayLength,Color.red);
            if(hit)
            {
                CharCollisionInfo.Below = true;
            }
        }
    }

    private void OnUpdateRayCastOrigins()
    {
        Bounds bounds = CharBoxCollider2D.bounds;
        bounds.Expand(-2 * SKIN_WIDTH);
        _rayCastOrigin.BottomLeft = new Vector2(bounds.min.x,bounds.min.y);
        _rayCastOrigin.BottomRight = new Vector2(bounds.max.x,bounds.min.y);
    }


    private void OnCalculateRaySpacing()
    {
        Bounds bounds = CharBoxCollider2D.bounds;
        bounds.Expand(-2 * SKIN_WIDTH);
        _horizontalRayCount = Mathf.Clamp(_horizontalRayCount,2,int.MaxValue); 
        _horizontalRaySpacing = bounds.size.x / (_horizontalRayCount - 1);
    }

    private struct RayCastOrigins
    {
        public Vector2 TopLeft,TopRight;
        public Vector2 BottomLeft,BottomRight;     
    }

    protected struct CollisionInfo
    {
        public bool Above, Below;
        public bool Left, Right;
        public void OnReset()
        {
            Above = Below = false;
            Left = Right = false;
        }
    }

    protected enum CharStatus
    {
        Idle = 0, 
        WarkBackward = 1, 
        WalkForward = 2, 
        Jump = 3,
        JumpBackward = 4,
        JumpForward = 5,
        MeleeAttack = 6,
        RangeAttack = 7,
        TakeDamage = 8,
        Dead = 9
    }
}

