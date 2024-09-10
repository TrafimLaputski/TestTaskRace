using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CarBonusController
{
    private PlayerMagnet _playerMagnet = null;
    private PlayerCar _playerCar = null;
    private PlayerUI _playerUI = null;
    private CancellationToken _cancellationToken;

    private bool _pause = false;

    public CarBonusController(PlayerCar playerCar, PlayerUI playerUI, PlayerMagnet playerMagne, ControlSystem controlSystem, CancellationToken cancellationToken)
    {
        _playerCar = playerCar;
        _playerUI = playerUI;
        _playerMagnet = playerMagne;
        controlSystem.AllStop += Pause;

        _cancellationToken = cancellationToken;
    }

    public async void SetSpeedBonus(ItemData data)
    {
        _playerCar.SpeedBonus += data.itemValue;

        if (data.duration > 0)
        {
            await Timer(_playerUI.Ready, data, _cancellationToken);
        }

        _playerCar.SpeedBonus -= data.itemValue;
    }

    public async void SetShield(ItemData data)
    {
        _playerCar.Shield = true;

        if (data.duration > 0)
        {
            await Timer(_playerUI.Ready, data, _cancellationToken);
        }

        _playerCar.Shield = false;
    }

    public async void SetMagnet(ItemData data)
    {
        _playerMagnet.TurnOn(true);

        if (data.duration > 0)
        {
            await Timer(_playerUI.Ready ,data, _cancellationToken);
        }

        _playerMagnet.TurnOn(false);
    }

    private async Task Timer(bool showUI, ItemData data, CancellationToken cancellationToken)
    {
        if (showUI)
        {
            if (data.barSprite != null)
            {
                _playerUI.SetBonus(data);
            }

            if (data.itemBacklight != null)
            {
                _playerUI.SetBonusVisual(data);
            }
        }

        for (int i = data.duration; i > 0; i--)
        {
            while (_pause)
            {
                await Task.Delay(1, cancellationToken);
            }

            await Task.Delay(1, cancellationToken);

            if (showUI && data.barSprite != null)
            {
                _playerUI.ChangeBonus(data.duration, i);
            }
        }

        if (showUI && data.barSprite != null)
        {
            _playerUI.Clear();
        }
    }

    private void Pause(bool value)
    {
        _pause = value;
    }
}