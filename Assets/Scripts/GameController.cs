using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public Globals globals;
    public Characters characters;
    public Canvas uiRoot;
     
    Contexts _contexts;
    GameContext _game;

    UpdateSystems _updateSystems;
    FixedUpdateSystems _fixedUpdateSystems;

    void Start()
    {
        _contexts = Contexts.sharedInstance;
        _game = _contexts.game;
        _game.SetGlobals(globals).AddName("Globals");
        _game.SetCharacters(characters).AddName("Characters");
        _game.SetUIRoot(uiRoot).AddName("UIRoot");

        _updateSystems = new UpdateSystems(_contexts);
        _fixedUpdateSystems = new FixedUpdateSystems();

        _updateSystems.Initialize();
        _fixedUpdateSystems.Initialize();
    }

    void Update()
    {
        _updateSystems.Execute();
        _updateSystems.Cleanup();
    }

    void FixedUpdate()
    {
        _fixedUpdateSystems.Execute();
        _fixedUpdateSystems.Cleanup();
    }

    private void OnDestroy()
    {
        _updateSystems.TearDown();
        _fixedUpdateSystems.TearDown();
    }
}
