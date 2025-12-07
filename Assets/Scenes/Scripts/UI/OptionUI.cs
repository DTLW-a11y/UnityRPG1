using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static int ispuase = 2;
    RectTransform selftrans;
    [SerializeField] GameObject button1;
    // Start is called before the first frame update
    void Start()
    {
        selftrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ispuase += (ispuase == 0) ? 2 : (-2);
            SceenHPUI.selftx += (ispuase - 1) * 1000.0f;
        }
        button1.GetComponent<Button>().interactable = (ispuase == 0) ? true : false;
        selftrans.anchoredPosition = new Vector2(ispuase * 1500.0f, 0.0f);
        Time.timeScale = ispuase / 2;
    }
}
