using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("knockback info")]
    [SerializeField]protected Vector2 knockbackdir;
    [SerializeField] protected float knockbacktime;
    protected bool isknocked;
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackcheckdistance;
    [SerializeField] protected Transform groundcheck;
    [SerializeField] protected float groundcheckdistance;
    [SerializeField] protected Transform wallcheck;
    [SerializeField] protected float wallcheckdistance;


    [SerializeField] protected LayerMask whatisGround;

    

    public int facingdir { get; private set; } = 1;
    protected bool facingright = true;

    public System.Action OnFlip;

    #region Conponents
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CharacterStats stats { get; private set; }
    #endregion
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponentInChildren<EntityFX>();
        stats = GetComponent<CharacterStats>();
    }
    protected virtual void Update()
    {

    }
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundcheck.position, Vector2.down, groundcheckdistance, whatisGround);
    public virtual bool IsWallDetected()
    {
        return Physics2D.Raycast(wallcheck.position, Vector2.right * facingdir, wallcheckdistance, whatisGround);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundcheck.position, new Vector3(groundcheck.position.x, groundcheck.position.y - groundcheckdistance));

        Gizmos.DrawLine(wallcheck.position, new Vector3(wallcheck.position.x + wallcheckdistance, wallcheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackcheckdistance);
    }
    #endregion
    public virtual void Flip()
    {
        facingdir *= -1;
        facingright = !facingright;
        transform.Rotate(0, 180, 0);
        if(OnFlip != null) 
        OnFlip();
    }
    public virtual void flipController(float _x)
    {
        if (_x > 0 && !facingright)
            Flip();
        else if (_x < 0 && facingright)
            Flip();
    }
    public void ZeroVelocity()
    {
        if(isknocked) return;
        rb.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isknocked)return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        flipController(_xVelocity);
    }
    public void DamageEffect()
    {
        StartCoroutine("Knocked");
        fx.StartCoroutine("FlashFX");
        Debug.Log("im attacked");
    }
    public IEnumerator Knocked()
    {
        isknocked = true;
        rb.velocity = new Vector2(knockbackdir.x * -facingdir, knockbackdir.y);
        yield return new WaitForSeconds(knockbacktime);
        isknocked = false;
    }
}
