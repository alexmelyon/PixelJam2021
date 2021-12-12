using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    public void CloseGates()
    {
        GetComponent<Animation>().Play();
        // GetComponent<Animator>().SetBool("GATES_OPENED", false);
    }

    public void OpenGates()
    {
        // GetComponent<Animator>().SetBool("GATES_OPENED", true);
    }
}
