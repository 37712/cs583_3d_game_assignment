using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLevel;

    public int damage;
    public int score = 0; 
    public int maxHP;
    public int currentHP;
    public int KillsToWin = 5;

    public AudioClip injuredSound;

    private void Update()
    {
        // loads win scene when score is met
        if (score >= KillsToWin)SceneManager.LoadScene("Winner Scene");

        // loads louser scene if health reaches 0
        if (currentHP <= 0)SceneManager.LoadScene("Loser Scene");
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(injuredSound);

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int amount)
    {
        //Example of special command
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void AddPoints(int num)
    {
        score += num;
    }

}