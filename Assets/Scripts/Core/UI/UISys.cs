using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UISystem 实现UIManager的生命周期，可注册Controller实现MVC结构，以及MVVM结构实现双向绑定
/// </summary>
class UISys:BaseLogicSys<UISys>
{
    private List<IUIController> m_listController = new List<IUIController>();

    public static UIManager Mgr
    {
        get { return UIManager.Instance; }
    }

    public override void OnUpdate()
    {
        UIManager.Instance.Update();
    }

    public override bool OnInit()
    {
        base.OnInit();

        RegistAllController();

        return true;
    }

    private void RegistAllController()
    {
        //AddController<LoadingUIController>();
    }

    private void AddController<T>() where T : IUIController, new()
    {
        for (int i = 0; i < m_listController.Count; i++)
        {
            var type = m_listController[i].GetType();

            if (type == typeof(T))
            {
                Debug.Log(string.Format("repeat controller type: {0}", typeof(T).Name));

                return;
            }
        }

        var controller = new T();

        m_listController.Add(controller);

        controller.ResigterUIEvent();
    }

    public static void ShowTipMsg(string tex,float waitTime = 0, int diyTime = 0)
    {
        EventCenter.Instance.EventTrigger<string>(TipsEvent.Log, tex);
        return;

        if (waitTime != 0)
        {
            //MonoManager.Instance.StartCoroutine(ShowTip(tex, waitTime));
            var tipUI = Mgr.ShowWindow<TipUI>(UI_Layer.System);
            if (tipUI != null)
            {
                tipUI.ShowNewTip(tex, diyTime);
            }
        }
        else
        {
            var tipUI = Mgr.ShowWindow<TipUI>(UI_Layer.System);
            if (tipUI != null)
            {
                tipUI.ShowNewTip(tex, diyTime);
            }
        }
    }

    static IEnumerator ShowTip(string tex, float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        var tipUI = Mgr.ShowWindow<TipUI>(UI_Layer.System);
        if (tipUI != null)
        {
            tipUI.ShowNewTip(tex);
        }
    }

    public static void ShowTipMsg(string msg, params object[] args)
    {
        var tipUI = Mgr.ShowWindow<TipUI>(UI_Layer.System);
        if (tipUI != null)
        {
            var Message = string.Format(msg, args);
            tipUI.ShowNewTip(Message, 0);
        }
    }
    
}

