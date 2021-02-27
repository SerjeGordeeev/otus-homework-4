using Entitas;
using System.Collections.Generic;

public class CharactrDeadReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CharactrDeadReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.ReplaceCharacterState(CharacterState.BeginDying);


            var player = entity.characterPlayer.value;
            var alivePlayerCharacters = player.aliveCharacters.value;
            alivePlayerCharacters.Remove(entity);
            player.ReplaceAliveCharacters(alivePlayerCharacters);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isDead && entity.isDead;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Character, GameMatcher.Dead));
    }
}