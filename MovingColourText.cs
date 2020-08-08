using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingColourText : MonoBehaviour
{
    TextMeshProUGUI myText;


    public string currentText;
    private int currentStringId = 0;


    private float timer = 1;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        currentText = myText.text;

    }

    // Update is called once per frame
    void Update()
    {

        //MovingTextColour();
        if (timer > 1)
        {
            timer -= Time.deltaTime;
            return;
        }
        else
        {
            timer = 1.1f;
            myText.text = MovingTextColour(currentText, ref currentStringId);
        }
    }
    private string MovingTextColour(string _myText, ref int _currentId)
    {
        string newString = "";

        for (int i = 0; i < _myText.Length; i++)
        {
            if (_myText.Substring(i, 1) == " ")
            {
                newString += " ";
                continue;
            }
            if (i == _currentId)
            {
                newString += "<color=red>" + _myText.Substring(i, 1) + "</color>";
            }
            else
            {
                newString += _myText.Substring(i, 1);
            }
        }
        currentStringId++;
        if (_currentId > _myText.Length)
            _currentId = 0;

        return newString;
    }

}
