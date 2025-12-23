using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float fsize = 25;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.black;
    }
    
    // Update is called once per frame
    void Update()
    {
        text.fontSize += (fsize - text.fontSize) * 6.4f * Time.deltaTime;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        fsize = 32;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fsize = 25;
    }
    public void Click()
    {
        text.color = Color.white;
    }
}
