using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishStates
{
    LookForFood,
    ChaseFood,
    EatFood,
    FleePredators,
    BeDigested
}

public class FishStateManager : MonoBehaviour
{
    public List<GameObject> food, predators;

    private FishState _currentState;
    private FishState[] _states;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchState(FishStates state)
    {

    }
}
