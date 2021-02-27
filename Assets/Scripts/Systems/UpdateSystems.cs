using UnityEngine;
using System.Collections;

public class UpdateSystems : Feature
{
    public UpdateSystems(Contexts contexts): base("UpdateSystems")
    {
        Add(new GameInitializeSystem(contexts))
       .Add(new PlayersInitializeSystem(contexts))
       .Add(new UIInitializeSystem(contexts))

       .Add(new PrefabReactiveSystem(contexts))
       .Add(new ToolbarReactiveSystem(contexts))
       .Add(new MessagePanelReactiveSystem(contexts))
       .Add(new ToolbarShowReactiveSystem(contexts))
       .Add(new CharacterUserTargetingInitializeSystem(contexts))
       .Add(new EnemyTargetingInitializeSystem(contexts))
       .Add(new CharacterReactiveSystem(contexts))

       .Add(new NextPlayerTurnReactiveSystem(contexts))
       .Add(new PlayerTurnIndexReactiveSystem(contexts))
       .Add(new PlayerTurnAIControlledReactiveSystem(contexts))
       .Add(new PlayerTurnUserControlledReactiveSystem(contexts))
       .Add(new PlayerTurnUserControlledRemovedReactiveSystem(contexts))

       .Add(new CharacterAttackReactiveSystem(contexts))
       .Add(new CharacterRunningToEnemyRemovedReactiveSystem(contexts))
       .Add(new CharacterRunningFromEnemyRemovedReactiveSystem(contexts))
       .Add(new CharacterStateReactiveSystem(contexts))
       .Add(new CharacterTargetReactiveSystem(contexts))
       .Add(new NextCharacterReactiveSystem(contexts))
       .Add(new NextEnemyReactiveSystem(contexts))

       .Add(new DamageReactiveSystem(contexts))
       .Add(new HealthReactiveSystem(contexts))
       .Add(new HealthHealthBarReactiveSystem(contexts))
       .Add(new CharactrDeadReactiveSystem(contexts))
       .Add(new CheckPlayerLostReactiveSystem(contexts))

       .Add(new PositionReactiveSystem(contexts))
       .Add(new RotationReactiveSystem(contexts))

       .Add(new CharacterRunningToEnemyExecuteSystem(contexts))
       .Add(new CharacterRunningFromEnemyExecuteSystem(contexts))
       .Add(new CharacterHealthBarExecuteSystem(contexts))
         ;
    }

}
