//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public NameComponent name { get { return (NameComponent)GetComponent(GameComponentsLookup.Name); } }
    public bool hasName { get { return HasComponent(GameComponentsLookup.Name); } }

    public void AddName(string newValue, bool newIsUnique) {
        var index = GameComponentsLookup.Name;
        var component = (NameComponent)CreateComponent(index, typeof(NameComponent));
        component.value = newValue;
        component.isUnique = newIsUnique;
        AddComponent(index, component);
    }

    public void ReplaceName(string newValue, bool newIsUnique) {
        var index = GameComponentsLookup.Name;
        var component = (NameComponent)CreateComponent(index, typeof(NameComponent));
        component.value = newValue;
        component.isUnique = newIsUnique;
        ReplaceComponent(index, component);
    }

    public void RemoveName() {
        RemoveComponent(GameComponentsLookup.Name);
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

    static Entitas.IMatcher<GameEntity> _matcherName;

    public static Entitas.IMatcher<GameEntity> Name {
        get {
            if (_matcherName == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Name);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherName = matcher;
            }

            return _matcherName;
        }
    }
}
