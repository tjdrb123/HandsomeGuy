using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BrokenMachine : MonoBehaviour
{
    private InputAction inputAction;

    
    [SerializeField]Image repairBar;
    [SerializeField] GameObject progress;

    bool repair = false;

    private void Awake()
    {
        // InputAction 초기화
        inputAction = new InputAction(binding: "<Keyboard>/E");

        // '누르고 있을 때'에 대한 이벤트 설정
        inputAction.performed += ctx => {
            RepairToggle();
        };

        // '뗐을 때'에 대한 이벤트 설정
        inputAction.canceled += ctx => {
            RepairToggle();
        };

    }
    private void Start()
    {
        inputAction.Disable();
    }
    private void OnEnable()
    {
        // InputAction 활성화
        inputAction.Enable();
    }

    private void OnDisable()
    {
        // InputAction 비활성화
        inputAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(repair && repairBar.fillAmount < 1)
            Repair();
    }
    private void Repair()
    {
        repairBar.fillAmount += Time.deltaTime/10;
        progress.GetComponent<TMP_Text>().text = $"Progress : {(repairBar.fillAmount*100).ToString("N2")}%\r\nRefair - Press Hold E";
        if(repairBar.fillAmount == 1 ) progress.GetComponent<TMP_Text>().text = $"Progress :Complete";
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //플레이어 상호작용 키 이벤트에 연결 해제 RepairToggle()
            inputAction.Disable(); // InputAction 비활성화
            progress.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //플레이어 상호작용 키 이벤트에 연결 RepairToggle()
            inputAction.Enable(); // InputAction 활성화
            progress.SetActive(true);
        }
    }
    private void RepairToggle()
    {
        repair = !repair;
    }
}