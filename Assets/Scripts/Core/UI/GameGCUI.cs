using UnityEngine;
using UnityEngine.UI;

class GameGCUI : UIWindow
{
    public float _duration = 5f;
    private bool m_complete;
    #region 脚本工具生成的代码
    private Text m_textsp;
    private Text m_texttips;
    private GameObject m_gofiled;
    private Image m_imgJinDu;
    protected override void ScriptGenerator()
    {
        m_textsp = FindChildComponent<Text>("m_textsp");
        m_texttips = FindChildComponent<Text>("m_texttips");
        m_gofiled = FindChild("m_gofiled").gameObject;
        m_imgJinDu = FindChildComponent<Image>("m_gofiled/m_imgJinDu");
    }
    #endregion

    protected override void OnCreate()
    {
        base.OnCreate();
        MonoManager.Instance.GC();
    }

    #region 事件

    protected override void OnUpdate()
    {
        //if (m_complete)
        //{
        //    return;
        //}

        //_duration-=Time.deltaTime

        //if (m_imgJinDu.fillAmount < 1)
        //{
        //    float value = 1 / _duration * Time.deltaTime;
        //    m_imgJinDu.fillAmount += value;
        //}
        //else
        //{
        //    m_imgJinDu.fillAmount = 0;
        //}

        //curProgressValue = Mathf.Lerp(0, 1f, 4f);
        //m_imgJinDu.fillAmount = curProgressValue;
        //if (curProgressValue >= 1f)
        //{
        //    m_complete = true;
        //    Close();
        //}
    }

    #endregion

}