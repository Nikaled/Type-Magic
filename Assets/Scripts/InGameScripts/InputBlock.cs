using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputBlock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextInBlock;
 
    void Update()
    {
        if (Input.inputString != string.Empty) { 
            TextInBlock.text = Input.inputString[0].ToString();
        }
    }
}
