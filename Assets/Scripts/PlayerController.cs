using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int _iyiToplanabilirDeger;

    [SerializeField] private int _kötüToplanabilirDeger;

    [SerializeField] private GameObject _karakterPaketi;

    private int _elmasSayisi;

    private GameObject _player;

    private UIController _uiController;

    private int _toplananElmasSayisi;



    void Start()
    {
        LevelStart();

        _uiController = GameObject.Find("UIController").GetComponent<UIController>();

    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Elmas")
        {
            _elmasSayisi += 1;
            _toplananElmasSayisi += 1;
            PlayerPrefs.SetInt("ElmasSayisi", _elmasSayisi);
            Destroy(other.gameObject);
        }
        else
        {

        }
    }

    private void WinScreenAc()
    {
        _uiController.WinScreenPanelOpen();
    }

    private void LoseScreenAc()
    {
        _uiController.LoseScreenPanelOpen();
    }




    public void LevelStart()
    {
        _toplananElmasSayisi = 1;
        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 1, 0);

    }

    void test()
    {
        int k = 3;

        int leftSideSuccessCount = 0, rightSideSuccessCount = 0;

        bool leftSideSuccess = false, rightSideSuccess = false;
        List<int> days = new List<int>();

        List<int> answers = new List<int>();

        for (int i = 0; i < days.Count; i++)
        {
            for (int j = i - k; j < i; j++)
            {
                if (j < days.Count)
                    break;

                if (days[j] >= days[j + 1])
                {
                    leftSideSuccessCount++;
                }
                if (leftSideSuccessCount == k)
                {
                    leftSideSuccess = true;
                }
            }

            for (int j = i; j < i + k; j++)
            {
                if (j < days.Count)
                    break;
                if (days[j] <= days[j + 1])
                {
                    rightSideSuccessCount++;
                }
                if (leftSideSuccessCount == k)
                {
                    leftSideSuccess = true;
                }
            }

            if (leftSideSuccess && rightSideSuccess)
            {
                answers.Add(days[i]);
            }

            leftSideSuccessCount = 0;
            rightSideSuccessCount = 0;
            leftSideSuccess = false;
            rightSideSuccess = false;
        }

    }

}
