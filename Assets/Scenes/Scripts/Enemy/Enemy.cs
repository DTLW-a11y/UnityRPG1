using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Stunned info")]
    public Vector2 stunneddistance;
    public float stunnedduration;
    protected bool canstunned = false;
    [SerializeField]protected GameObject stunnedimg;
    [Header("Move Info")]
    public float movespeed;
    public float idletime;
    public float battletime;
    [Header("Attack Info")]
    public float attackdistance;
    public float attckcooldown;
    public float lastattcktime;
    public EnemyStateMachine stateMachine { get; private set; }
    [SerializeField] protected LayerMask whatisplayer;

    [SerializeField] protected Transform Isattackcheck;
    [SerializeField] protected float Isattackcheckdistance;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }
    protected override void Update()
    {
        base.Update();  
        stateMachine.currentstate.Update();//���ø���
    }
    public virtual void CanStunnedWindowOpen()
    {
        canstunned = true;
        stunnedimg.SetActive(true);
    }
    public virtual void CanStunnedWindowClosed()
    {
        canstunned = false;
        stunnedimg.SetActive(false);
    }
    public virtual bool canbestunned()
    {
        if(canstunned)
        {
            CanStunnedWindowClosed();
            return true;
        }
        else 
            return false;   
    }
    public virtual RaycastHit2D isplayerdetected() =>Physics2D.Raycast(Isattackcheck.position, Vector2.right * facingdir, Isattackcheckdistance, whatisplayer);//Ϊʲô����д�ɺ�����ʽ

    public virtual void AnimationFinishTrigger() => stateMachine.currentstate.AnimationFinishTrigger();
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackdistance * facingdir, transform.position.y));
        Gizmos.DrawLine(Isattackcheck.position, new Vector3(Isattackcheck.position.x + Isattackcheckdistance, Isattackcheck.position.y));
    }
}
