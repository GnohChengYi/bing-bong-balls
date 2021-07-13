using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem rippleSystem;

    private bool inCompletedList;

    private void Update()
    {
        if (rippleSystem.IsAlive())
        {
            // trust that RippleManager removed this from completed list
            inCompletedList = false;
        }
        else if (!inCompletedList)
        {
            RippleManager.completedRipples.Add(gameObject);
            inCompletedList = true;
        }
    }
}
