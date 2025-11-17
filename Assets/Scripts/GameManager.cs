using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;
using System.Net.Http.Headers;

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
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI panelText;
    [SerializeField] private GameObject portal;


    public void AddGold()
    {
        coins++;
        coinsText.text = coins.ToString();

        if (coins == coinsAmount)
        {
            portal.SetActive(true);        
        }
        print(coinsAmount);
        print(coins);
    }

    public void OpenPortal()
    {
        if (currentLevel + 1 < SceneManager.sceneCountInBuildSettings)
        {
            infoPanel.SetActive(true);
            Invoke(nameof(NextLevel), 3f);
        }
        else
        {          
            Invoke(nameof(LoadExit), 3f);
        }
    }

    public void ShowTextInfo(string text)
    {
        panelText.text = text;
        infoPanel.SetActive(true);
    }


    void NextLevel()
    {
        print("NextLvl");
        SceneManager.LoadScene(currentLevel + 1);
    }

    public void RemoveLive()
    {
        lives--;
        if (lives == 0)
        {
            ShowTextInfo("Ты лох, тебе надо тренироваться");
            Invoke("LoadExit", 3f);
        }
        else
        {
            livesText.text = lives.ToString();

            ShowTextInfo("Последний лох ты");
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

        portal.SetActive(false);
    }


    void Update()
    {
        
    }
}
