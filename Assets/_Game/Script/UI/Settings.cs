using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void CloseDirectly()
    {
        Time.timeScale = 1;
        base.CloseDirectly();
    }

    public void ContinueButton()
    {
        CloseDirectly();
    }

    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        CloseDirectly();
    }
}
