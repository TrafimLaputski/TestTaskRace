using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField] private ControlSystem _controlSystem = null;
    [SerializeField] private PlayerUI _playerUI = null;
    [SerializeField] private PlayerMagnet _playerMagnet = null;

    [SerializeField] private int _health = 100;
    [SerializeField] private float _speedMultiplayer = 1f;

    private float _speedBonus = 0;
    private float _moveSpeed = 0;
    private float _sideSpeed = 0;
    private float _currentMoveSpeedMultiplayer = 0f;

    private int _currentHealth = 0;
    private int _points = 0;

    private bool _shield = false;
    private bool _pause = false;

    private CancellationTokenSource _cancellationTokenSource;
    private CarBonusController _bonusController = null;
    private SaveManager _saveManager = null;

    public CarBonusController BonusController
    {
        get { return _bonusController; }
    }

    public float SpeedBonus
    {
        get { return _speedBonus; }
        set { _speedBonus = value; }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

    public bool Shield
    {
        get { return _shield; }
        set { _shield = value; }
    }

    private void Start()
    {
        StartCalculate();
    }

    private void StartCalculate()
    {
        _currentHealth = _health;

        if (_controlSystem != null)
        {
            _controlSystem.MovingForward += SpeedMove;
            _controlSystem.MovingSide += SpeedSide;
        }
        else
        {
            Debug.LogError("ControlSystem is null");
        }

        _cancellationTokenSource = new CancellationTokenSource();
        _bonusController = new CarBonusController(this, _playerUI, _playerMagnet, _controlSystem, _cancellationTokenSource.Token);
        _saveManager = new SaveManager();

        _controlSystem.AllStop += Pause;
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
        float newX = Mathf.Clamp(transform.position.x + _sideSpeed  * Time.deltaTime, -2f, 2f);
        float newY = Mathf.Clamp(transform.position.y + (_moveSpeed + _speedBonus) * Time.deltaTime, -4f, 2f);

        Vector3 newPos = new Vector3(newX, newY, 0);

        transform.position = newPos;

        _moveSpeed = Mathf.Clamp(_moveSpeed + _currentMoveSpeedMultiplayer - 0.001f, -5f, 5f);

        int addPoints = (int)Mathf.Clamp(_moveSpeed, 1, 5);
        TakeCoins(addPoints);
    }

    private void SpeedMove(int value)
    {
        _currentMoveSpeedMultiplayer = _speedMultiplayer * value;
        _moveSpeed = 0;
    }

    private void SpeedSide(int value)
    {
        _sideSpeed = value;
    }

    public void TakeCoins(int value)
    {
        _points += value;
        _playerUI.ChandeScore(_points);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0 && _shield)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _health);
        _playerUI.ChangeHealth(_health, _currentHealth);

        if (_currentHealth == 0)
        {
            int bestPoints = _saveManager.LoadData("points");

            if (_points > bestPoints)
            {
                _saveManager.SaveData("points", _points);
                bestPoints = _points;
            }
            _controlSystem.PauseAll(true);
            _playerUI.ShowFinalMenu(_points, bestPoints);
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