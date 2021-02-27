using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public ToolbarReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var entity = entities.SingleEntity();
        var toolbar = entity.view.value.GetComponent<Toolbar>();
        entity.toolbar.value = toolbar;

        toolbar.NextCharacterBtn.onClick.AddListener(() => {
            var gameEntity = _contexts.game.gameEntity;
            var player = gameEntity.playersList.value[gameEntity.playerTurnIndex.value];
            player.isNextCharacter = true;
           // Debug.Log("Next Character");
        });

        toolbar.NextEnemyBtn.onClick.AddListener(() => {
            var gameEntity = _contexts.game.gameEntity;
            var player = gameEntity.playersList.value[gameEntity.playerTurnIndex.value];
            player.isNextEnemy = true;
        });

        toolbar.AttackBtn.onClick.AddListener(() => {
            var gameEntity = _contexts.game.gameEntity;
            var player = gameEntity.playersList.value[gameEntity.playerTurnIndex.value];

            player.targetCharacter.value.ReplaceAttack(player.targetEnemy.value);
        }); 

        entity.view.value.SetActive(false);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasToolbar && entity.hasView;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Toolbar, GameMatcher.View));
    }
}