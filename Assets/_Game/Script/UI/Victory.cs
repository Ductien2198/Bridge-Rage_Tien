using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : UICanvas
{
   public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        CloseDirectly();
    }

    public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        CloseDirectly();
    }
}
