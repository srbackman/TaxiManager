using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ClassLibrary lib;

    [SerializeField] private float TickInterval = 1.0f;

    private float Time = 0f;

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
        while (Time >= TickInterval)
        {
            lib.vehicleManager.TaxiManagment();
            lib.vehicleManager.CivieManagment();
            lib.cityManager.CityManagment();
            
            Time -= TickInterval;
        }
    }
}
