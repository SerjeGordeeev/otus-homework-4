using Entitas;
using System.Collections.Generic;

public class MessagePanelReactiveSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public MessagePanelReactiveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var view = entity.view.value;
            entity.messagePanel.value = view.GetComponent<MessagePanel>();
            view.SetActive(false);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasMessagePanel;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.View, GameMatcher.MessagePanel));
    }
}