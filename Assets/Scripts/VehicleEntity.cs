using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VehicleType
{
    civ,
    taxi
}

public enum DriverPositivieType
{
    normal,         //-
    fast,           //drives 2x faster but might get fines for speeding
    happy,          //earns more bonus money 1.2x
    petPerson,      //doesnt mind pets
    chatty,         //earns more bonus money 1.6x but is slower driver 0.8x
    charismatic,    //earns more bonus money 2x
    expert          //earns more bonus money 1.5x and drives 1.6x faster
}

public enum DriverNegativeType
{
    normal,         //-
    slow,           //drives 0.5x slower
    sad,            //drives 0.8x slower and earns 0.8x less bonus money
    irritated,      //drives 1.8x faster and earns 0.4x less bonus money
    notPetPerson,   //doesnt like pets, earns 0.7x less bonus money
    forgetfull,     //might drive to wrong place and earns 0.1x less bonus money otherwise earns 1x bonus money
    loud,           //earns 2.3x more bonus money if transporting elderly people otherwise earns 0.3x less bonus money
    sketchy         //earns 4x more bonus money but might dissapear with the taxi
}

public class DriverDetailsPack
{
    public DriverPositivieType positive;
    public DriverNegativeType negative;
}

public class VehicleEntity : MonoBehaviour
{
    [SerializeField] private VehicleType Type = VehicleType.civ;
    [SerializeField] private Sprite VehicleSprite;
    [SerializeField] private DriverPositivieType DriverPositivie = DriverPositivieType.normal;
    [SerializeField] private DriverNegativeType DriverNegative = DriverNegativeType.normal;

    public float CurrentTickAmount = 0f;
    public GameObject VehicleObject;

    public VehicleEntity(VehicleType type, Sprite vehicleSprite, DriverPositivieType driverPositivie, DriverNegativeType driverNegative)
    {
        this.Type = type;
        this.VehicleSprite = vehicleSprite;
        this.DriverPositivie = driverPositivie;
        this.DriverNegative = driverNegative;
    }

    public void Set(VehicleType type, Sprite vehicleSprite, DriverPositivieType driverPositivie, DriverNegativeType driverNegative)
    {
        this.Type = type;
        this.VehicleSprite = vehicleSprite;
        this.DriverPositivie = driverPositivie;
        this.DriverNegative = driverNegative;
    }
}
