using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNodes : MonoBehaviour
{
    [SerializeField] bool _nodesHaveChildren = true;
    [SerializeField] int _numberOfChildObjects = 3;

    public Transform[] _nodeTransforms;

    private void Awake()
    {
        _nodeTransforms = GetComponentsInChildren<Transform>();
        int originalLenght = _nodeTransforms.Length;

        if (_nodesHaveChildren && _numberOfChildObjects > 0)
        {
            Transform[] withoutParent = new Transform[(originalLenght - 1) / _numberOfChildObjects];
            for (int i = 1; i - 1 < (originalLenght - 1) / _numberOfChildObjects; i++)
            {
                withoutParent[i - 1] = _nodeTransforms[(i * _numberOfChildObjects) - 2];
            }
            _nodeTransforms = withoutParent;
        }

        List<Transform> filter = new List<Transform>();
        for (int i = 1; i < _nodeTransforms.Length; i++)
        {
            filter.Add(_nodeTransforms[i]);
        }

        _nodeTransforms = new Transform[_nodeTransforms.Length - 1];

        for (int i = 0; i < _nodeTransforms.Length; i++)
        {
            _nodeTransforms[i] = filter[i];
        }
        //else
        //{
        //    Transform[] withoutParent = new Transform[(originalLenght - 1)];
        //    for (int i = 1; i < (originalLenght - 1); i++)
        //    {
        //        withoutParent[i] = _nodeTransforms[i];
        //    }
        //    _nodeTransforms = withoutParent;
        //}
    }
}
