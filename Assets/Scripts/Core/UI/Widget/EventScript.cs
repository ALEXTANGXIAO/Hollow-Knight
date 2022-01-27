using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



public class EventScript : MonoBehaviour
{
    [System.Serializable] public class mEvent : UnityEvent { }

    [System.Serializable]
    public class eventText
    {
        public string textContent;
#if ES_USE_TMPRO
		public TMPro.TextMeshProUGUI TMProField;
#endif
        public Text textField;
    }

    //Position is not so nice, but for compatibility reason: #ES_USE_TMPRO - Define only on one place. 
    [System.Serializable]
    public class C_Texts
    {
#if ES_USE_TMPRO
		public TMPro.TextMeshProUGUI TMProField;
#endif
        public Text textField;

        public string text
        {
            set
            {
#if ES_USE_TMPRO
                if (TMProField != null) {
                    TMProField.text = value;
                }
#endif
                if (textField != null)
                {
                    textField.text = value;
                }
            }
        }
    }

    [System.Serializable]
    public class eventTexts
    {
        public eventText titleText;
        public eventText questionText;
        public eventText answerLeft;
        public eventText answerRight;
        public eventText answerUp;
        public eventText answerDown;
        public List<eventText> additionalTexts = new List<eventText>();

        public string[] getCsvHeader()
        {
            string[] ret;
            ret = new string[6];

            ret[0] = "EventScript.titleText";
            ret[1] = "EventScript.questionText";
            ret[2] = "EventScript.answerLeft";
            ret[3] = "EventScript.answerRight";
            ret[4] = "EventScript.answerUp";
            ret[5] = "EventScript.answerDown";
            //additional texts not possible at the moment
            return ret;
        }

        public string[] getCsvData()
        {
            string[] ret;
            ret = new string[6];

            ret[0] = titleText.textContent;
            ret[1] = questionText.textContent;
            ret[2] = answerLeft.textContent;
            ret[3] = answerRight.textContent;
            ret[4] = answerUp.textContent;
            ret[5] = answerDown.textContent;
            //additional texts not possible at the moment
            return ret;
        }
        //Yes, I know. Could also be done with serialized objects in editor scripts.
        public bool setData(string variable, string data)
        {
            switch (variable)
            {
                case "titleText":
                    titleText.textContent = data;
                    break;
                case "questionText":
                    questionText.textContent = data;
                    break;
                case "answerLeft":
                    answerLeft.textContent = data;
                    break;
                case "answerRight":
                    answerRight.textContent = data;
                    break;
                case "answerUp":
                    answerUp.textContent = data;
                    break;
                case "answerDown":
                    answerDown.textContent = data;
                    break;
                default:
                    Debug.LogWarning("The variable '" + variable + "' is unknown and could not be written.");
                    return false;
            }
            return true;
        }
    }
}

