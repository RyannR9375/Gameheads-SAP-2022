using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Components
    private SpriteRenderer Sprite;
    #endregion

    #region Enemy Health Stats
    [Header("Color Flash on Damage")]
    public float health;
    public float flashDuration = 2f;

    public int numberOfFlashes = 3;

    public Color flashColor;
    private Color regularColor;
    #endregion

    #region Unity Callback Functions
    public void Awake()
    {
        flashColor = Color.red;
        regularColor = Color.white;
        Sprite = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        CheckStatus();
    }
    #endregion

    #region Functions
    public void CheckStatus()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Damage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashCo());
        //Particles
        //Enemies flash red

    }
    #endregion

    #region ENUMS
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
    #endregion
}
