using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIChangeButton : MonoBehaviour
{
    [SerializeField] public GameObject UI1, UI2, inv, cra;
    RectTransform UIT1, UIT2, invtr, cratr;
    // Start is called before the first frame update
    void Start()
    {
        UIT1 = UI1.GetComponent<RectTransform>();
        UIT2 = UI2.GetComponent<RectTransform>();
        invtr = inv.GetComponent<RectTransform>();
        cratr = cra.GetComponent<RectTransform>();
    }
    public void Pressed()
    {
        if (invtr.anchoredPosition != Vector2.zero)
        {
            invtr.anchoredPosition = Vector2.zero;
            cratr.anchoredPosition = new Vector2(2000, 0);
            ;
        }
        UIT1.anchoredPosition = new Vector2((UIT1.anchoredPosition.x == 0) ? 5000 : 0, 0);
        UIT2.anchoredPosition = new Vector2((UIT2.anchoredPosition.x == 0) ? 5000 : 0, 0);
    }
}
