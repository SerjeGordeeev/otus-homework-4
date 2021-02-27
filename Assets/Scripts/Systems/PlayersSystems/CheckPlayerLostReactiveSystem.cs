using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerLostReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CheckPlayerLostReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.aliveCharacters.value.Count == 0)
            {
                // Debug.Log($"Player '{entity.name.value}' lost. Player '{entity.playerEnemy.value.name.value}' is winner!");
                var messagePanel = _contexts.game.messagePanelEntity;
                messagePanel.messagePanel.value.text.text = $"Player '{entity.name.value}' lost. Player '{entity.playerEnemy.value.name.value}' is winner!";
                messagePanel.view.value.SetActive(true);

                _contexts.game.toolbarEntity.view.value.SetActive(false);
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.hasAliveCharacters;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.AliveCharacters));
    }
}