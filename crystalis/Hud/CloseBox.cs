using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBox : MonoBehaviour
{
    public void Close () {
        gameObject.SetActive(false);
    }
}
