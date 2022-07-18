using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ScriptableObject
{
    //DECLARING VARIABLES
    [SerializeField] private string _enemyName;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private float _moveSpeed;

    public string Name { get { return _enemyName; } }
    public int Damage { get { return _damage; } }
    public int maxHealth { get { return _health; } }
    public float moveSpeed { get { return _moveSpeed; } }
}
