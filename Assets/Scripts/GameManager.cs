using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int coins = 0;
    int lives = 3;
    public static GameManager instance;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text livesText;

    public void AddGold()
    {
        coins++;
        coinsText.text = coins.ToString();
    }

    public void RemoveLive()
    {
        lives--;
        livesText.text = lives.ToString();
    }

    void Start()
    {
        livesText.text = lives.ToString();

        instance = GetComponent<GameManager>();   
    }


    void Update()
    {
        
    }
}
