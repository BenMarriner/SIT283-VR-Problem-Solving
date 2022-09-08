using UnityEngine;

public class EatingFoodState : FishState
{
    GameObject otherFish;
    
    public override void EnterState(FishStateManager fish)
    {
        otherFish = fish.otherCollidingFish;
    }

    public override void UpdateState(FishStateManager fish)
    {
        FishRoutines.EvaluateSurroundingFish(fish);
        FishRoutines.GetClosestPredator(fish);
        FishRoutines.GetClosestFood(fish);

        // Fish will prioritise their search for predators over their prey to simulate their urge to survive
        //if (!fish.closestNearbyPredatorFish.Equals(default(FishInfo)))
        //{
        //    fish.SwitchState(FishStates.FleePredators);
        //}
        if (otherFish)
        {
            if (otherFish.TryGetComponent<FishStateManager>(out var otherFishStateManager))
            {
                otherFishStateManager.fishSize -= fish.digestionRate * Time.deltaTime;
                fish.fishSize += fish.digestionRate * Time.deltaTime;
            }
        }
        else if (!fish.closestNearbyFoodFish.Equals(default(FishInfo)))
        {
            fish.SwitchState(FishStates.ChaseFood);
        }
        else
        {
            fish.SwitchState(FishStates.LookForFood);
        }
    }
}