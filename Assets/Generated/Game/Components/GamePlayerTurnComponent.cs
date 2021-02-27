//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PlayerTurnComponent playerTurnComponent = new PlayerTurnComponent();

    public bool isPlayerTurn {
        get { return HasComponent(GameComponentsLookup.PlayerTurn); }
        set {
            if (value != isPlayerTurn) {
                var index = GameComponentsLookup.PlayerTurn;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : playerTurnComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPlayerTurn;

    public static Entitas.IMatcher<GameEntity> PlayerTurn {
        get {
            if (_matcherPlayerTurn == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PlayerTurn);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayerTurn = matcher;
            }

            return _matcherPlayerTurn;
        }
    }
}
