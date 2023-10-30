using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class Creator : MonoBehaviour
{
    [SerializeField] GameObject _exampleBlock;
    [SerializeField] Controller controller;
    public int RowLenght;
    [HideInInspector] public GameObject[] BlocksInARow;
    public TextFragments textfragments;
    [HideInInspector] public int CountOfRows;
    public float _spaceBetweenRows;
    public string LevelText { get; private set; }

    public static Creator instance;
    private void Awake()
    {
        instance = this;

    }
    public List<Block> CreateBlocks()
    {
         LevelText = textfragments.FragmentOfText1 + String.Empty;
        List<String> StreamAsListOfStrings = DivideStreamIntoStrings(LevelText, RowLenght);
        CountOfRows = StreamAsListOfStrings.Count;

        CreateZeroBlock();
        List<Block> ListOfBlocks = new List<Block>();
             Vector3 FirstBlockPosition = new(transform.position.x, transform.position.y);
        for (int i = 0; i < StreamAsListOfStrings.Count; i++)
        {
           ListOfBlocks.AddRange(CreateRow(StreamAsListOfStrings[i], FirstBlockPosition));
            FirstBlockPosition = new Vector3(transform.position.x,
                ListOfBlocks[^1].transform.position.y - _spaceBetweenRows);
        }
        Debug.Log("Количество объектов в массиве" + ListOfBlocks.Count);
        return ListOfBlocks;

    }
    private void CreateZeroBlock()
    {
        var SpriteWide = new Vector3(_exampleBlock.GetComponent<Block>().render.bounds.size.x, 0);
        GameObject ZeroBlock = Instantiate(_exampleBlock, gameObject.transform.position - SpriteWide, transform.rotation);
        ZeroBlock.GetComponent<Block>().render.color = Color.green;
    }
    private List<String> DivideStreamIntoStrings(string stream, int RowLenght)
    {
        int CurrentIndexInStream = 0;
        List<String> StreamAsRowStrings = new List<string>();
        for (int i = 0; CurrentIndexInStream < stream.Length; i++)
        {
            string NextStringFirstSymbol = "ф";
            if (CurrentIndexInStream + RowLenght < stream.Length) 
            { 
                NextStringFirstSymbol = stream[CurrentIndexInStream + RowLenght].ToString();
            }
            string RowString = CutStreamToRow(stream.Substring(CurrentIndexInStream), RowLenght, NextStringFirstSymbol);
            CurrentIndexInStream += RowString.Length;
            StreamAsRowStrings.Add(RowString);

            //Debug.Log(StreamAsRowStrings[i]);
          
        }

        return StreamAsRowStrings;
    }
    private string CutStreamToRow(string stream, int RowLength, string NextStringFirstSymbol)
    {
        string WordsInARow;
        string space = " ";
        if (stream.Length < RowLength)
            return stream;
        else
            WordsInARow = stream.Substring(0, RowLength);
        if (stream[^1].Equals(space) || stream[^1].Equals(String.Empty) || NextStringFirstSymbol.Equals(space))
        {
            return WordsInARow;
        }
        int LastWordInARowIndex = WordsInARow.LastIndexOf(space);
            if (LastWordInARowIndex != -1)
        {
            Debug.Log("LastWordInARowIndex" + LastWordInARowIndex);
        string MainPartOfStream = stream.Substring(0, LastWordInARowIndex);
        return MainPartOfStream;

        }
            Debug.Log("Строка длиннее 17 символов");
            return WordsInARow;
    }
    private List<Block> CreateRow(string stream, Vector3 StartPosition)
    {
        List<GameObject> GameObjList = new();
         List<Block> BlocksList = new();
        var SpriteWide = new Vector3(_exampleBlock.GetComponent<Block>().render.bounds.size.x, 0);
        Vector3 StartPos = new Vector3(transform.position.x, StartPosition.y);
        for (int i = 0; i < stream.Length; i++)
        {
            GameObjList.Add(Instantiate(_exampleBlock, StartPos + SpriteWide*i, transform.rotation));
            GameObjList[i].transform.SetParent(this.transform);
            GameObjList[i].transform.name = "Block#" + i;

            BlocksList.Add(GameObjList[i].GetComponent<Block>());
            BlocksList[i].SetupSymbol(stream[i]);
        }
        //BlocksList[^1].render.color = Color.magenta;
        BlocksList[^1].IsLastBlockInRow = true;
        return BlocksList;
    }
}
