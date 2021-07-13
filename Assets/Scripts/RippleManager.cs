using System.Collections.Generic;
using UnityEngine;

public class RippleManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ripplePrefab;

    public static List<GameObject> completedRipples;

    private void Start()
    {
        completedRipples = new List<GameObject>();
    }

    public static void CreateRippleAt(ContactPoint2D contact)
    {
        Vector2 point = contact.point;
        Vector3 position = new Vector3(point.x, point.y, 0);
        if (completedRipples.Count > 0)
        {
            GameObject ripple = completedRipples[0];
            completedRipples.RemoveAt(0);
            ripple.transform.position = position;
            ParticleSystem particleSystem = ripple.GetComponent<ParticleSystem>();
            particleSystem.Clear();
            particleSystem.Play();
        }
        else
        {
            GameObject ripple = (GameObject)
                Instantiate(Instance.ripplePrefab, position, Quaternion.identity);
        }
    }

    private static RippleManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
