using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private float _magnetSpeed = 1f;
    [SerializeField] private Collider2D _magnetCollader = null;
    private List<GameObject> _gravityObjects = new List<GameObject>();

    public void TurnOn(bool value)
    {
        _magnetCollader.enabled = value;
    }

    public void AddGravityObject(RoadItem roadItem)
    {
        _gravityObjects.Add(roadItem.gameObject);
        roadItem.ReturnToPool += RemoveGravityObject;
    }

    private void RemoveGravityObject(RoadItem roadItem)
    {
        _gravityObjects.Remove(roadItem.gameObject);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        foreach (GameObject gravityObject in _gravityObjects)
        {
            gravityObject.transform.position = Vector3.MoveTowards(gravityObject.transform.position, transform.position, _magnetSpeed * Time.deltaTime);
        }
    }
}