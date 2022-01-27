using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TypeWriter : Text
{

    [Range(1f, 150f)] public float typesPerSecond = 25f;
    [Range(0f, 10f)] public float startDelay = 1.0f;
    private float typeStartTime = 0f;
    public EventScript.C_Texts targetText;

    [System.Serializable] public class mEvent : UnityEvent { }

    public mEvent OnTypeCharacter;
    public mEvent OnTypeFinished;

    public string _text = "";
    private string _writtenText = "";
    public override string text
    {
        set
        {
            _text = value;
        }
        get
        {
            return _text;
        }
    }


    private int typeCounter = 0;


    void Start()
    {

        StartCoroutine(typeText());
        if (_text != "")
        {
            RestartTypewriting();
        }
    }

    /// <summary>
    /// Finish the actual typewriting.
    /// </summary>
    public void FinishTypewriting()
    {
        if (typeWritingFinished == false)
        {
            typeCounter = _text.Length;
            typeWritingFinished = true;
            actualizeTextfield(_text);
            OnTypeFinished.Invoke();
        }
    }
    /// <summary>
    /// Restart the typewriting.
    /// </summary>
    public void RestartTypewriting()
    {
        actualizeTextfield("");
        typeCounter = 0;
        typeStartTime = Time.time;
        typeWritingFinished = false;
    }


    private bool typeWritingFinished = false;
    IEnumerator typeText()
    {
        actualizeTextfield("");
        typeCounter = 0;
        typeWritingFinished = false;


        float typeDuartion = 0f;
        int typeCounterOld = 0;

        yield return new WaitForSeconds(startDelay);

        typeStartTime = Time.time;

        //forever in case the text changes in the meantime
        while (true)
        {

            typeDuartion = Mathf.Abs(Time.time - typeStartTime);
            typeCounter = (int)(typeDuartion * typesPerSecond);

            if (typeCounter != typeCounterOld && typeWritingFinished == false)
            {
                if (typeCounter > _text.Length)
                {
                    typeCounter = _text.Length;
                }
                _writtenText = _text.Substring(0, typeCounter);
                actualizeTextfield(_writtenText);

                OnTypeCharacter.Invoke();

                if (typeCounter >= _text.Length && typeWritingFinished == false)
                {
                    OnTypeFinished.Invoke();
                    typeWritingFinished = true;
                }
                else
                {
                    typeWritingFinished = false;
                }

                yield return null;

                typeCounterOld = typeCounter;
            }

            yield return null;
        }
    }

    void actualizeTextfield(string txt)
    {
        targetText.text = txt;
    }


}
