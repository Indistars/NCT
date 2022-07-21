using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;

public class OrderPanelUI : MonoBehaviour
{
    [SerializeField] private Text animalNameText; // 동물 이름 텍스트
    [SerializeField] private Image[] likeIngredients; // 좋아하는 식재료
    [SerializeField] private Image[] disLikeIngredients; // 싫어하는 식재료

    TdAnimal tdAnimal;

    private void OnEnable()
    {
        tdAnimal = DataBaseManager.Instance.tdAnimalDict[10001]; // 동물 초기화 ※ 추가 예정 ※
        //for (int i = 0; i < tdAnimal.Prefer_Food.Count; i++)
        //{
        //}
    }
    
    public void OnAcceptButton()
    {
        gameObject.SetActive(false);
    }

    public void OnRefuseButton()
    {
        gameObject.SetActive(false);
    }
}
