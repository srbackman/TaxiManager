using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public enum CustomerType
{
    normal,
    senior,
    petPerson,
    police
}

public class Task
{
    public int Id;
    public Transform StartPoint;
    public Transform EndPoint;
    public int TimeElapsed;
    public int BonusTimeLimit;
    public CustomerType Customer;
}


public class CityManager : MonoBehaviour
{
    [SerializeField] private int MaxTasks = 12;
    [SerializeField] private float NewTaskMinInterval = 7.0f;
    [SerializeField] private float NewTaskMaxInterval = 15.0f;
    [SerializeField] private int MinBonusTickLimit = 1;
    [SerializeField] private int MaxBonusTickLimit = 10;

    int IdCounter = 0;
    float NewTaskTimer = 0f;
    List<Task> Tasks = new List<Task>();
    List<NodeRoutes> TraficLightJuncsions = new List<NodeRoutes>();
    ClassLibrary lib;

    private void Awake()
    {
        lib = FindFirstObjectByType<ClassLibrary>();
    }

    private void Start()
    {
        foreach (Transform nodeTransform in lib.navigationNodes._nodeTransforms)
        {
            NodeRoutes node = nodeTransform.GetComponent<NodeRoutes>();
            if (node && node.HasTraficLights)
            {
                TraficLightJuncsions.Add(node);
            }
        }

    }

    private void CheckTasks()
    {
        if (Tasks.Count < MaxTasks)
        {
            NewTaskTimer -= Time.deltaTime;
            if (NewTaskTimer <= 0)
            {
                Task newTask = new Task();
                IdCounter++;
                NewTaskTimer = Random.Range(NewTaskMinInterval, NewTaskMaxInterval);
                int randomStartIndex = Random.Range(0, lib.navigationNodes._nodeTransforms.Length);
                int randomEndIndex = -1;
                while (randomEndIndex == -1 || randomEndIndex == randomStartIndex)
                {
                    randomEndIndex = Random.Range(0, lib.navigationNodes._nodeTransforms.Length);
                }
                Transform startTransform = lib.navigationNodes._nodeTransforms[randomStartIndex];
                Transform endTransform = lib.navigationNodes._nodeTransforms[randomEndIndex];
                newTask.Id = IdCounter;
                newTask.StartPoint = startTransform;
                newTask.EndPoint = endTransform;
                newTask.BonusTimeLimit = Random.Range(MinBonusTickLimit, MaxBonusTickLimit + 1);

                Tasks.Add(newTask);
            }
        }

        foreach (Task task in Tasks)
        {
            task.TimeElapsed++;
        }
    }

    public void CityManagment()
    {
        CheckTasks();
        
    }

    public List<NodeRoutes> TraficLightManagment()
    {
        foreach (NodeRoutes node in TraficLightJuncsions)
        {
            if (node.TraficGreenState == TraficLightState.Horizontal)
            {
                if (!CheckHorizontalRoad(node) && CheckVerticalRoad(node))
                {
                    node.TraficGreenState = TraficLightState.Vertical;
                    node.TraficRedState = TraficLightState.Horizontal;
                }
            }
            else //Green on vertical.
            {
                if (!CheckVerticalRoad(node) && CheckHorizontalRoad(node))
                {
                    node.TraficGreenState = TraficLightState.Horizontal;
                    node.TraficRedState = TraficLightState.Vertical;
                }
            }
        }

        return (TraficLightJuncsions);
    }

    private bool CheckHorizontalRoad(NodeRoutes node)
    {
        foreach (Transform t in node._routes)
        {
            if (t.position.x == node.transform.position.x)
                continue;

            NodeRoutes horizontalNode = t.GetComponent<NodeRoutes>();
            if (horizontalNode && horizontalNode.VehiclesAtNode > 0)
                return (true);
        }

        return (false);
    }

    private bool CheckVerticalRoad(NodeRoutes node)
    {
        foreach (Transform t in node._routes)
        {
            if (t.position.y == node.transform.position.y)
                continue;

            NodeRoutes verticalNode = t.GetComponent<NodeRoutes>();
            if (verticalNode && verticalNode.VehiclesAtNode > 0)
                return (true);
        }

        return (false);
    }
}
