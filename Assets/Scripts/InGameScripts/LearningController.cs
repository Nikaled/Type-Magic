using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearningController : Controller
{
    private int _firstSpaceIndex=7;
    private int _firstCapitalLetterIndex=8;
    [SerializeField] private GameObject _firstWordPanel;
    [SerializeField] private GameObject _firstSpacePanel;
    [SerializeField] private GameObject _firstCapitalLetterPanel;
    [SerializeField] private GameObject _showTextPanel;
    private void Start()
    {
        StopGame(); // Игра будет начата после нажатия кнопки в меню обучения
    }
    public override void BlockPressed()
    {
        base.BlockPressed();
        if (CurrentIndex == 1)
        {
            _firstWordPanel.SetActive(false);
        }
        if (CurrentIndex == _firstSpaceIndex)
        {
            _firstSpacePanel.SetActive(true);
        }
        if(CurrentIndex == _firstSpaceIndex + 1)
        {
            _firstSpacePanel.SetActive(false);
        }
        if(CurrentIndex == _firstCapitalLetterIndex)
        {
            _firstCapitalLetterPanel.SetActive(true);
        }
        if(CurrentIndex == _firstCapitalLetterIndex + 1)
        {
            _firstCapitalLetterPanel.SetActive(false);
        }
    }
    protected override void OnCompleted()
    {
        base.OnCompleted();
        _showTextPanel.SetActive(true);
    }
}
