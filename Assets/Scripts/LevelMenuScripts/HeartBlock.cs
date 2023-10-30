using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartBlock : MonoBehaviour
{
    [SerializeField] GameObject _heartTextObject;
    [SerializeField] private TextMeshPro _heartText;
    [SerializeField] string _heartReplica1;
    [SerializeField] string _heartReplica2;
    [SerializeField] string _heartReplica3;
    [SerializeField] string _heartReplica4;
    [SerializeField] string _heartReplica5;
    private List<string> _heartReplics = new();
    private int _replicaCurrentIndex = 0;
    void Start()
    {
        _heartReplics = new List<string>() { _heartReplica1, _heartReplica2, _heartReplica3, _heartReplica4, _heartReplica5 };
        Debug.Log(_heartReplics.Count);
    }
    private void OnMouseDown()
    {
        StopAllCoroutines();
        StartCoroutine(ShowText());
    }
    IEnumerator ShowText()
    {

        _heartText.text = _heartReplics[_replicaCurrentIndex];
        _replicaCurrentIndex++;
        if (_replicaCurrentIndex >= _heartReplics.Count)
            _replicaCurrentIndex = 0;

        _heartTextObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _heartTextObject.SetActive(false);
    }
}
