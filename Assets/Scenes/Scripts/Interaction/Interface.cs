using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  enum InterType
{
    gamespot,
    elsespot,
    npc
}
public interface Interface 
{
    public InterType GetType();
    public void Text();
    public void ThingToDo();
}
