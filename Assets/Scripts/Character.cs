using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Sprite sprite;
    public bool isBought;
    public void Awake()
    {
        sprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
    }
}
