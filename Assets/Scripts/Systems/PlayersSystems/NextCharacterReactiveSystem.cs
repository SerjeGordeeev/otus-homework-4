using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class NextCharacterReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public NextCharacterReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var army = entity.army.value;
            var aliveCharacters = entity.aliveCharacters.value;
            var currentCharcaterIx = aliveCharacters
                .FindIndex((character) => character == entity.targetCharacter.value);

            currentCharcaterIx++;
            currentCharcaterIx = currentCharcaterIx == army.Count 
                ? 0 
                : currentCharcaterIx;

            entity.ReplaceTargetCharacter(aliveCharacters[currentCharcaterIx]);

            entity.isNextCharacter = false;
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.isNextCharacter;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.NextCharacter));
    }
}