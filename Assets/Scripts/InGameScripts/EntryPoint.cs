using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] Creator creator;
    [SerializeField] Controller controller;
    [SerializeField] KillingPlatform platform;
    [SerializeField] GUIManager manager;
    [SerializeField] SettingsLoader settingsLoader;
    [SerializeField] YandexAdv yandexAdv;

    void Start()
    {

        if (yandexAdv != null)
        yandexAdv.ShowYandexAdv();

        Time.timeScale = 1f;
        settingsLoader.SetClientText();
        List<Block> ListOfBlocks = creator.CreateBlocks();
        Vector3 HeroAndBlocksHeight = controller.ActivateController(ListOfBlocks);
        platform.SetMoveDistance(HeroAndBlocksHeight);
        settingsLoader.SetPlatformSettings();
        manager.SetWindowWithLevelText();
    }

}
