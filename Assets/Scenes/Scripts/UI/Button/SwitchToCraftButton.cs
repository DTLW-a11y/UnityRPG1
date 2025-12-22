using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToCraftButton : MonoBehaviour
{
    [SerializeField] public GameObject craft, inventory;
    RectTransform ct, it;
    // Start is called before the first frame update
    void Start()
    {
        ct = craft.GetComponent<RectTransform>();
        it = inventory.GetComponent<RectTransform>();
    }
    public void Pressed()
    {
        ct.anchoredPosition = new Vector2(ct.anchoredPosition.x == 0 ? 2000 : 0, 0);
        it.anchoredPosition = new Vector2(it.anchoredPosition.x == 0 ? 2000 : 0, 0);
    }
}
