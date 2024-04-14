using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMover : MonoBehaviour
{
    private ClassLibrary lib;
    [SerializeField] private float _entitySpeed = 2f;
    [SerializeField] private float _entityNodeDistanceTreshold = 1f;

    //[HideInInspector] public List<Transform> _navigationNodeList = new List<Transform>();

    private void Awake()
    {
        lib = FindObjectOfType<ClassLibrary>();
    }

    void Update()
    {
        //if (_navigationNodeList.Count > 0)
        //{
        //    NodeManager();
        //}
    }

    public List<Transform> GetNavigationNodes(Transform target)
    {
        List<Transform> _navigationNodeList = lib.dijkstraSearch.GetRoute(transform, target);

        //for (int i = 0; i < _navigationNodeList.Count; i++)
        //{
        //    print(transform.name + ", To: " + _navigationNodeList[i]);
        //}
        print("moving");
        return (_navigationNodeList);
    }

    //private void NodeManager() /*Node order and handling.*/
    //{
    //    Move(_navigationNodeList[0]);
    //    CheckIfAtDestination();
    //}

    public void Teleport(Transform self, Transform currentTarget)
    {
        
        //self.LookAt(currentTarget);
        self.position = currentTarget.position;
    }

    public void Move(Transform currentTarget)
    {
        Vector3 velocity;
        Vector3 allDirections;
        allDirections = new Vector3((currentTarget.position.x - transform.position.x), 0f, (currentTarget.position.z - transform.position.z)).normalized;
        /*Set velocity.*/
        velocity = allDirections * _entitySpeed;
        transform.position += (velocity * Time.deltaTime); /*Move.*/
    }

    //private void CheckIfAtDestination()
    //{
    //    float distance = Vector3.Distance(transform.position, _navigationNodeList[0].position);

    //    if (_entityNodeDistanceTreshold >= distance)
    //    {
    //        print("clear: " + _navigationNodeList[0]);
    //        _navigationNodeList.RemoveAt(0);
    //    }
    //}

    public bool CheckIfAtDestination(Transform _navigationNodeList)
    {
        float distance = Vector3.Distance(transform.position, _navigationNodeList.position);

        if (_entityNodeDistanceTreshold >= distance)
        {
            print("clear: " + _navigationNodeList);
            return (true);
        }
        return (false);
    }
}
