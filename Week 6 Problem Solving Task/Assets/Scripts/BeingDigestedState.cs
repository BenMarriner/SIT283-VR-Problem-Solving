using UnityEngine;

/*
 * In this state, fish will become immobilised the instant they begin being digested.
 * If the predator is unable to completely digest the fish (i.e.; they were pursued by another predator),
 * the fish will still remain immobilised. Other predators can feed on them
 * The fish will be completely removed from the world once it's size has reached zero (they have been completely digested)
*/
public class BeingDigested : FishState
{
    public override void EnterState(FishStateManager fish)
    {
        fish.movement.canSwim = false;
        fish.movement.isFrozen = true;
    }

    public override void UpdateState(FishStateManager fish)
    {
        if (fish.fishSize <= 0) Object.Destroy(fish.gameObject);
        Debug.Log(fish.transform.localScale);
    }
}