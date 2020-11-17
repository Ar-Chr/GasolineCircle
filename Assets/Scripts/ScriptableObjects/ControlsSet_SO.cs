using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls Set", menuName = "Scriptable Objects/Controls Set")]
public class ControlsSet_SO : ScriptableObject
{
    public KeyCode forwardButton;
    public KeyCode leftButton;
    public KeyCode rightButton;
    public KeyCode backwardButton;
    [Space]
    public KeyCode abilityButton;
}
