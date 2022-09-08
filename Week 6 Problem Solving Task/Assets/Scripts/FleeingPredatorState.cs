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

        if (!fish.closestNearbyPredatorFish.Equals(default(FishInfo)))
        {
            if (fish.otherCollidingFish == fish.closestNearbyPredatorFish.fish)
            {
                fish.SwitchState(FishStates.BeDigested);
            }
            else
            {
                fish.movement.SwimAway(fish.closestNearbyPredatorFish.fish);
            }
        }
        else
        {
            fish.SwitchState(FishStates.LookForFood);
        }
    }
}