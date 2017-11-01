using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : MonoBehaviour {
    public Text text;
    public Button btn;


    public void SetText(string _text)
    {
        text.text = _text;
    }
}
