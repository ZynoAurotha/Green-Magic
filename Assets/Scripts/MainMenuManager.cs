using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject _transition1;
    [SerializeField] private GameObject[] _buttonAnimator;
    [SerializeField] private GameObject[] _buttonImage;
    [SerializeField] private GameObject _fadingTransition;
    [SerializeField] private GameObject _helpBoard;
    [SerializeField] private AudioSource _moveSound;
    [SerializeField] private AudioSource _selectSound;
    [SerializeField] private AudioSource _openningHelpSound;
    [SerializeField] private AudioSource _playSound;
    private int _index = 0;
    private int _previousIndex = 0;
    private bool _upKeyPressed;
    private bool _downKeyPressed;
    private bool _helpPanelIsOpenned;

    public void Start()
    {
        _fadingTransition.SetActive(true);
        Color c = _fadingTransition.GetComponent<Image>().color;
        c.a = 0;
        _fadingTransition.GetComponent<Image>().color = c;
    }

    public void Update()
    {
        _upKeyPressed = false;
        _downKeyPressed = false;

        if(Input.GetKeyDown(KeyCode.S) && !_helpPanelIsOpenned)
        {
            _downKeyPressed = true;
            _index++;
            _previousIndex =_index - 1;
            if(_index > (_buttonAnimator.Length - 1))
            {
                _index = 0;
            }
        }
        else if(Input.GetKeyDown(KeyCode.W) && !_helpPanelIsOpenned)
        {
            _upKeyPressed = true;
            _index--;
            _previousIndex =_index + 1;
            if(_index < 0)
            {
                _index = _buttonAnimator.Length - 1;
            }
        }

        _buttonAnimator[_index].GetComponent<Animator>().enabled = true;
        _buttonImage[_index].GetComponent<Image>().enabled = false;

        // if(_index == _previousIndex)
        // {
        //     _buttonAnimator[_index + 1].GetComponent<Animator>().enabled = false;
        //     _buttonImage[_index + 1].GetComponent<Image>().enabled = true;
        // }

        if(_downKeyPressed)
        {
            _buttonAnimator[_previousIndex].GetComponent<Animator>().enabled = false;
            _buttonImage[_previousIndex].GetComponent<Image>().enabled = true;
            _moveSound.Play();

        } else if(_upKeyPressed)
        {
            _buttonAnimator[_previousIndex].GetComponent<Animator>().enabled = false;
            _buttonImage[_previousIndex].GetComponent<Image>().enabled = true;
            _moveSound.Play();
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            if(_buttonImage[_index].name == "PVSPButtonImage")
            {
                _selectSound.Play();
                _playSound.Play();
                OnPlay();
            }else if(_buttonImage[_index].name == "PVSBButtonImage")
            {
                _selectSound.Play();
                _playSound.Play();
                OnPlay();
            }else if(_buttonImage[_index].name == "HelpButtonImage" && !_helpPanelIsOpenned)
            {
                _selectSound.Play();
                _helpPanelIsOpenned = true;
                OnOpenHelp(); 
            }
            else if(_buttonImage[_index].name == "ExitButtonImage")
            {
                _selectSound.Play();
                Application.Quit();
            }
        }

        if(Time.timeScale == 0 && _helpPanelIsOpenned)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                _helpBoard.SetActive(false);
                _helpPanelIsOpenned = false;
                Time.timeScale = 1;
                _openningHelpSound.Play();
            }
        }
    }

    public void OnOpenHelp()
    {
        _helpBoard.SetActive(true);
        Time.timeScale = 0; 
        _openningHelpSound.Play();  
    }

    public void OnPlay()
    {
        StartCoroutine(OnPlayCoroutine());
    }

    IEnumerator OnPlayCoroutine()
    {
        _transition1.SetActive(true);
        Animator tranAnimator = _transition1.GetComponent<Animator>();
        tranAnimator.SetTrigger("transition1");
        while(_fadingTransition.GetComponent<Image>().color.a <= 1f)
        {
            Color c = _fadingTransition.GetComponent<Image>().color;
            c.a += 0.01f;
            _fadingTransition.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        
        if(_buttonImage[_index].name == "PVSPButtonImage")
        {
            SceneManager.LoadScene(0);
            //Debug.Log("load");
        }else if(_buttonImage[_index].name == "PVSBButtonImage")
        {
            SceneManager.LoadScene(1); 
        }
       
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
