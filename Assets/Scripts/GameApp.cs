using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

sealed partial class GameApp : UnitySingleton<GameApp>
{
    public int TargetFrameRate = 300;

    public bool UseBuggly;

    public override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Init()
    {
#if !UNITY_WEBGL
        if (UseBuggly)
        {
            BugglyMgr.Instance.OnInit();
        }
#endif

        SetTargetFrameRate();
        InitLibImp();
        RegistAllSystem();

        MonoManager.Instance.StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        UISys.Mgr.ShowWindow<HurtUI>();

#if UNITY_ANDROID || UNITY_WEBGL
        UISys.Mgr.ShowWindow<JoyStickUI>(UI_Layer.Top);
#endif

        yield return new WaitForSeconds(0.3f);
    }
}