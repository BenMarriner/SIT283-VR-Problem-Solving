using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private GameObject hitObject;
    private float hitDistance;

    public GameObject laserPrefab;
    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public bool laserEnabled = false;

    public GameObject HitObject { get { return hitObject; } }
    public float HitDistance { get { return hitDistance; } }
    public Vector3 HitPoint { get { return hitPoint; } }

    private void ShowLaser()
    {
        RaycastHit hit;
        bool hitDetected = LaserRayCast(out hit);

        laser.SetActive(hitDetected);

        if (hitDetected)
        {
            startPos = transform.position;
            laserTransform.position = Vector3.Lerp(startPos, hitPoint, 0.5f);
            laserTransform.LookAt(hitPoint);
            laserTransform.localScale = new Vector3
            (
                laserTransform.localScale.x,
                laserTransform.localScale.y,
                hit.distance
            );
        }
    }

    private bool LaserRayCast(out RaycastHit hitRay)
    {
        RaycastHit hit;

        if (Physics.Raycast(startPos, transform.forward, out hit, 100))
        {
            hitRay = hit;
            hitDistance = hit.distance;
            hitPoint = hit.point;
            hitObject = hit.transform.gameObject;

            return true;
        }

        hitRay = hit;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (laserEnabled) ShowLaser();
        else laser.SetActive(false);
    }
}
