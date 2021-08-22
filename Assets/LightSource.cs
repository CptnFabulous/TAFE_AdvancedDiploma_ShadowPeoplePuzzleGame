using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightSource : MonoBehaviour
{
    public Light lightData { get; private set; }
    public float hypotheticalDistanceAwayIfDirectionalLight = float.MaxValue;

    private void Awake()
    {
        lightData = GetComponent<Light>();
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */


    public float HowMuchLightIsHittingThing(Collider[] collidersInThing)
    {
        Bounds boundsOfThing = TotalColliderBounds(collidersInThing);

        #region Angle check (if spot light)
        // If the light is a spotlight, make sure it is actually inside the angle. If so, proceed to other checks
        if (lightData.type == LightType.Spot)
        {
            // Checks if the object is outside the angle
            if (Vector3.Angle(transform.forward, boundsOfThing.center - transform.position) > (lightData.spotAngle / 2))
            {
                // If so, object is not in light
                return 0;
            }
        }
        #endregion



        // line of sight check
        Vector3 origin = transform.position;
        float range = lightData.range;
        if (lightData.type == LightType.Directional)
        {
            // Since a directional light has no functional position, only a direction, the 'origin' is a new point far away opposite its direction
            origin = boundsOfThing.center + (-transform.forward * hypotheticalDistanceAwayIfDirectionalLight);
            // Sets the range so the raycast will always travel far enough to hit the player
            range = Vector3.Distance(origin, boundsOfThing.center) + boundsOfThing.max.magnitude;
        }
        Vector3 direction = boundsOfThing.center - origin;

        // Performs a line of sight check. Since the range is set to the light's range, this pulls double duty as a range check
        RaycastHit lineOfSightCheck;
        if (Physics.Raycast(origin, direction, out lineOfSightCheck, range, lightData.cullingMask))
        {
            for (int i = 0; i < collidersInThing.Length; i++)
            {
                if (lineOfSightCheck.collider == collidersInThing[i])
                {
                    // One of the colliders is within line of sight and close enough to be hit by the raycast.
                    // Collider is within range and not behind cover. 
                    // If the angle check did not return false, this means the 
                    float percentage = 1 / range * lineOfSightCheck.distance;
                    return lightData.intensity * percentage;
                }
            }
        }


        // If the object was too far away for the raycast to hit
        return 0;
    }


    public static Bounds TotalColliderBounds(Collider[] colliders)
    {
        if (colliders.Length == 1)
        {
            return colliders[0].bounds;
        }
        if (colliders.Length <= 0)
        {
            return new Bounds();
        }

        // Do actual check

        // Get the collider of the first hitbox in the list, and store its bounds.
        Bounds totalBounds = colliders[0].bounds;
        // A standard for loop, skips zero because the first one is automatically assigned
        for (int i = 1; i < colliders.Length; i++)
        {
            // For each new collider, check their minimum and maximum extents against the main bounds, and expand to encompass them if necessary.
            Bounds b = colliders[i].bounds;
            totalBounds.min = Vector3.Min(totalBounds.min, b.min);
            totalBounds.max = Vector3.Max(totalBounds.max, b.max);
        }

        return totalBounds;
    }
}
