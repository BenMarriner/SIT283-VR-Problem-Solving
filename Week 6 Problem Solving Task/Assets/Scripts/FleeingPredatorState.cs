using UnityEngine;

public class FleeingPredatorState : FishState
{
    public override void EnterState(FishStateManager fish)
    {

    }

    public override void UpdateState(FishStateManager fish)
    {
        FishRoutines.EvaluateSurroundingFish(fish);
        FishRoutines.GetClosestPredator(fish);

        if (fish.otherCollidingFish == fish.closestNearbyPredatorFish.fish)
        {
            fish.SwitchState(FishStates.BeDigested);
            return;
        }

        if (!fish.closestNearbyPredatorFish.Equals(default(FishInfo))) fish.movement.SwimAway(fish.closestNearbyPredatorFish.fish);
        else fish.SwitchState(FishStates.LookForFood);
    }
}