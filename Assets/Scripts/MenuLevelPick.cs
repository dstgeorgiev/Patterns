using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelPick : MonoBehaviour
{
    public GameObject levelMenuPick;

    public void SendToFieldDay()
    {
        Time.timeScale = 1f;
        levelMenuPick.SetActive(false);
        SceneManager.LoadScene("level4_day - Copy");
    }

    public void SendToFieldNight()
    {
        Time.timeScale = 1f;
        levelMenuPick.SetActive(false);
        SceneManager.LoadScene("level3_night - Copy");
    }

    public void SendToTownDay()
    {
        Time.timeScale = 1f;
        levelMenuPick.SetActive(false);
        SceneManager.LoadScene("level6_CITY_Day - Copy");
    }

    public void SendToTownNight()
    {
        Time.timeScale = 1f;
        levelMenuPick.SetActive(false);
        SceneManager.LoadScene("level7_City_Night - Copy");
    }

    public void CloseMenu()
    {
        levelMenuPick.SetActive(false);
        Time.timeScale = 1f;
    }
}
