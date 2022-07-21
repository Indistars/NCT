using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBase;

public class OrderPanelUI : MonoBehaviour
{
    [SerializeField] private Text animalNameText; // ���� �̸� �ؽ�Ʈ
    [SerializeField] private Image[] likeIngredients; // �����ϴ� �����
    [SerializeField] private Image[] disLikeIngredients; // �Ⱦ��ϴ� �����

    TdAnimal tdAnimal;

    private void OnEnable()
    {
        tdAnimal = DataBaseManager.Instance.tdAnimalDict[10001]; // ���� �ʱ�ȭ �� �߰� ���� ��
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
