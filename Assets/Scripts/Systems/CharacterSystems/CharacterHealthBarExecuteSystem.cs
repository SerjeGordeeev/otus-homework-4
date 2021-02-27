
using Entitas;
using System.Linq;
using UnityEngine;

public class CharacterHealthBarExecuteSystem : IExecuteSystem
{
    Contexts _contexts;
    private IGroup<GameEntity> _healthBarGroup;

    public CharacterHealthBarExecuteSystem(Contexts contexts)
    {
        _contexts = contexts;
        _healthBarGroup = _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.HealthBar));
    }
    public void Execute()
    {
        var mainCamera = _contexts.game.mainCamera.value.transform;
        foreach (var entity in _healthBarGroup.GetEntities())
        {
            var healthBar = entity.healthBar.value;
            healthBar.transform.LookAt(mainCamera);
        }
    }
}