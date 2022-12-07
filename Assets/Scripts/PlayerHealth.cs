using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trinitrotoluene;

public class PlayerHealth : MonoBehaviour
{
    public RectTransform[] chunks;
    public float maxHP;
    [System.NonSerialized] public float HP;
    float HPPerChunk;
    private void Start()
    {
        HP = maxHP;
        HPPerChunk = maxHP / (float)chunks.Length;
    }
    private void Update()
    {

    }
    public void TakeDamage(float damage)
    {
        float oldHealth = HP;
        if (damage == 0)
        {
            return;
        }

        damage = Mathf.Clamp(damage, 1, HP);

        print(transform.name + " took " + damage + " Damage.");
        HP -= damage;
        print("Player has " + HP + " HP!");
        if (HP <= 0)
        {
            Functions.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        int amountFitIntoDamage = Math.f.FitToInt(maxHP - HP, HPPerChunk);
        int countUp = 1;
        for (int i = chunks.Length - 1; i >= 0; i--)
        {
            PlayerHealthChunk chunk = chunks[i].GetComponent<PlayerHealthChunk>();
            if (amountFitIntoDamage >= countUp)
            {
                if (!chunk.isDisabled)
                {
                    chunk.Disable();
                }
            }
            else
            {
                if (countUp == amountFitIntoDamage + 1)
                {
                    if ((maxHP - HP) % HPPerChunk >= (HPPerChunk / 2f))
                    {
                        StartCoroutine(chunk.FlashRed());
                    }
                }
            }
            countUp++;
        }


    }
}
