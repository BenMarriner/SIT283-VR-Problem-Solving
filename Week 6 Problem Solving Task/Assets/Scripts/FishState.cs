using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FishState
{
    public abstract void EnterState(FishStateManager fish);
    public abstract void UpdateState(FishStateManager fish);
}
