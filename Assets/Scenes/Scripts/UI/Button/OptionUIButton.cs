using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIButton : MonoBehaviour
{
    public void BackGame()
    {
        OptionUI.ispuase += 2;
        SceenHPUI.selftx += 1000.0f;
        return;
    }
    public void GoHome()
    {
        BackGame();
        GameManager.Instance.NormalJumpToScene("Home");
    }
    public void ExitGame()
    {
        Application.Quit();
        return;
    }
}
