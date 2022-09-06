using System.Collections.Generic;
using UnityEngine;

public class LookForFoodState : FishState
{

    public override void EnterState(FishStateManager fish)
    {

    }

    public override void UpdateState(FishStateManager fish)
    {
        FishRoutines.EvaluateSurroundingFish(fish);
        FishRoutines.GetClosestPredator(fish);
        FishRoutines.GetClosestFood(fish);

        // Fish will prioritise their search for predators over their prey to simulate their urge to survive
        if (!fish.closestNearbyPredatorFish.Equals(default(FishInfo)))
        {
            fish.SwitchState(FishStates.FleePredators);
            return;
        }

        if (!fish.closestNearbyFoodFish.Equals(default(FishInfo)))
        {
            fish.SwitchState(FishStates.ChaseFood);
            return;
        }

        fish.movement.SwimRandom();
    }

}