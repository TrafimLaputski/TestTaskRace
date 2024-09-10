using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    [SerializeField] private ControlSystem _controlSystem = null;
    [SerializeField] private PlayerCar _playerCar;
    [SerializeField] private float _speed = 1.0f;

    private bool _race = false;
    private bool _pause = false;

    private CancellationTokenSource _cancellationTokenSource;

    private void Start()
    {
        StartCalculate();
        Move();
    }

    private void StartCalculate()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _controlSystem.AllStop += Pause;
    }

    private async Task StateTimer(int time)
    {
        await Task.Delay(time, _cancellationTokenSource.Token);
        _race = !_race;
    }

    private async Task Move()
    {
        while (true)
        {
            StateTimer(30000);

            while (_race)
            {
                await Task.Yield();

                if (!_pause)
                {
                    StateRace();
                }
                
            }
            
            bool moveToStart = true;

            while (moveToStart)
            {
                await Task.Yield();

                if (!_pause)
                {
                    moveToStart = StateMoveToStart();
                }
            }

            await StateTimer(5000);
        }

    }

    private void StateRace()
    {
        Vector3 target = new Vector3(_playerCar.transform.position.x, -2, 0);

        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _playerCar.transform.position) <= 1f)
        {
            _playerCar.TakeDamage(10000);
        }
    }

    private bool StateMoveToStart()
    {
        Vector3 target = new Vector3(transform.position.x, -6, 0);
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) <= 0.5f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource.Cancel();
    }

    private void Pause(bool value)
    {
        _pause = value;
    }
}
