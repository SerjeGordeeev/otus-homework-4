using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Create Characters")]
public class Characters : ScriptableObject
{
    [SerializeField]
    public List<Character> characters = new List<Character>();
}

