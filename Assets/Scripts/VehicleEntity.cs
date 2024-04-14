using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VehicleType
{
    civ,
    taxi
}

public enum VehicleStateType
{
    idling,
    roaming,
    patrolling,
    summoned,
    transporting
}

public enum DriverPositivieType
{
    normal,         //-
    fast,           //drives 2x faster but might get fines for speeding
    happy,          //earns more money 1.2x
    petPerson,      //doesnt mind pets
    chatty,         //earns more money 1.6x but is slower driver 0.8x
    charismatic,    //earns more money 2x
    expert          //earns more money 1.5x and drives 1.6x faster
}

public enum DriverNegativeType
{
    normal,         //-
    slow,           //drives 0.5x slower
    sad,            //drives 0.8x slower and earns 0.8x less money
    irritated,      //drives 1.8x faster and earns 0.4x less money
    notPetPerson,   //doesnt like pets, earns 0.7x less money
    forgetfull,     //might drive to wrong place and earns 0.1x less money otherwise earns 1x bonus money
    loud,           //earns 2.3x more bonus money if transporting elderly people otherwise earns 0.3x less money
    sketchy         //earns 4x more money but might dissapear with the taxi
}

public class DriverDetailsPack
{
    public DriverPositivieType positive;
    public DriverNegativeType negative;
}

public class VehicleEntity : MonoBehaviour
{
    public VehicleType Type = VehicleType.civ;
    public Task AssingedTask = null;

    public DriverPositivieType DriverPositivie = DriverPositivieType.normal;
    public DriverNegativeType DriverNegative = DriverNegativeType.normal;

    public NodeRoutes PreviousNode = null;
    public VehicleStateType VehicleState = VehicleStateType.idling;
    public float CurrentTickCurrency = 0f;
    public int TransportationTotalCost = 0;
    public bool vehicleLost = false;
    public List<Transform> CurrentRoute = new List<Transform>();

    public VehicleEntity(VehicleType type, DriverPositivieType driverPositivie, DriverNegativeType driverNegative)
    {
        this.Type = type;
        this.DriverPositivie = driverPositivie;
        this.DriverNegative = driverNegative;
    }

    public void Set(VehicleType type, DriverPositivieType driverPositivie, DriverNegativeType driverNegative)
    {
        this.Type = type;
        this.DriverPositivie = driverPositivie;
        this.DriverNegative = driverNegative;
    }
}
