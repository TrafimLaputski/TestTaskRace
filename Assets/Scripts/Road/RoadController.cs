using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private ItemPools _pool = null;
    [SerializeField] private ControlSystem _controlSystem = null;
    [SerializeField] private List<RoadPart> _roads = new List<RoadPart>();
    [SerializeField] private float _speedMultiplayer = 0.1f;

    [SerializeField] private List<ItemData> _data = new List<ItemData>();

    private float _currentSpeedMultiplayer = 0f;
    private float _speed = 0f;
    private float _startDistance = 0;

    private Vector3 _finishPoticion = Vector3.zero;

    private bool _pause = false;

    private void Start()
    {
        StartCalculation();
    }

    private void StartCalculation()
    {
        if (_roads.Count > 1)
        {
            _startDistance = Vector3.Distance(_roads[0].transform.position, _roads[1].transform.position);
            _finishPoticion = -_roads[1].transform.position;
        }
        else
        {
            Debug.LogError("Small road");
        }

        if (_controlSystem != null)
        {
            _controlSystem.MovingForward += MoveSpeed;
            _controlSystem.AllStop += Pause;
        }
        else
        {
            Debug.LogError("ControlSystem is null");
        }
    }

    private void Update()
    {
        if (!_pause)
        {
            Move();
        }
    }

    private void Move()
    {
        for (int i = 0; i < _roads.Count; i++)
        {
            if (_roads[i].transform.position == _finishPoticion && i != 0)
            {
                int n = i - 1;
                if (n == 0)
                {
                   n = _roads.Count - 1;
                }

                Vector3 newPos = new Vector3(_roads[i].transform.position.x, _roads[n].transform.position.y + _startDistance, _roads[i].transform.position.z);

                _roads[i].transform.position = newPos;

                float randomValue = Random.value;

                _roads[i].ResetObjects();
                if (randomValue <= 0.5f)
                {
                    int rundNum = Random.Range(0, _data.Count);
                    RoadItem tempItem = _pool.GetItem();
                    tempItem.Construct(_data[rundNum]);

                    _roads[i].UpdateÑalculate(tempItem);
                }
            }

            _roads[i].transform.position = Vector3.MoveTowards(_roads[i].transform.position, _finishPoticion, _speed * Time.deltaTime);
        }

        _speed = Mathf.Clamp(_speed + _currentSpeedMultiplayer - 0.001f, 1f, 20f);
    }

    private void MoveSpeed(int value)
    {
        _currentSpeedMultiplayer = _speedMultiplayer * value;
    }

    private void Pause(bool value)
    {
        _pause = value;
    }
}