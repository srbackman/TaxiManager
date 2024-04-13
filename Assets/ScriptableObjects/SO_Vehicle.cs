using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Vehicle", order = 1)]
public class SO_Vehicle : ScriptableObject
{
    public VehicleType type;
    public Sprite sprite;
}
