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
    public bool debugLists = false;
    public FishStates currentStateEnum { get; private set; }

    [HideInInspector]
    public List<FishInfo> food, predators;
    [HideInInspector]
    public MovementController movement;
    public float searchRadius = 10.0f;
    [HideInInspector]
    public int fishID;
    [HideInInspector]
    public FishInfo closestNearbyFoodFish;
    [HideInInspector]
    public FishInfo closestNearbyPredatorFish;
    [HideInInspector]
    public GameObject otherCollidingFish;
    [HideInInspector]
    public const float digestionRate = 0.1f;

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

        transform.localScale = Vector3.one * fishSize;

        // Enter looking for food state to start
        SwitchState(FishStates.LookForFood);
    }

    // Update is called once per frame
    void Update()
    {
        // Clean up fish that are out of the search radius
        food.RemoveAll(fish => fish.distanceToTarget > searchRadius);
        predators.RemoveAll(fish => fish.distanceToTarget > searchRadius);

        //foreach (var fish in food)
        //    if (fish.distanceToTarget > searchRadius) food.Remove(fish);
        //foreach (var fish in predators)
        //    if (fish.distanceToTarget > searchRadius) predators.Remove(fish);

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

        var stateCanvas = transform.Find("Canvas");
        var stateText = stateCanvas.transform.Find("StateText");
        stateText.GetComponent<Text>().text = currentStateEnum.ToString();

        Debug.Log(searchRadius);
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