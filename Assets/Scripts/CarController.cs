﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public Roads roads;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Vertex position;

    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    private WheelCollider[] colliders;
    private List<Vertex> waypoints;

    public void Start()
    {
        Initialize();
        SelectDestination();
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        frontLeft.steerAngle = steering;
        frontRight.steerAngle = steering;

        rearLeft.motorTorque = motor;
        rearRight.motorTorque = motor;

        foreach (WheelCollider collider in colliders) {
            ApplyLocalPositionToVisuals(collider);
        }
    }

    private void Initialize()
    {
        colliders = new WheelCollider[] { frontLeft, frontRight, rearLeft, rearRight };

        foreach (WheelCollider collider in colliders)
        {
            collider.ConfigureVehicleSubsteps(5, 12, 15);
        }
    }

    private void SelectDestination()
    {
        waypoints = roads.GetPathToRandomTarget(position);
    }

    private void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
