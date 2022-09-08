using UnityEngine;

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
        }
        else if (!fish.closestNearbyFoodFish.Equals(default(FishInfo))) // Is there a closest food fish nearby?
        {
            if (fish.closestNearbyFoodFish.fish) // Is the fish still in the world?
            {
                if (fish.otherCollidingFish == fish.closestNearbyFoodFish.fish) // Is this fish touching the other fish?
                {
                    fish.SwitchState(FishStates.EatFood);
                }
                else
                {
                    fish.movement.SwimTowards(fish.closestNearbyFoodFish.fish);
                }
            }
            else
            {
                fish.SwitchState(FishStates.LookForFood);
            }
        }
        else
        {
            fish.SwitchState(FishStates.LookForFood);
        }
    }
}