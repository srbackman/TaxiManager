using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ClassLibrary lib;

    [SerializeField] private float TickInterval = 1.0f;

    private float _Time = 0f;
    public int TotalMoney = 0;

    private void Awake()
    {
        lib = FindObjectOfType<ClassLibrary>();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<NodeRoutes> TraficLights = new List<NodeRoutes>();

        _Time += Time.deltaTime;
        while (_Time >= TickInterval)
        {
            lib.cityManager.CityManagment();
            TraficLights = lib.cityManager.TraficLightManagment();
            TotalMoney += lib.vehicleManager.TaxiManagment(TraficLights);
            lib.vehicleManager.CivieManagment(TraficLights);
            
            _Time -= TickInterval;
            print("tick");
        }
    }
}
