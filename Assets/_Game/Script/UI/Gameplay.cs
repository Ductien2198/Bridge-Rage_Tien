using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : UICanvas
{

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<Settings>();
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
    }

    public void PlayButton()
    {
        Time.timeScale = 1;
    }
}
