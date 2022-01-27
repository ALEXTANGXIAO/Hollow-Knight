using System;
using System.Collections;
using UnityEngine;

using UnityEngine;


class AudioSys : BaseLogicSys<AudioSys>
{
    private GameObject m_audioListener;
    private Transform m_audioListenerTrans;

    public override bool OnInit()
    {
        RegUnityUIClickSound(OnClickButtonSound);

        return true;
    }

    public static void RegUnityUIClickSound(Action<string> onClick)
    {
        UIButtonSound.AddPlaySoundAction(onClick);
    }

    private void OnClickButtonSound(string clickAudioType)
    {
        GameMgr.PlaySound(clickAudioType);
    }


    public static AudioMgr GameMgr
    {
        get { return AudioMgr.Instance; }
    }
}
