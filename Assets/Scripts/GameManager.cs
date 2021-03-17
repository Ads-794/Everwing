using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Vector2 bottomLeft; 
    public static Vector2 topRight; 

    [SerializeField] Player player;
    [SerializeField] monster monster;
    [SerializeField] GameObject wave;
    [SerializeField] Text score;
    [SerializeField] Boss boss;

    int coins = 0;
    int monsterWaveCount = 3;
    int waveLeft;
    float monsterSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2 (0,0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2 (Screen.width,Screen.height));

        Player p = Instantiate(player) as Player;
        p.onGainCoin += HandleGainCoin;
        p.onPlayerDied += HandlePlayerDeath;
        Boss.onBossDied += HandleBossDeath;

        waveLeft = monsterWaveCount; 
        StartMonsterGeneration();
    }

    void HandleBossDeath()
    {
        monsterSpeed+=2;
        monsterWaveCount+=2;
        waveLeft = monsterWaveCount;
        StartMonsterGeneration();
    }

    void HandlePlayerDeath()
    {
        Debug.Log("Player dies..");
        Time.timeScale = 0; 
    }

    void HandleGainCoin()
    {
        coins++;
        score.text = coins.ToString();
    }

    void StartMonsterGeneration()
    {
        InvokeRepeating("GenerateWave", 2, 3);
    }

    void GenerateBoss()
    {
        Vector2 bossPos = new Vector2(0,topRight.y);
        Instantiate(boss, bossPos, Quaternion.identity, transform);
    }

    void GenerateWave()
    {
        if (waveLeft == 0)
        {
            CancelInvoke();
            Invoke("GenerateBoss", 5f);
            return;
        }waveLeft--;

        GameObject monsterWave = Instantiate(wave, Vector2.zero, Quaternion.identity, transform);

        for (int i = 0; i < 5; i++)
        {
            float x = (i + 0.5f)/5;
            Vector2 pos = Camera.main.ScreenToWorldPoint ( new Vector2(Screen.width * x, Screen.height));
            pos += Vector2.up * monster.transform.localScale.y;

            monster monsterGO = Instantiate(monster, pos, Quaternion.identity, monsterWave.transform) as monster;
            monsterGO.SetSpeed(monsterSpeed);
        }
    }
}
