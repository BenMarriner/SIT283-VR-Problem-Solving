using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FishStates
{
    LookForFood,
    ChaseFood,
    EatFood,
    FleePredators,
    BeDigested
}

public struct FishInfo
{
    public int fishID;
    public int fishInfoID;
    public GameObject fish;
    public Vector3 position;
    public float distanceToTarget;
    public float fishSize;

    public FishInfo(int fishID, int fishInfoID, GameObject fish, Vector3 pos, float distance, float size)
    {
        this.fishID = fishID;
        this.fishInfoID = fishInfoID;
        this.fish = fish;
        position = pos;
        distanceToTarget = distance;
        fishSize = size;
    }
}

public class FishStateManager : MonoBehaviour
{
    public float fishSize = 1.0f;
    public Vector3 minBounds, maxBounds;
    public float swimSpeed;
    public bool debugLists = false;
    public bool debugState = false;
    public FishStates currentStateEnum { get; private set; }

    public List<FishInfo> food, predators;
    public MovementController movement;
    public float searchRadius = 100.0f;
    public int fishID;
    public FishInfo closestNearbyFoodFish;
    public FishInfo closestNearbyPredatorFish;
    public GameObject otherCollidingFish;
    public readonly float digestionRate = 10.0f;


    private FishState[] _states =
    {
        new LookForFoodState(),
        new ChasingFoodState(),
        new EatingFoodState(),
        new FleeingPredatorState(),
        new BeingDigested()
    };

    private FishState currentState { get { return _states[(int)currentStateEnum]; } }

    // Start is called before the first frame update
    void Start()
    {
        food = new List<FishInfo>();
        predators = new List<FishInfo>();
        movement = GetComponent<MovementController>();

        movement.baseSpeed = swimSpeed;
        movement.minBounds = minBounds;
        movement.maxBounds = maxBounds;

        // Enter looking for food state to start
        SwitchState(FishStates.LookForFood);
    }

    // Update is called once per frame
    void Update()
    {
        // Clean up fish that are out of the search radius
        food.RemoveAll(fish => Vector3.Distance(transform.position, fish.position) > searchRadius);
        predators.RemoveAll(fish => Vector3.Distance(transform.position, fish.position) > searchRadius);

        // Clean up closest invalidated fish (i.e.: Fish that have been fully digested and removed from the world)
        if (!closestNearbyFoodFish.fish)        closestNearbyFoodFish = default(FishInfo);
        if (!closestNearbyPredatorFish.fish)    closestNearbyPredatorFish = default(FishInfo);

        // Extend search radius to maintain uniformity with fish size
        searchRadius = fishSize * 3.0f;

        transform.localScale = new Vector3()
        {
            x = Mathf.Abs(transform.localScale.x),
            y = Mathf.Abs(transform.localScale.y),
            z = Mathf.Abs(transform.localScale.z)
        };


        //foreach (var fish in food)
        //    if (fish.distanceToTarget > searchRadius) food.Remove(fish);
        //foreach (var fish in predators)
        //    if (fish.distanceToTarget > searchRadius) predators.Remove(fish);

        transform.localScale = Vector3.one * fishSize;
        currentState.UpdateState(this);

        if (debugLists)
        {
            string foodListDebugString = "";
            foreach(var fish in food) foodListDebugString += $"{fish.fishID} ";
            Debug.Log($"{gameObject.name}, Food seen: {foodListDebugString}");

            string predatorsListDebugString = "";
            foreach (var fish in predators) predatorsListDebugString += $"{fish.fishID} ";
            Debug.Log($"{gameObject.name}, Predators seen: {predatorsListDebugString}");
        }

        if (debugState) Debug.Log($"{gameObject.name}, State: {currentStateEnum}");

        var stateCanvas = transform.Find("Canvas");
        var stateText = stateCanvas.transform.Find("StateText");
        stateText.GetComponent<Text>().text = currentStateEnum.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        otherCollidingFish = collision.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        otherCollidingFish = null;
    }

    public void SwitchState(FishStates state)
    {
        currentStateEnum = state;
        currentState.EnterState(this);
    }
}