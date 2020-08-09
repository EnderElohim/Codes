using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingColourText : MonoBehaviour
{
   public TextColour textColour;
    private TextMeshProUGUI textDisplayer;
    private string currentText;
    private int currentStringId = 0;
    private float timer = 1;

    void Start()
    {
        textDisplayer = GetComponent<TextMeshProUGUI>();
        currentText = textDisplayer.text;
    }
    
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
            textDisplayer.text = MovingTextColour(currentText, ref currentStringId);
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
                newString += "<color="+ textColour.ToString() + ">" + _myText.Substring(i, 1) + "</color>";
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
    
    public enum TextColour
    {
        black, 
        blue, 
        green, 
        orange, 
        purple, 
        red, 
        white,
        yellow
    }
}
