using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public Action<int> MovingForward;
    public Action<int> MovingSide;
    public Action<bool> AllStop;

    public void Gas(bool value)
    {
        if (value)
        {
            MovingForward?.Invoke(1);
        }
        else
        {
            MovingForward?.Invoke(0);
        }
    }

    public void Break(bool value)
    {
        if (value)
        {
            MovingForward?.Invoke(-1);
        }
        else
        {
            MovingForward?.Invoke(0);
        }
    }

    public void Left(bool value)
    {
        if (value)
        {
            MovingSide?.Invoke(-1);
        }
        else
        {
            MovingSide?.Invoke(0);
        }
    }

    public void Right(bool value)
    {
        if (value)
        {
            MovingSide?.Invoke(1);
        }
        else
        {
            MovingSide?.Invoke(0);
        }
    }

    public void PauseAll(bool pause)
    {
        AllStop?.Invoke(pause);
    }
}