using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionText : MonoBehaviour
{

    Transform Target;
    Camera camera;
    TextMeshProUGUI text;
    RectTransform rectTransform;
    GameObject tip;
    Vector2 screenPixelOffset = new Vector2(0, 20);

    private void Start()
    {
        Target = GetComponentInParent<Transform>();
        camera = GameObject.Find("CameraPrefab").GetComponent<Camera>();
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    //private void Update()
    //{
    //    Vector3 targetworldpos = new Vector3(Target.position.x , Target.position.y , camera.nearClipPlane + .1f );
    //    Vector3 targetscreenpos = camera.WorldToScreenPoint( targetworldpos );
    //    rectTransform.position = new Vector2(targetscreenpos.x + screenPixelOffset.x, targetscreenpos.y + screenPixelOffset.y);
    //}
}
