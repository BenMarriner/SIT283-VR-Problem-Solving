using System.Collections.Generic;
using UnityEngine;

public static class FishRoutines
{
    public static void EvaluateSurroundingFish(FishStateManager fish)
    {
        var fishes = Physics.OverlapSphere(fish.transform.position, fish.searchRadius);

        // Search for nearby fishes
        foreach (var otherFishCollider in fishes)
        {
            if (otherFishCollider.TryGetComponent<FishStateManager>(out var otherFish))
            {
                List<FishInfo> fishList;

                if (otherFish.fishSize != fish.fishSize)
                {
                    fishList = otherFish.fishSize < fish.fishSize ? fish.food : fish.predators; // List of fish to access. Will reference a different list depending on fish's size
                    var existingFishInfo = fishList.Find(fish => fish.fishID == otherFish.fishID); // Used for modifying existing fish if it is in the list

                    // If the fish is already listed, update existing information.
                    // Otherwise, add new fish info
                    if (!existingFishInfo.Equals(default(FishInfo)))
                    {
                        existingFishInfo.position = otherFish.transform.position;
                        existingFishInfo.distanceToTarget = Vector3.Distance(fish.transform.position, otherFish.transform.position);
                        existingFishInfo.fishSize = otherFish.fishSize;
                    }
                    else
                    {
                        FishInfo newFishInfo = new FishInfo()
                        {
                            fishID = otherFish.fishID,
                            fishInfoID = fishList.Count,
                            fish = otherFish.gameObject,
                            position = otherFish.transform.position,
                            distanceToTarget = Vector3.Distance(fish.transform.position, otherFish.transform.position),
                            fishSize = otherFish.fishSize
                        };

                        fishList.Add(newFishInfo);
                    }
                }
            }
        }
    }
    public static void GetClosestPredator(FishStateManager fish)
    {
        // Get closest predator
        if (fish.predators.Count > 0)
        {
            FishInfo closestFish = fish.predators[0];

            foreach (var fishInfo in fish.predators)
            {
                if (fishInfo.distanceToTarget < closestFish.distanceToTarget)
                {
                    closestFish = fishInfo;
                }
            }

            fish.closestNearbyPredatorFish = closestFish;
        }
        else fish.closestNearbyPredatorFish = default;
    }
    public static void GetClosestFood(FishStateManager fish)
    {
        // Get closest food
        if (fish.food.Count > 0)
        {
            FishInfo closestFish = fish.food[0];

            foreach (var fishInfo in fish.food)
            {
                if (fishInfo.distanceToTarget < closestFish.distanceToTarget)
                {
                    closestFish = fishInfo;
                }
            }

            fish.closestNearbyFoodFish = closestFish;
        }
        else fish.closestNearbyFoodFish = default;
    }
}
