using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique]
public class MainCameraComponent : IComponent
{
    public Camera value;
}
