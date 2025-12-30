using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private Transform detector ;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;
    Collider2D Closest = null;
    private Interface currentTarget;
    private bool hastrigger = false;

    public void Update()
    {
        //Debug.Log(hastrigger);
        Detect();
        if (Closest != null && currentTarget.GetType() == InterType.gamespot && !hastrigger)//剧情点，靠近触发
        {
            Debug.Log("gamespot");
            currentTarget.ThingToDo();
            //Destroy( Closest.gameObject);
            hastrigger = true;
        }
        else if(Closest != null && Input.GetKeyDown(KeyCode.E))//正常交互物品
        {
            Debug.Log("thing");
            currentTarget.ThingToDo();
        }
        if(Closest == null)
        {
            hastrigger = false;
        }
    }
    public void Detect()
    {
        Closest = null;
        Collider2D[] COL = Physics2D.OverlapCircleAll(detector.position , radius ,layer);//保存交互层所有检测到的碰撞体

        float distance = Mathf.Infinity;

        foreach(Collider2D col in COL)//寻找最近的交互对象
        {
            Transform item = col.GetComponent<Transform>();
            float distance2 = Vector2.Distance(item.position, detector.position);
            if(distance2 <distance)
            {
                distance = distance2;
                Closest = col;
            }
        }
        if(Closest != null) 
        if (Closest.TryGetComponent(out Interface component))
        {
            currentTarget = component;
            //Debug.Log(Closest.gameObject.name);
            //检测到了物体ui显示
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(detector.position, radius);
    }
}
