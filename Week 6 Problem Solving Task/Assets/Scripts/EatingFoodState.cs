using UnityEngine;

public class EatingFoodState : FishState
{
    FishInfo otherFish;
    
    public override void EnterState(FishStateManager fish)
    {
        //otherFish = fish.otherCollidingFish;
    }

    public override void UpdateState(FishStateManager fish)
    {
        
    }
}