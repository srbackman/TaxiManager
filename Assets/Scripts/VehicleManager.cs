using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] private List<SO_Vehicle> VehicleTypes;
    [SerializeField] private GameObject VehicleBase;
    [SerializeField] private Transform TaxiParent;
    [SerializeField] private Transform CiviParent;

    private List<VehicleEntity> Civies;
    private List<VehicleEntity> Taxies;

    [SerializeField] private List<Transform> CivieSpawnPoints;
    [SerializeField] private Transform TaxiSpawnPoint;

    public DriverDetailsPack GetDriver()
    {
        DriverDetailsPack driver = new DriverDetailsPack();
        DriverPositivieType positive;
        DriverNegativeType negative;

        switch (Random.Range(0, 7))
        {
            case 0: positive = DriverPositivieType.normal; break;
            case 1: positive = DriverPositivieType.charismatic; break;
            case 2: positive = DriverPositivieType.chatty; break;
            case 3: positive = DriverPositivieType.expert; break;
            case 4: positive = DriverPositivieType.fast; break;
            case 5: positive = DriverPositivieType.happy; break;
            default: positive = DriverPositivieType.petPerson; break;
        }

        driver.positive = positive;


        switch (Random.Range(0, 8))
        {
            case 0: negative = DriverNegativeType.normal; break;
            case 1: negative = DriverNegativeType.forgetfull; break;
            case 2: negative = DriverNegativeType.irritated; break;
            case 3: negative = DriverNegativeType.loud; break;
            case 4: negative = DriverNegativeType.notPetPerson; break;
            case 5: negative = DriverNegativeType.sad; break;
            case 6: negative = DriverNegativeType.slow; break;
            default: negative = DriverNegativeType.sketchy; break;
        }

        driver.negative = negative;

        return (driver);
    }

    public void CreateVehicle(VehicleType type, Sprite sprite, DriverPositivieType positivie, DriverNegativeType negative)
    {
        VehicleEntity vehicle = new VehicleEntity(type, sprite, positivie, negative);

        if (VehicleType.taxi == type)
        {
            vehicle.VehicleObject = Instantiate(VehicleBase, TaxiParent);
            vehicle.VehicleObject.transform.position = TaxiSpawnPoint.position;
            Taxies.Add(vehicle);
        }
        else
        {

            Civies.Add(vehicle);
        }
    }

    public void TaxiManagment()
    {
        foreach (VehicleEntity vehicle in Taxies)
        {

        }
    }

    public void CivieManagment()
    {
        foreach (VehicleEntity vehicle in Civies)
        {

        }
    }
}
