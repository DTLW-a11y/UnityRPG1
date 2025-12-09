using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarUI : MonoBehaviour
{
    private CharacterStats mystats;
    Entity entity;
    RectTransform mytransform;
    Slider slider;
    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        mytransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        mystats = GetComponentInParent<CharacterStats>();
        entity.OnFlip += flip;
        mystats.Onmanachange += ChangeManaUI;
    }
    private void flip()
    {
        mytransform.Rotate(0, 180, 0);
    }

    private void OnDisable()
    {
        entity.OnFlip -= flip;
        mystats.Onmanachange -= ChangeManaUI;
    }
    private void ChangeManaUI()
    {
        slider.maxValue = mystats.GetMaxMana();
        slider.value = mystats.Mana;
        Debug.Log(slider.value);
    }

}
