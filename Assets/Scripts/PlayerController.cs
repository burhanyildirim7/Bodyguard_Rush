using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        GameObject mainCharacter = GameObject.FindGameObjectWithTag("BizimKarakter");

        PlayerManager.BodyguardPositionList.Clear();
        PlayerManager.Bodyguards.Clear();

        foreach (var item in GameObject.FindGameObjectsWithTag("CalmBodyguard").ToList())
        {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("SadHayran").ToList())
        {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Bodyguard").ToList())
        {
            Destroy(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("SadPaparazi").ToList())
        {
            Destroy(item);
        }
        GameObject.FindGameObjectsWithTag("CalmBodyguard").ToList().Clear();
        GameObject.FindGameObjectsWithTag("SadHayran").ToList().Clear();
        GameObject.FindGameObjectsWithTag("SadPaparazi").ToList().Clear();
        GameObject.FindGameObjectsWithTag("Bodyguard").ToList().Clear();

        _toplananElmasSayisi = 1;
        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayisi");
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 0, 0);

        mainCharacter.GetComponent<Animator>().SetBool("isRun", false);
        mainCharacter.GetComponent<Animator>().SetBool("isCry", false);
        mainCharacter.GetComponent<Animator>().SetBool("isDance", false);
        mainCharacter.GetComponent<Animator>().SetBool("isFall", false);
        mainCharacter.GetComponent<Animator>().SetBool("isIdle", true);

        mainCharacter.transform.localPosition = new Vector3(0, 0, 0);

        mainCharacter.GetComponent<PlayerManager>().fxHavaiFisek1.SetActive(false);
        mainCharacter.GetComponent<PlayerManager>().fxHavaiFisek2.SetActive(false);

        PlayerManager.rowChecker_TriggerEnter = 0;
        PlayerManager.rowCounter_TriggerEnter = 1;

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().Player = _player;


    }

}
