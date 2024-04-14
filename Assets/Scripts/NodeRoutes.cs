using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraficLightState
{
    Horizontal,
    Vertical
}

public class NodeRoutes : MonoBehaviour
{
    public List<Transform> _routes = new List<Transform>();
    public int BlockNumber = 0;
    public string RoadName = "";
    public int RoadNumber = 0;
    [HideInInspector] public bool HasTraficLights = false;
    [HideInInspector] public TraficLightState TraficGreenState = TraficLightState.Horizontal;
    [HideInInspector] public TraficLightState TraficRedState = TraficLightState.Vertical;

    [HideInInspector] public bool HorizontalTrafic = true;
    [HideInInspector] public bool VerticalTrafic = false;

    [HideInInspector] public int VehiclesAtNode = 0;

    private void Awake()
    {
        if (_routes.Count > 2)
            HasTraficLights = true;
    }

    private void OnDrawGizmos()
    {
        foreach (Transform t in _routes)
        {
            if (!t) continue;

            Vector3 direction = transform.position + ((t.position - transform.position).normalized * 5);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, direction);
        }
    }
}
