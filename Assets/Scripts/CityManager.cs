using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public enum TraficLightState
{
    Horizontal,
    Vertical
}

public class Task
{
    public int Id;
    public Transform StartPoint;
    public Transform EndPoint;
    public int TimeElapsed;
    public int BonusTimeLimit;
}

public class CityManager : MonoBehaviour
{
    [SerializeField] private int MaxTasks = 12;
    [SerializeField] private float NewTaskInterval = 7.0f;
    [SerializeField] private int MinBonusTickLimit = 1;
    [SerializeField] private int MaxBonusTickLimit = 10;

    int IdCounter = 0;
    float NewTaskTimer = 0f;
    List<Task> Tasks = new List<Task>();
    ClassLibrary lib;

    private void Awake()
    {
        lib = FindFirstObjectByType<ClassLibrary>();
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
                NewTaskTimer = NewTaskInterval;
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
        TraficLightManagment();
    }

    private void TraficLightManagment()
    {

    }
}
