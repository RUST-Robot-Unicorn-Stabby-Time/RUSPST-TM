using UnityEngine;
using UnityEngine.InputSystem;

public interface IController
{
    // This will most likley change a lot during production depending on what input we require.
    // In that case, press/release actions will use events, and continuous values will be a getter.

    Vector3 MovementDirection { get; }
    
    event System.Action<float> JumpEvent;
    event System.Action<float> LightAttackEvent;
    event System.Action<float> HeavyAttackEvent;
}