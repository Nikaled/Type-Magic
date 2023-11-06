using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class Block : MonoBehaviour
{
    public TextMeshPro BlockText;
    [SerializeField] char symbol;
    public SpriteRenderer render;
    public bool isDone = false;
    public bool IsLastBlockInRow;
    public static float BlocksDoneInTheRow=0;
    [SerializeField] AudioSource _keySound;

    void Update()
    {

        if (Input.inputString.Contains(symbol) && Controller.IsGameActive)
        {
            BlockActivated();
        }
    }
    public void BlockActivated()
    {
        render.color = Color.green;
        isDone = true;
        BlocksDoneInTheRow++;
        _keySound.pitch = Mathf.Lerp(0.85f, 1.15f, BlocksDoneInTheRow / Creator.instance.RowLenght);
        _keySound.panStereo = Mathf.Lerp(-1f, 1f, BlocksDoneInTheRow / Creator.instance.RowLenght);
        _keySound.PlayOneShot(_keySound.clip);
        Controller.instance.BlockPressed();
        if (IsLastBlockInRow)
        {
            BlocksDoneInTheRow = 0;
        }
    }
    public void SetupSymbol(char symbol)
    {
        this.symbol = symbol;
        BlockText.text = symbol.ToString();
        if(BlockText.text == " ")
        {
            BlockText.text = " ";
        }
    }
}
