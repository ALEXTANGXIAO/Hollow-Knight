using UnityEngine;
using UnityEngine.UI;

class ERRORUI : UIWindow
{
    #region 脚本工具生成的代码
    private Text m_textError;
    private Button m_btnClose;
    protected override void ScriptGenerator()
    {
        m_textError = FindChildComponent<Text>("m_textError");
        m_btnClose = FindChildComponent<Button>("m_btnClose");
        m_btnClose.onClick.AddListener(OnClickCloseBtn);
    }
    #endregion

    public void Init(string msg)
    {
        if (m_textError == null)
        {
            return;
        }
        m_textError.text = msg;
    }

    #region 事件
    private void OnClickCloseBtn()
    {
        Close();
        EventCenter.Instance.EventTrigger("BugglyUnVisiable");
    }
    #endregion

}