using System.Collections.Generic;
using UnityEngine;

public class RoadPart : MonoBehaviour
{
    [SerializeField] private List<Transform> _objectPoint = new List<Transform>();
    RoadItem _item = null;

    public void Update—alculate(RoadItem item)
    {
        int rundNum = Random.Range(0, _objectPoint.Count);

        item.gameObject.SetActive(true);
        item.transform.parent = transform;
        item.transform.position = _objectPoint[rundNum].position;

        _item = item;
    }

    public void ResetObjects()
    {
        if (_item != null)
        {
            _item.ItemReturn();
            _item = null;
        }
    }
}
