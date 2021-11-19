using RayFire;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolishhh : MonoBehaviour
{
    [SerializeField] bool i;
    [SerializeField] bool d;
    private RayfireRigid[] fires;

    private void Start()
    {
        fires = GetComponentsInChildren<RayfireRigid>();
    }

    private void Update()
    {

        if(i)
        {
            i = false;
            foreach (var item in fires)
            {
                item.Initialize();
            }
        }
        if (d)
        {
            d = false;
            foreach (var item in fires)
            {
                item.Demolish();
            }
        }
    }
}
