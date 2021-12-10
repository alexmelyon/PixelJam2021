using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public void CloseGates()
    {
        GetComponent<Animation>().Play();
    }
}
