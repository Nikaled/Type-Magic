using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterNamePanel : MonoBehaviour
{
    [SerializeField] GameObject Background;
    [SerializeField] CanvasGroup _chapterPanel;
    [SerializeField] float _timeBeforeStartingDisappear;
    [SerializeField] float _timeToDisappear;
    void Start()
    {
        LeanTween.alphaCanvas(_chapterPanel, 1, _timeToDisappear);
        StartCoroutine(DisappearPanel());
    }

    private IEnumerator DisappearPanel()
    {
        yield return new WaitForSeconds(_timeBeforeStartingDisappear);
        Background.SetActive(false);
        LeanTween.alphaCanvas(_chapterPanel, 0, _timeToDisappear);
        yield return new WaitForSeconds(_timeToDisappear);
        _chapterPanel.gameObject.SetActive(false);
    }

}
