using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int coinsAmount;
    int coins = 0;
    int lives = 3;
    public static GameManager instance;

    List<GameObject> listCoins = new List<GameObject>();
    internal int currentLevel;

    [SerializeField] private Text coinsText;
    [SerializeField] private Text livesText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private TextMeshProUGUI nextText;

    public void AddGold()
    {
        coins++;
        coinsText.text = coins.ToString();
    }

    public void RemoveLive()
    {
        lives--;
        if (lives == 0)
        {
            infoText.text = "Ты лох, тебе надо тренироваться";
            infoText.gameObject.SetActive(true);
            Invoke("LoadExit", 3f);
        }
        else
        {
            livesText.text = lives.ToString();

            infoText.text = "Лох";
            infoText.gameObject.SetActive(true);
        }
            
    }

    public void HideInfoText()
    {
        infoText.gameObject.SetActive(false);
    }

    public void LoadExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            Application.Quit();
        #endif
    }

    void Start()
    {
        livesText.text = lives.ToString();

        instance = GetComponent<GameManager>();

        currentLevel = SceneManager.GetActiveScene().buildIndex;

        listCoins.AddRange(GameObject.FindGameObjectsWithTag("Coin"));

        coinsAmount = listCoins.Count;
        listCoins.Clear();
    }


    void Update()
    {
        
    }
}
