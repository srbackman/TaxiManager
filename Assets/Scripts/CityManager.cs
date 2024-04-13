using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public int Id;
    public Transform StartPoint;
    public Transform EndPoint;
    public float TimeElapsed;
    public float BonusTimeLimit;
}

public class CityManager : MonoBehaviour
{
    [SerializeField] private int MaxTasks = 12;
    [SerializeField] private float NewTaskInterval = 7.0f;

    List<Task> Tasks = new List<Task>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTasks()
    {

    }

    public void CityManagment()
    {

    }
}
