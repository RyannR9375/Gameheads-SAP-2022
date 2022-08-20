using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private SpriteRenderer Sprite;

    [Header("Color Flash on Damage")]
    public float flashDuration = 2f;
    public int numberOfFlashes = 3;
    public Color flashColor;
    private Color regularColor;

    public float health;

    public void Awake()
    {
        flashColor = Color.red;
        regularColor = Color.white;
        Sprite = GetComponent<SpriteRenderer>();
    }
    public void Damage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashCo());
        //Particles
        //Enemies flash red

    }

    public void Update()
    {
        CheckStatus();
    }

    public void CheckStatus()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator FlashCo()
    {
        int temp = 0;
        while (temp < numberOfFlashes)
        {
            Sprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            Sprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
    }
}
