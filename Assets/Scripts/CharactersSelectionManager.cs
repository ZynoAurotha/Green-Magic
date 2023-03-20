using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharactersSelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _transition2;
    [SerializeField] private GameObject _closeTransition;
    [SerializeField] private GameObject _charSelectionImage;
    [SerializeField] private GameObject _charSelectionAnimator;
    [SerializeField] private GameObject _charShowingSpaceImage;
    [SerializeField] private int _numberOfIconTableArray;
    [SerializeField] private Transform _tableTransform;
    [SerializeField] private GameObject _charSelec1;
    [SerializeField] private GameObject _charSelec2;

    void Start()
    {
        InitializeIconTable();
        OnDisableObject();
        _charSelec1.GetComponent<CharSelect1>().OnFreezyScript1 += OnDisableScript1;
        _charSelec2.GetComponent<CharSelect2>().OnFreezyScript2 += OnDisableScript2;
        Color c = _charShowingSpaceImage.GetComponent<Image>().color;
        c.a = 0;
        _charShowingSpaceImage.GetComponent<Image>().color = c;
        StartCoroutine(OnStartCoroutine(c));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PlayerInfoTable.getPlayer1() == null && PlayerInfoTable.getPlayer2() == null)
            {
                 SceneManager.LoadScene(0);   
            }else if(PlayerInfoTable.getPlayer1() != null)
            {
                _charSelec1.GetComponent<CharSelect1>().enabled = true;
                _charSelec1.transform.GetChild(0).gameObject.SetActive(true);
                _charSelec1.transform.GetChild(1).gameObject.SetActive(false);
                PlayerInfoTable.setPlayer1(null);     
            }else if(PlayerInfoTable.getPlayer2() != null)
            {
                _charSelec2.GetComponent<CharSelect2>().enabled = true;
                _charSelec2.transform.GetChild(0).gameObject.SetActive(true);
                _charSelec2.transform.GetChild(1).gameObject.SetActive(false);
                PlayerInfoTable.setPlayer2(null);
            }
        }
        if(PlayerInfoTable.getPlayer1() != null && PlayerInfoTable.getPlayer2() != null)
        {
            StartCoroutine(OnTransitionCoroutine());
        }
        Debug.Log("player1" + PlayerInfoTable.getPlayer1());
        // Debug.Log("player2" + PlayerInfoTable.getPlayer2());
    }

    void OnDisableScript1()
    {
        _charSelec1.GetComponent<CharSelect1>().enabled = false;
    }
    void OnDisableScript2()
    {
        _charSelec2.GetComponent<CharSelect2>().enabled = false;
    }

    IEnumerator OnTransitionCoroutine()
    {
        _closeTransition.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
    void OnDisableObject()
    {
        _tableTransform.gameObject.SetActive(false);
        _charSelectionImage.SetActive(false);
        _charSelectionAnimator.SetActive(false);
        _charShowingSpaceImage.SetActive(false);
        _charSelec1.SetActive(false);
        _charSelec2.SetActive(false);
    }

    void InitializeIconTable()
    {
        IconTable.setcharIconArray(_numberOfIconTableArray);
        IconTable.setInputTransform(_tableTransform);
        IconTable.AddGameobject();
    }

    IEnumerator OnStartCoroutine(Color c)
    {
        _transition2.SetActive(true);
        yield return new WaitForSeconds(1f);
        _transition2.SetActive(false);
        _charSelectionAnimator.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _charShowingSpaceImage.SetActive(true);
        while(_charShowingSpaceImage.GetComponent<Image>().color.a <= 1f)
        {
            c.a += 0.01f;
            _charShowingSpaceImage.GetComponent<Image>().color = c;
            yield return null;
        }
        _tableTransform.gameObject.SetActive(true);
        _charSelec1.SetActive(true);
        _charSelec2.SetActive(true);
    }
}
