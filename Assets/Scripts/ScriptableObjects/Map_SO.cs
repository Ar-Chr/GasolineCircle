using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapInfo", menuName = "Scriptable Objects/Map Info")]
public class Map_SO : ScriptableObject
{
    public Sprite background;
    public string description;
}
