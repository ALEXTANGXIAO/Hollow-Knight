using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GuiManager : MonoBehaviour
    {
        public static GuiManager Instance;
        
        public Image hurtVignette;
        public TextMeshProUGUI bossNameText;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        
        public void FadeHurtVignette(float intensity)
        {
            FadeVignette(hurtVignette, intensity);
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

        public void ShowBossName(string bossName)
        {
            bossNameText.gameObject.SetActive(true);
            bossNameText.text = bossName;
            bossNameText.color = new Color(1, 1, 1, 0);
            DOTween.Sequence()
                .Append(bossNameText.DOFade(1.0f, 0.5f))
                .AppendInterval(2.0f)
                .Append(bossNameText.DOFade(0.0f, 0.5f));
        }
    }
}