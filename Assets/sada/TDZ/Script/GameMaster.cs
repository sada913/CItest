using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : SingletonMonoBehaviour<GameMaster> {
    public GameObject Player;
    [System.NonSerialized]
    public int MaxEnemy;
    

    [System.NonSerialized]
    public int EnemyCounts = 0;

    public int MaxLife = 5;
    public int Life;

    public int Score = 0;
    public int AddScoreRange = 100;

    public float NonAtackedTime = 3.0f;

    public bool CanAtack = true;

    [SerializeField] Text LifeText,ScoreText;


    // Use this for initialization
    void Start () {
        Life = MaxLife;
        LifeText.text = "Life " + Life.ToString();
        ScoreText.text = "Score " + Score.ToString();
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    IEnumerator NonAtack()
    {
        if (CanAtack)
        {
            CanAtack = false;
            yield return new WaitForSeconds(NonAtackedTime);
            CanAtack = true;
        }
        
    }

    public void NonAtackCoroutine()

    {
        StartCoroutine(NonAtack());
    }

    public void Damage()

    {
        Life--;
        LifeText.text = Life.ToString();
    }
    public void AddScore()
    {
        Score += AddScoreRange;
        ScoreText.text = Score.ToString();
    }


}