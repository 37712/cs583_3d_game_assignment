using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public int killCount; 
    public int maxHP;
    public int currentHP;
    public int view_range; // how far can unit see
    public int damage;
    public int attack; // attack points
    public int attack_range; // how far can unit attack
    public int speed;

    public AudioClip injuredSound;

    private void Update()
    {
        // loads louser scene if health reaches 0
        if (currentHP <= 0)SceneManager.LoadScene("Loser Scene");
    }

    public void TakeDamage(int dmg)
    {
        // play sound
        // AudioSource audio = GetComponent<AudioSource>();
        // audio.PlayOneShot(injuredSound);

        currentHP -= dmg;

        // if unit is dead?
        if (currentHP <= 0) currentHP = 0;
    }

    // restores max health
    public void Heal()
    {
        //Example of special command
        currentHP = maxHP;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void AddPoints()
    {
        killCount++;
    }

}