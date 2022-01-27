using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum TipUIStatus
{
    Init,
    Show,
    Cool
}

class TipUI : UIWindow
{
    private Text m_textTip;

    private TipUIStatus m_status = TipUIStatus.Init;
    private float m_statusTime = 0f;

    private List<string> m_waitTip = new List<string>();

    private float m_duration = 1f;
    private float m_showTime;

    private TipUIStatus Status
    {
        set
        {
            m_status = value;
            m_statusTime = Time.time;
        }
    }

    protected override void ScriptGenerator()
    {
        m_textTip = FindChildComponent<Text>("m_goTips/Text");
    }

    protected override void OnCreate()
    {
        m_showTime = 1f;
        Status = TipUIStatus.Init;
    }

    protected override void OnUpdate()
    {
        RefreshTip();
    }

    public void ShowNewTip(string tip, int diyTime = 0)
    {
        if (diyTime > 0)
        {
            m_duration = diyTime;
        }
        else if (m_showTime > 0)
        {
            m_duration = m_showTime;
        }
        else
        {
            m_duration = 1;
        }
        if (m_waitTip.Contains(tip))
        {
            return;
        }
        m_waitTip.Add(tip);
        RefreshTip();
    }

    private void RefreshTip()
    {
        ///判断当前是否在显示
        switch (m_status)
        {
            case TipUIStatus.Init:
                {
                    if (m_waitTip.Count > 0)
                    {
                        m_textTip.text = m_waitTip[0];
                        m_waitTip.RemoveAt(0);

                        Status = TipUIStatus.Show;
                    }
                }
                break;
            case TipUIStatus.Show:
                {
                    if (m_statusTime + m_duration < Time.time)
                    {
                        if (m_waitTip.Count <= 0)
                        {
                            Close();
                        }
                        else
                        {
                            ///这儿应该做动画，省略，继续运行
                            m_textTip.text = string.Empty;
                            Status = TipUIStatus.Cool;
                        }
                    }
                }
                break;
            case TipUIStatus.Cool:
                {
                    if (m_statusTime + 0.2 < Time.time)
                    {
                        ///这儿应该做动画，省略，继续运行
                        Status = TipUIStatus.Init;
                        if (m_waitTip.Count <= 0)
                        {
                            Close();
                        }
                    }
                }
                break;
        }
    }
}
