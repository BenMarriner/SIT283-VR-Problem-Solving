﻿using UnityEngine;

public class ChasingFoodState : FishState
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

        if (fish.otherCollidingFish == fish.closestNearbyFoodFish.fish)
        {
            fish.SwitchState(FishStates.EatFood);
            return;
        }

        if (!fish.closestNearbyFoodFish.Equals(default(FishInfo))) fish.movement.SwimTowards(fish.closestNearbyFoodFish.fish);
        else fish.SwitchState(FishStates.LookForFood);
    }
}