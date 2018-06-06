using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PopUpMenu : PopupWindowContent
{
    private bool lightBlueToggle;
    private bool darkBlueToggle;
    private bool lightGreenToggle;
    private bool darkGreenToggle;
    private bool violetToggle;
    private bool pinkToggle;
    private bool redToggle;
    private bool yellowToggle;
    private bool oragneToggle;

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Wybierz kolory:", EditorStyles.boldLabel);
        lightBlueToggle = EditorGUILayout.Toggle("Jasny Niebieski", lightBlueToggle);
        darkBlueToggle = EditorGUILayout.Toggle("Ciemny niebieski", darkBlueToggle);
        lightGreenToggle = EditorGUILayout.Toggle("Jansy zielony", lightGreenToggle);
        darkGreenToggle = EditorGUILayout.Toggle("Ciemno zielony", darkGreenToggle);
        violetToggle = EditorGUILayout.Toggle("Fioletowy", violetToggle);
        pinkToggle = EditorGUILayout.Toggle("Różowy", pinkToggle);
        redToggle = EditorGUILayout.Toggle("Czerwony", redToggle);
        yellowToggle = EditorGUILayout.Toggle("Żółty", yellowToggle);
        oragneToggle = EditorGUILayout.Toggle("Pomarańczowy", oragneToggle);

        //if (GUI.Button(new Rect(60, 200, 80, 30), "Zamknij"))
        //    OnClose();

    }

    public override void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
        SetInitialState();
    }

    public override void OnClose()
    {
        SaveToPlayerPrefs();
        Debug.Log("Popup closed: " + this);
        //editorWindow.Close(); //Double closing
    }

    private void SetInitialState()
    {
        if (PlayerPrefs.HasKey("Light_Blue"))
        {
            lightBlueToggle = PlayerPrefs.GetInt("Light_Blue") == 1;
            darkBlueToggle = PlayerPrefs.GetInt("Dark_Blue") == 1;
            lightGreenToggle = PlayerPrefs.GetInt("Light_Green") == 1;
            darkGreenToggle = PlayerPrefs.GetInt("Dark_Green") == 1;
            violetToggle = PlayerPrefs.GetInt("Violet") == 1;
            pinkToggle = PlayerPrefs.GetInt("Pink") == 1;
            redToggle = PlayerPrefs.GetInt("Red") == 1;
            yellowToggle = PlayerPrefs.GetInt("Yellow") == 1;
            oragneToggle = PlayerPrefs.GetInt("Orange") == 1;
        }
        else
        {
            lightBlueToggle = true;
            darkBlueToggle = true;
            lightGreenToggle = false;
            darkGreenToggle = false;
            violetToggle = false;
            pinkToggle = false;
            redToggle = false;
            yellowToggle = false;
            oragneToggle = false;
        }
    }

    private void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetInt("Light_Blue", lightBlueToggle ? 1 : 0);
        PlayerPrefs.SetInt("Dark_Blue", darkBlueToggle ? 1 : 0);
        PlayerPrefs.SetInt("Light_Green", lightGreenToggle ? 1 : 0);
        PlayerPrefs.SetInt("Dark_Green", darkGreenToggle ? 1 : 0);
        PlayerPrefs.SetInt("Violet", violetToggle ? 1 : 0);
        PlayerPrefs.SetInt("Pink", pinkToggle ? 1 : 0);
        PlayerPrefs.SetInt("Red", redToggle ? 1 : 0);
        PlayerPrefs.SetInt("Yellow", yellowToggle ? 1 : 0);
        PlayerPrefs.SetInt("Orange", oragneToggle ? 1 : 0);
    }
}

