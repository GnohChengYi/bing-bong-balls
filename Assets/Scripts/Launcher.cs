using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
