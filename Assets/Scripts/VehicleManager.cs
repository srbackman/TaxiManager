using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    ClassLibrary lib;

    [SerializeField] private Camera cam;
    [SerializeField] private List<SO_Vehicle> CivVehicleTypes;
    [SerializeField] private List<SO_Vehicle> TaxiVehicleTypes;
    [SerializeField] private GameObject VehicleBase;
    [SerializeField] private Transform TaxiParent;
    [SerializeField] private Transform CiviParent;
    [SerializeField] private int CiviVehicleAmount = 12;

    [Header("PositiveTraitsSpeed")]
    [SerializeField] private float fastSpeed = 2f;
    [SerializeField] private float chattySpeed = 0.8f;
    [SerializeField] private float expertSpeed = 1.6f;

    [Header("NegativeTraitsSpeed")]
    [SerializeField] private float slowSpeed = 0.5f;
    [SerializeField] private float sadSpeed = 0.8f;
    [SerializeField] private float irritatedSpeed = 1.8f;

    [Header("PositiveTraitsMoney")]
    [SerializeField] private float happyMoney = 1.2f;
    [SerializeField] private float chattyMoney = 1.6f;
    [SerializeField] private float charismaticMoney = 2f;
    [SerializeField] private float expertMoney = 1.5f;

    [Header("NegativeTraitsMoney")]
    [SerializeField] private float sadMoney = 0.8f;
    [SerializeField] private float irritatedMoney = 0.4f;
    [SerializeField] private float sketchyMoney = 4f;

    [Header("SpecialTraitSituations")]
    [SerializeField] private int speedingFine = 30;
    [SerializeField] private float notPetPersonMoney = 0.7f;
    [SerializeField] private float forgetfullMoney = 0.1f;
    [SerializeField] private float loudMoneyPositive = 2.3f;
    [SerializeField] private float loudMoneyNegative = 0.3f;

    private List<VehicleEntity> Civies = new List<VehicleEntity>();
    private List<VehicleEntity> Taxies = new List<VehicleEntity>();

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

    private void Awake()
    {
        lib = FindObjectOfType<ClassLibrary>();
    }

    private void Start()
    {
        //Spawn civies
        for (int i = 0; i < CiviVehicleAmount; i++)
        {
            SO_Vehicle vehicle = CivVehicleTypes[Random.Range(0, CivVehicleTypes.Count)];
            print(vehicle);
            CreateVehicle(VehicleType.civ, vehicle.sprite, GetRandomPositiveType(), GetRandomNegativeType());
        }
    }

    public DriverPositivieType GetRandomPositiveType()
    {
        DriverPositivieType type = DriverPositivieType.normal;

        switch (Random.Range(0, 7))
        {
            case 0: type = DriverPositivieType.normal; break;
            case 1: type = DriverPositivieType.fast; break;
            case 2: type = DriverPositivieType.happy; break;
            case 3: type = DriverPositivieType.petPerson; break;
            case 4: type = DriverPositivieType.chatty; break;
            case 5: type = DriverPositivieType.charismatic; break;
            case 6: type = DriverPositivieType.expert; break;
        }

        return (type);
    }

    public DriverNegativeType GetRandomNegativeType()
    {
        DriverNegativeType type = DriverNegativeType.normal;

        switch (Random.Range(0, 8))
        {
            case 0: type = DriverNegativeType.normal; break;
            case 1: type = DriverNegativeType.forgetfull; break;
            case 2: type = DriverNegativeType.irritated; break;
            case 3: type = DriverNegativeType.loud; break;
            case 4: type = DriverNegativeType.notPetPerson; break;
            case 5: type = DriverNegativeType.sad; break;
            case 6: type = DriverNegativeType.sketchy; break;
            case 7: type = DriverNegativeType.slow; break;
        }

        return (type);
    }

    public void CreateVehicle(VehicleType type, Sprite sprite, DriverPositivieType positivie, DriverNegativeType negative)
    {
        if (VehicleType.taxi == type)
        {
            GameObject obj = Instantiate(VehicleBase, TaxiParent);
            VehicleEntity vehicle = obj.GetComponent<VehicleEntity>();
            vehicle.Set(type, positivie, negative);
            vehicle.transform.position = TaxiSpawnPoint.position;
            Taxies.Add(vehicle);
        }
        else //Civi
        {
            //Spawn outside of screen and at random node.
            List<NodeRoutes> nodes = new List<NodeRoutes>();
            foreach(Transform nodeTransform in lib.navigationNodes._nodeTransforms)
            {

                //Vector3 viewPos = cam.WorldToViewportPoint(nodeTransform.position);
                //if (!(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0))
                //{
                //}
                    NodeRoutes node = nodeTransform.GetComponent<NodeRoutes>();
                    nodes.Add(node);
            }

            GameObject obj = Instantiate(VehicleBase, CiviParent);
            obj.GetComponent<SpriteRenderer>().sprite = sprite;
            VehicleEntity vehicle = obj.GetComponent<VehicleEntity>();
            vehicle.Set(type, positivie, negative);
            vehicle.transform.position = nodes[Random.Range(0, nodes.Count - 1)].transform.position;
            while (vehicle.CurrentRoute.Count == 0)
            {
                vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, GetRandomDestination());
            }

            if (vehicle.CurrentRoute == null)
                print("null");


            print(obj + "   " + vehicle + " .");
            Civies.Add(vehicle);
        }
    }

    private Transform GetRandomDestination()
    {
        Transform t;
        t = lib.navigationNodes._nodeTransforms[Random.Range(0, lib.navigationNodes._nodeTransforms.Length)];
        return (t);
    }

    public int TaxiManagment(List<NodeRoutes> TraficLights)
    {
        int moneyEarnedLost = 0;

        foreach (VehicleEntity vehicle in Taxies)
        {
            AddManageTickCurrency(vehicle);
            moneyEarnedLost = VehicleMover(vehicle);
        }

        return (moneyEarnedLost);
    }

    public void CivieManagment(List<NodeRoutes> TraficLights)
    {
        foreach (VehicleEntity vehicle in Civies)
        {
            AddManageTickCurrency(vehicle);
            VehicleMover(vehicle);
            
        }
    }

    private void AddManageTickCurrency(VehicleEntity vehicle)
    {
        float addableTickCurrency = 1f;

        switch (vehicle.DriverPositivie)
        {
            case DriverPositivieType.fast: addableTickCurrency *= fastSpeed; break;
            case DriverPositivieType.chatty: addableTickCurrency *= chattySpeed; break;
            case DriverPositivieType.expert: addableTickCurrency *= expertSpeed; break;
            default: break;
        }

        switch (vehicle.DriverNegative)
        {
            case DriverNegativeType.irritated: addableTickCurrency *= irritatedSpeed; break;
            case DriverNegativeType.sad: addableTickCurrency *= sadSpeed; break;
            case DriverNegativeType.slow: addableTickCurrency *= slowSpeed; break;
            default: break;
        }

        vehicle.CurrentTickCurrency += addableTickCurrency;
    }

    private int VehicleMover(VehicleEntity vehicle)
    {
        int moneyEarnedLost = 0;

        while (vehicle.CurrentTickCurrency >= 1)
        {
            print("move");

            if (vehicle.CurrentRoute.Count == 0)
                vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, GetRandomDestination());
            print(vehicle.CurrentRoute[0]);
            NodeRoutes node = vehicle.CurrentRoute[0].GetComponent<NodeRoutes>();
            if (node && !node.HasTraficLights) //no trafic lights
            {
                //move
                if (vehicle.PreviousNode)
                    vehicle.PreviousNode.VehiclesAtNode--;
                lib.entityMover.Teleport(vehicle.transform, node.transform);
                node.VehiclesAtNode++;
                vehicle.PreviousNode = node;
                //----

                if ((vehicle.CurrentRoute.Count == 0)) //0
                {
                    //civ
                    if (vehicle.Type == VehicleType.civ)
                    {
                        vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, GetRandomDestination());

                        //Check if at destination.
                        if (lib.entityMover.CheckIfAtDestination(vehicle.CurrentRoute[0]))
                        {
                            vehicle.CurrentRoute.RemoveAt(0);
                            while (vehicle.CurrentRoute.Count == 0)
                            {
                                vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, GetRandomDestination());
                            }
                            
                        }
                    }

                    //taxi
                    else
                    {
                        //if(vehicle.AssingedTask.StartPoint == vehicle.CurrentRoute[0])
                    }
                }
                else //>=1
                    moneyEarnedLost += CheckAndAdvanceLocation(vehicle);
            }

            else if (node) //trafic lights
            {
                print("Lights");
                if (MoveThroughTraficLights(vehicle, node))
                {
                    if (vehicle.PreviousNode)
                        vehicle.PreviousNode.VehiclesAtNode--;
                    lib.entityMover.Teleport(vehicle.transform, node.transform);
                    node.VehiclesAtNode++;
                    vehicle.PreviousNode = node;

                    if ((vehicle.CurrentRoute.Count == 0)) //0
                    {
                        //civ
                        if (vehicle.Type == VehicleType.civ)
                        {
                            vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, GetRandomDestination());

                            //Check if at destination.
                            if (lib.entityMover.CheckIfAtDestination(vehicle.CurrentRoute[0]))
                            {
                                vehicle.CurrentRoute.RemoveAt(0);
                                while (vehicle.CurrentRoute.Count == 0)
                                {
                                    vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, GetRandomDestination());
                                }
                            }
                        }

                        //taxi
                        else
                        {
                            //if(vehicle.AssingedTask.StartPoint == vehicle.CurrentRoute[0])
                        }
                    }
                    else //>=1
                        moneyEarnedLost += CheckAndAdvanceLocation(vehicle);
                }
            }

            vehicle.CurrentTickCurrency -= 1;
        }

        return (moneyEarnedLost);
    }

    private bool MoveThroughTraficLights(VehicleEntity vehicle, NodeRoutes node)
    {



        return (true);
    }

    private List<NodeRoutes> GetHorizontalRoad(NodeRoutes node)
    {
        List<NodeRoutes> Horizontals = new List<NodeRoutes>();

        foreach (Transform t in node._routes)
        {
            if (t.position.x == node.transform.position.x)
                continue;

            NodeRoutes horizontalNode = t.GetComponent<NodeRoutes>();
            if (horizontalNode && horizontalNode.VehiclesAtNode > 0)
                Horizontals.Add(horizontalNode);
        }

        return (Horizontals);
    }

    private List<NodeRoutes> GetVerticalRoad(NodeRoutes node)
    {
        List<NodeRoutes> Verticals = new List<NodeRoutes>();

        foreach (Transform t in node._routes)
        {
            if (t.position.y == node.transform.position.y)
                continue;

            NodeRoutes verticalNode = t.GetComponent<NodeRoutes>();
            if (verticalNode && verticalNode.VehiclesAtNode > 0)
                Verticals.Add(verticalNode);
        }

        return (Verticals);
    }

    private int CheckAndAdvanceLocation(VehicleEntity vehicle)
    {
        int moneyEarnedLost = 0;
        print("advanced area");
        //civ
        if (vehicle.Type == VehicleType.civ)
        {
            //Check if at destination.
            if (lib.entityMover.CheckIfAtDestination(vehicle.CurrentRoute[0]))
            {
                vehicle.CurrentRoute.RemoveAt(0);
                while (vehicle.CurrentRoute.Count == 0)
                {
                    vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, GetRandomDestination());
                }
            }
        }

        //taxi
        else
        {
            //Check if at destination. //Transporting //taxi
            if ((vehicle.AssingedTask != null) && lib.entityMover.CheckIfAtDestination(vehicle.CurrentRoute[0]))
            {
                if (vehicle.AssingedTask.StartPoint == vehicle.CurrentRoute[0])
                {
                    moneyEarnedLost += (vehicle.AssingedTask.BonusTimeLimit - vehicle.AssingedTask.TimeElapsed);
                    vehicle.CurrentRoute = lib.dijkstraSearch.GetRoute(vehicle.transform, vehicle.AssingedTask.EndPoint);

                }
                else if (vehicle.AssingedTask.EndPoint == vehicle.CurrentRoute[0])
                {
                    moneyEarnedLost += vehicle.TransportationTotalCost;
                    int tempInt = CalculateSpecialDriverTypes(vehicle);
                    if (tempInt == -1)
                        vehicle.vehicleLost = true;

                    moneyEarnedLost += tempInt;
                    moneyEarnedLost += CalculateNegativePositiveMoney(vehicle);

                    vehicle.AssingedTask = null;
                    vehicle.CurrentRoute.RemoveAt(0);
                }
            }

            //Patrolling //taxi
            else if ((vehicle.AssingedTask == null) && (vehicle.CurrentRoute.Count > 0)
                && lib.entityMover.CheckIfAtDestination(vehicle.CurrentRoute[0]))
            {
                Transform temp = vehicle.CurrentRoute[0];
                vehicle.CurrentRoute.RemoveAt(0);
                vehicle.CurrentRoute.Add(temp);
            }
        }
        return (moneyEarnedLost);
    }
    private int CalculateSpecialDriverTypes(VehicleEntity vehicle)
    {
        int money = 0;

        if ((vehicle.DriverPositivie == DriverPositivieType.fast)
            && (vehicle.AssingedTask.Customer == CustomerType.police))
            money -= speedingFine;

        if ((vehicle.DriverNegative == DriverNegativeType.notPetPerson)
            && (vehicle.AssingedTask.Customer == CustomerType.petPerson))
            money -= (int)(vehicle.TransportationTotalCost * notPetPersonMoney);

        if ((vehicle.DriverNegative == DriverNegativeType.loud)
            && (vehicle.AssingedTask.Customer != CustomerType.senior))
            money -= (int)(vehicle.TransportationTotalCost * loudMoneyNegative);

        if (vehicle.DriverNegative == DriverNegativeType.sketchy && 0 == (Random.Range(0, 10)))
            money = -1;

        return (money);
    }

    private int CalculateNegativePositiveMoney(VehicleEntity vehicle)
    {
        int result = 0;

        switch (vehicle.DriverPositivie)
        {
            case DriverPositivieType.happy: result += (int)(vehicle.TransportationTotalCost * happyMoney); break;
            case DriverPositivieType.charismatic: result += (int)(vehicle.TransportationTotalCost * charismaticMoney); break;
            case DriverPositivieType.expert: result += (int)(vehicle.TransportationTotalCost * expertMoney); break;
        }

        switch (vehicle.DriverNegative)
        {
            case DriverNegativeType.sad: result += (int)(vehicle.TransportationTotalCost * sadMoney); break;
            case DriverNegativeType.irritated: result += (int)(vehicle.TransportationTotalCost * irritatedMoney); break;
        }

        return (result);
    }
}
