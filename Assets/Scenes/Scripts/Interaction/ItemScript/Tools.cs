using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour, Interface
{
    
    public void Text()
    {
        throw new System.NotImplementedException();
    }

    public void ThingToDo()
    {
        if(TaskManager.instance.Find(6) == 1)
        {
            Inventory.Instance.AddItem(101);
        }
    }

    InterType Interface.GetType()
    {
        return InterType.elsespot;
    }
}
