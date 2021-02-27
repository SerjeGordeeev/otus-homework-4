
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public ControlType controlType;
    public string name;
    public List<CharacterData> army = new List<CharacterData>();
}

[Serializable]
public struct CharacterData
{
    public CharacterType type;
    public float health;
    public Weapon weapon;
    public int damage;

    public float runSpeed;
    public float distanceFromTarget;
}

public enum ControlType
{
    User,
    Computer
}

public enum CharacterType
{
    Hooligan,
    Policman,
    Woman
}