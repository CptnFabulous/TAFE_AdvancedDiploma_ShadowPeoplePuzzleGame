using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightSource : MonoBehaviour
{
    public Light LightData { get; private set; }
    public MeshRenderer visual;
    public Material onMaterial;
    public Material offMaterial;
    float hypotheticalDistanceAwayIfDirectionalLight = float.MaxValue;

    private void Awake()
    {
        LightData = GetComponent<Light>();
    }
    
    private void OnEnable()
    {
        Debug.Log("Enabling light source " + name);
        LightData.enabled = true;
        visual.material = onMaterial;
    }

    private void OnDisable()
    {
        Debug.Log("Disabling light source " + name);
        LightData.enabled = false;
        visual.material = offMaterial;
    }
    
    public float HowMuchLightIsHittingThing(Collider[] collidersInThing)
    {
        if (LightData.enabled == false)
        {
            return 0;
        }
        
        Bounds boundsOfThing = TotalColliderBounds(collidersInThing);

        #region Angle check (if spot light)
        // If the light is a spotlight, make sure it is actually inside the angle. If so, proceed to other checks
        if (LightData.type == LightType.Spot)
        {
            // Checks if the object is outside the angle
            if (Vector3.Angle(transform.forward, boundsOfThing.center - transform.position) > (LightData.spotAngle / 2))
            {
                // If so, object is not in light
                return 0;
            }
        }
        #endregion



        // line of sight check
        Vector3 origin = transform.position;
        float range = LightData.range;
        if (LightData.type == LightType.Directional)
        {
            // Since a directional light has no functional position, only a direction, the 'origin' is a new point far away opposite its direction
            origin = boundsOfThing.center + (-transform.forward * hypotheticalDistanceAwayIfDirectionalLight);
            // Sets the range so the raycast will always travel far enough to hit the player
            range = Vector3.Distance(origin, boundsOfThing.center) + boundsOfThing.max.magnitude;
        }
        Vector3 direction = boundsOfThing.center - origin;

        // Performs a line of sight check. Since the range is set to the light's range, this pulls double duty as a range check
        RaycastHit lineOfSightCheck;
        if (Physics.Raycast(origin, direction, out lineOfSightCheck, range, LightData.cullingMask))
        {
            for (int i = 0; i < collidersInThing.Length; i++)
            {
                if (lineOfSightCheck.collider == collidersInThing[i])
                {
                    // One of the colliders is within line of sight and close enough to be hit by the raycast.
                    // Collider is within range and not behind cover. 
                    // If the angle check did not return false, this means the 
                    float percentage = 1 / range * lineOfSightCheck.distance;
                    return LightData.intensity * percentage;
                }
            }
        }


        // If the object was too far away for the raycast to hit
        return 0;
    }

    /// <summary>
    /// Returns a combined bounds covering the whole object. If array is empty, an empty bounds is returned
    /// </summary>
    /// <param name="colliders"></param>
    /// <returns></returns>
    public static Bounds TotalColliderBounds(Collider[] colliders)
    {
        if (colliders == null || colliders.Length <= 0)
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

    public void KillPlayer(Player p)
    {
        p.Health.Damage(p.Health.max * 2);
    }
}
