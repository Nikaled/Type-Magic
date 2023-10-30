using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _timeToPassBlock;
    [SerializeField] Animator animator;
    public List<IEnumerator> MovingCoroutines= new();
    public Vector3 lastBlockCoordinate;
    private void Update()
    {
        if (gameObject.transform.position == lastBlockCoordinate)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public  IEnumerator Move2(Vector3 blockPosition)
    {
        lastBlockCoordinate = blockPosition;
        animator.SetBool("IsMoving", true);
        MovingCoroutines.Add(MoveToCurrentBlock(blockPosition));
        //yield return new WaitForSeconds(_timeToPassBlock * MovingCoroutines.Count);
        //yield return StartCoroutine(MoveToCurrentBlock(blockPosition));
        //yield return StartCoroutine(MovingCoroutines[0]);
        yield return StartCoroutine(MoveToCurrentBlock(blockPosition));
        MovingCoroutines.RemoveAt(0);
    }
    public IEnumerator MoveToCurrentBlock( Vector3 blockPosition)
    {
        LeanTween.move(gameObject, blockPosition, _timeToPassBlock); 
        yield return null;

    }
}