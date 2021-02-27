using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Globals", menuName = "Create Globals")]
public class Globals : ScriptableObject
{

    public Toolbar toolbarPrefab;
    public MessagePanel messagePanelPrefab;

    [SerializeField]
    public List<Player> players = new List<Player>();
}
