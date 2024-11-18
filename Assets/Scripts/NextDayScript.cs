using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Globalization;

public class NextDayScript : MonoBehaviour
{   
    private int day;

    private void Awake()
    {
        day = 1;
    }
    public void NextDay(int SceneIndex)
    {
        Debug.Log(day);
        day = day + 1;
        SceneManager.LoadScene(sceneName: "GameScene");
        Debug.Log(day);
    }
}
