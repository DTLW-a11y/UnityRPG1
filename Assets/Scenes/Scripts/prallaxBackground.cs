using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;//控制背景移动速度与摄像机移动速度的比例

    private float xPosition;
    private float lenth;
    


    void Start()
    {
         cam = GameObject.Find("CameraPrefab");//和getcompoent的区别
        lenth = GetComponent<SpriteRenderer>().bounds.size.x;//背景长度
        xPosition = transform.position.x;//背景位置
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float  distanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if(distanceMoved > xPosition + lenth)
            xPosition = xPosition + lenth;
        else if(distanceMoved < xPosition - lenth)
            xPosition = xPosition - lenth;
    }
}
