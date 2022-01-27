using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

class HurtUI : UIWindow
{
    #region 脚本工具生成的代码
    private Image m_imgHurtVignette;
    protected override void ScriptGenerator()
    {
        m_imgHurtVignette = FindChildComponent<Image>("m_imgHurtVignette");
    }
    #endregion

    protected override void OnCreate()
    {
        base.OnCreate();
        m_imgHurtVignette.gameObject.SetActive(false);
    }

    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        EventCenter.Instance.AddEventListener("Hurt", Hurt);
    }

    public void FadeHurtVignette(float intensity)
    {
        FadeVignette(m_imgHurtVignette, intensity);
    }

    private void FadeVignette(Image vignette, float intensity)
    {
        vignette.gameObject.SetActive(true);
        DOTween.Sequence()
            .Append(vignette.DOFade(intensity, 0.05f))
            .AppendInterval(1.5f)
            .Append(vignette.DOFade(0.0f, 0.5f))
            .SetEase(Ease.OutCubic);
    }

    private void Hurt()
    {
        FadeHurtVignette(0.7f);
    }

    #region 事件
    #endregion

}