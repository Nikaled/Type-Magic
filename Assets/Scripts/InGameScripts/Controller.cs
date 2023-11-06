using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    public static Controller instance;
    public Creator creator;
    public List<Block> WrittenBlocks { get; private set; }
    private Block currentBlock;
    public int CurrentIndex { get; private set; }
    [SerializeField] Character character;
    private Vector3 BlockAndCharacterHeight;
    public static bool IsGameActive=true;

    public static Action OnCompletedAction;
    public static Action GameOverAction;
    public static Action FirstBlockActivated;

    private void Awake()
    {
        instance = this;
        ResumeGame();
    }
    private void OnEnable()
    {
        KillingPlatform.PlatformKills += LevelFailed;
    }
    private void OnDisable()
    {
        KillingPlatform.PlatformKills -= LevelFailed;
    }
    public virtual void BlockPressed()
    {
        if (currentBlock.isDone && CurrentIndex < WrittenBlocks.Count)
        {
            CurrentBlockChange(currentBlock);
            var SpaceToMoving = currentBlock.transform.position + BlockAndCharacterHeight;
            StartCoroutine(character.Move2(SpaceToMoving));
            CurrentIndex++;
            if (currentBlock.IsLastBlockInRow && CurrentIndex > creator.RowLenght)
                MoveCameraDown();
            if (CurrentIndex >= WrittenBlocks.Count) 
            {
                OnCompleted();
                return;
            }
            currentBlock = WrittenBlocks[CurrentIndex];
            NextBlockChange(currentBlock);
            if(CurrentIndex == 1)
            {
                FirstBlockActivated.Invoke();
            }
        }
    }
    public void MoveCameraDown()
    {
       var NewPosition =camera.transform.position - new Vector3(0, creator._spaceBetweenRows);
        LeanTween.move(camera.gameObject, NewPosition, 0.2f);
    }
    public Vector3 ActivateController(List<Block> blocks)
    {
        WrittenBlocks = blocks;
        for (int i = 0; i < WrittenBlocks.Count; i++)
        {

            WrittenBlocks[i].enabled = false;
        }
        WrittenBlocks[0].enabled = true;
        WrittenBlocks[0].render.color = Color.gray;
        currentBlock = WrittenBlocks[0];
        float blockHeight = currentBlock.render.bounds.size.y;
        float blockWidth = currentBlock.render.bounds.size.x;
        character.transform.position = currentBlock.transform.position + new Vector3(-blockWidth, blockHeight*1.7f);
        BlockAndCharacterHeight =new Vector3(0, blockHeight * 1.75f);
        return BlockAndCharacterHeight;
    }
    private void CurrentBlockChange(Block block)
    {
        block.render.color = Color.green;
        block.enabled = false;
    }
    private void NextBlockChange(Block block)
    {
        block.enabled = true;
        block.render.color = Color.gray;
    }
    public static void StopGame()
    {
        Time.timeScale = 0f;
        IsGameActive = false;

    }
    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        IsGameActive = true;
    }
    public void StopGameButton()
    {
        StopGame();
    }
    public void ResumeGameButton()
    {
        ResumeGame();
    }
    private void LevelFailed()
    {
        StopGame();
    }
    protected virtual void OnCompleted()
    {
        StopGame();
        OnCompletedAction.Invoke();
        GameManager.instance.progress.CurrentLevelName = SceneManager.GetActiveScene().name;
    }
}
