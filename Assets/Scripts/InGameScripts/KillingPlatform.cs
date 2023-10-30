using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KillingPlatform : MonoBehaviour
{
     private float _moveTime;
    [SerializeField] private TypeSpeedSO typeSpeed;
    private Vector3 _moveDistance;
    private int _averageCountSymbolsInARow;
    public bool IsPlatformActiveOnThisLevel;
    public static Action PlatformKills;
    [SerializeField] private float _firstRowTimeModifier;

    private void OnEnable()
    {
        Controller.FirstBlockActivated += StartMovingPlatform;
    }
    private void OnDisable()
    {
        Controller.FirstBlockActivated -= StartMovingPlatform;
    }
    public void StartMovingPlatform()
    {
        
        StartCoroutine(ActivatePlatform());
    }
    public IEnumerator ActivatePlatform()
    {
        if (IsPlatformActiveOnThisLevel)
        {
            float MoveTimeToFirstRow = _moveTime * _firstRowTimeModifier;
            Vector3 MoveDistanceToFirstRow = _moveDistance *0.2f;
            StartCoroutine(FirstRowActivatePlatform(MoveTimeToFirstRow, MoveDistanceToFirstRow));
            yield return new WaitForSeconds(MoveTimeToFirstRow);
            Debug.Log("œÂ‚ÓÂ ÓÊË‰‡ÌËÂ Á‡‚Â¯ÂÌÓ");

            int i =0;
        while (true) {
            MovePlatform(_moveTime, _moveDistance);
            Debug.Log("Platform Moving times:" + i);
            i++;
            yield return new WaitForSeconds(_moveTime);
        }
        }

       
    }
    private void MovePlatform(float moveTime, Vector3 moveDistance )
    {
        Vector3 Destination = gameObject.transform.position - moveDistance;
        LeanTween.move(gameObject, Destination, moveTime);
    }
    public IEnumerator FirstRowActivatePlatform(float moveTime, Vector3 MoveDistance)
    {
        if (IsPlatformActiveOnThisLevel)
        {
                MovePlatform(moveTime, MoveDistance);
                Debug.Log("FirstRowActivatePlatform");
                yield return new WaitForSeconds(moveTime);
            }
        }


    public void SetMoveDistance(Vector3  distance)
    {
        _moveDistance = distance;
        _moveDistance = new Vector3(0,Creator.instance._spaceBetweenRows-0.3f); // ›››››››››››››››››››››››››› —œ≈–»Ã≈Õ“
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        Debug.Log("œ–Œ»√–¿ÕŒ");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
            PlatformKills.Invoke();
        }
    }
    public void SetPlatformSpeed(int speed, bool IsChallengeMode)
    {
        
        IsPlatformActiveOnThisLevel = true;
        if (IsChallengeMode == false || speed <=0) 
        {
            IsPlatformActiveOnThisLevel = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            return;
        }
        if(speed > 0)
        {
            int TextLength = Creator.instance.LevelText.Length;
            int RowsAtTheLevel = Creator.instance.CountOfRows;
            _averageCountSymbolsInARow = TextLength / RowsAtTheLevel;
            Debug.Log("_averageCountSymbolsInARow:" + _averageCountSymbolsInARow);


            float charbysecond = speed / 60;
            float  MovementTime = _averageCountSymbolsInARow / charbysecond;
            _moveTime = MovementTime;
        }
    }
}
