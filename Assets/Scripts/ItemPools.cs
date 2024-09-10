using System.Collections.Generic;
using UnityEngine;

public class ItemPools : MonoBehaviour
{
    [SerializeField] RoadItem _roadItem = null;
    [SerializeField] private int _minItemsCount = 10;
    [SerializeField] private int _maxItemsCount = 100;
    [SerializeField] private bool _notLimit = false;

    protected List<RoadItem> _items = new List<RoadItem>();

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _minItemsCount; i++)
        {
            RoadItem temp = CreateObject();
            _items.Add(temp);
        }
    }

    private RoadItem CreateObject()
    {
        RoadItem temp = Instantiate(_roadItem, transform);
        temp.gameObject.SetActive(false);
        temp.ReturnToPool += Return;
        return temp;
    }

    public RoadItem GetItem()
    {
        foreach (RoadItem item in _items)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }

        if (_notLimit || _items.Count < _maxItemsCount)
        {
            RoadItem temp = CreateObject();
            _items.Add(temp);
            return temp;
        }

        return null;
    }

    public void Return(RoadItem item)
    {
        item.gameObject.SetActive(false);
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
    }
}
