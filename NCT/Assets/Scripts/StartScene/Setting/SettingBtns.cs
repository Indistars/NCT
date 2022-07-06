using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtns : MonoBehaviour
{
    public GameObject SoundControll;
    public GameObject ControllKey;
    public GameObject Language;
    public GameObject Credit;


    public void OpenSoundBtn()
    {
        SoundControll.SetActive(true);
    }

    public void CloseSoundBtn()
    {
        SoundControll.SetActive(false);
    }

    public void OpenControllKeyBtn()
    {
        ControllKey.SetActive(true);
    }

    public void CloseControllKeyBtn()
    {
        ControllKey.SetActive(false);
    }

    public void OpenLanguageBtn()
    {
        Language.SetActive(true);
    }

    public void CloseLanguageBtn()
    {
        Language.SetActive(false);
    }

    public void OpenCreditBtn()
    {
        Credit.SetActive(true);
    }

    public void CloseCreditBtn()
    {
        Credit.SetActive(false);
    }
}
