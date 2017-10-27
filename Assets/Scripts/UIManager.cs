using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [HideInInspector]
    public static UIManager instance;

    public GameObject button;
    public GameObject buttonHolder;

    public Text locationName;

    private void Awake()
    {
        instance = this;
    }

    public void SetLocationName(string name)
    {
        locationName.text = name;
    }

    public void AddLocationButton(string name, int id)
    {
        RemoveAllButtons();

        GameObject go = Instantiate(button, buttonHolder.transform);
        ButtonHelper btnHelp = go.GetComponent<ButtonHelper>();
        btnHelp.SetText(name);
        btnHelp.btn.onClick.AddListener(() => GameLogic.instance.MoveToLocation(id));
    }

    private void RemoveAllButtons()
    {
        if (buttonHolder.transform.childCount <= 0) return;

        for (int i = 0; i < buttonHolder.transform.childCount; i++)
        {
            Destroy(buttonHolder.transform.GetChild(i).gameObject);
        }
    }
}
