using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLeaf : MonoBehaviour
{
    [SerializeField] GameObject bookleaf;
    BookLeafUI bookleafUI;
    private void Start()
    {
        bookleafUI = bookleaf.GetComponent<BookLeafUI>();
    }
    public void LeftButton()
    {
        bookleafUI.LeftButton();
    }
    public void RightButton()
    {
        bookleafUI.RightButton();
    }
}
