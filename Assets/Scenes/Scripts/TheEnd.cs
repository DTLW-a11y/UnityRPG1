using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    [SerializeField] GameObject flower;
    Transform selftrans, flowertrans;
    // Start is called before the first frame update
    void Start()
    {
        selftrans = GetComponent<Transform>();
        flowertrans = flower.GetComponent<Transform>();
    }
    public float time = 0.0f, targetx = 0, targety = 0, center = 0.0f;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        selftrans.position = new Vector2(targetx, targety - 5.20f * (2.6f - time + 0.16f) * (2.6f - time + 0.16f));
        if (time >= 2.6f)
        {
            time -= 2.6f;
            flowertrans.position = new Vector2(targetx, targety);
            targetx = (Random.value - 0.5f) * 5.2f + center;
            targety = (Random.value - 0.5f) * 3.2f;
        }
    }
}
