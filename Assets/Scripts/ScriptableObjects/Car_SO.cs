using UnityEngine;

[System.Serializable] [CreateAssetMenu(fileName = "CarInfo", menuName = "Scriptable Objects/Car Info")]
public class Car_SO : ScriptableObject
{
    public new string name;
    public string decription;
    [Space]
    public Sprite sprite;
    public GameObject planePrefab;
    [Space]
    public CarSpecs_SO specs;
    public Ability ability;
    public Vector3 objectSpawnPosition;
    [Space]
    public float capsuleColliderRadius;
    public float capsuleColliderLength;
}
