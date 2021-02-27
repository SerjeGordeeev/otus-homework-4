using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class UIInitializeSystem : IInitializeSystem
{
    Contexts _contexts;
    public UIInitializeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var uiRoot = _contexts.game.uIRoot.value;
        var toolbarPrefab =_contexts.game.globals.value.toolbarPrefab;
        var messagePanelPrefab =_contexts.game.globals.value.messagePanelPrefab;
        var toolbarEntity = _contexts.game.CreateEntity();

        toolbarEntity.AddName("Toolbar");
        toolbarEntity.AddToolbar(null);
        toolbarEntity.AddPrefab(toolbarPrefab.gameObject, uiRoot.gameObject.transform);


        var messagePanelEntity = _contexts.game.CreateEntity();
        messagePanelEntity.AddName("MessagePanel");
        messagePanelEntity.AddMessagePanel(null);
        messagePanelEntity.AddPrefab(messagePanelPrefab.gameObject, uiRoot.gameObject.transform);
    }
}
