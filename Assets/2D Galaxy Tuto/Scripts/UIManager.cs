using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject _TitleScreen;

    public int _Score;
    public Text _Scoretext;

    public void Start()
    {
        _Scoretext.text = "Score : " + _Score;
    }

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives : " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        _Score += 10;
        _Scoretext.text = "Score : " + _Score;
    }

    public void ShowTitle()
    {
        _TitleScreen.SetActive(true);
    }

    public void HideTitle()
    {
        _TitleScreen.SetActive(false);
    }

    public void ResetScore()
    {
        _Score = 0;
        _Scoretext.text = "Score : " + _Score;
    }
}
