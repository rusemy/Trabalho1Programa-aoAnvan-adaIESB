using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRunner
{
    int runnerID { get; set; }
    int lapNumber { get; set; }
    int nextCheckPoint { get; set; }
    bool isStunned { get; set; }
    float maxSpeed { get; set; }
    PowerUp availablePowerUp { get; set; }

    void CompleteLap();

    bool WrongDirection();

    void UsePowerUp();
}