using UnityEngine;
using System.Collections;

public class TypeKey : MonoBehaviour
{

    public bool isBigChar = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkTypeInput();
    }

    private void checkTypeInput()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Keyboard.curSentence.Length <= 0)
            {
                return;
            }
            Keyboard.curSentence = Keyboard.curSentence.Remove(Keyboard.curSentence.Length - 1);
        }
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            isBigChar = !isBigChar;
        }
        if (Keyboard.curSentence.Length >= 20)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "A";
            }
            else
            {
                Keyboard.curSentence += "a";
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "B";
            }
            else
            {
                Keyboard.curSentence += "b";
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "C";
            }
            else
            {
                Keyboard.curSentence += "c";
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "D";
            }
            else
            {
                Keyboard.curSentence += "d";
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "E";
            }
            else
            {
                Keyboard.curSentence += "e";
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "F";
            }
            else
            {
                Keyboard.curSentence += "f";
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "G";
            }
            else
            {
                Keyboard.curSentence += "g";
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "H";
            }
            else
            {
                Keyboard.curSentence += "h";
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "I";
            }
            else
            {
                Keyboard.curSentence += "i";
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "J";
            }
            else
            {
                Keyboard.curSentence += "j";
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "K";
            }
            else
            {
                Keyboard.curSentence += "k";
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "L";
            }
            else
            {
                Keyboard.curSentence += "l";
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "M";
            }
            else
            {
                Keyboard.curSentence += "m";
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "N";
            }
            else
            {
                Keyboard.curSentence += "n";
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "O";
            }
            else
            {
                Keyboard.curSentence += "o";
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "P";
            }
            else
            {
                Keyboard.curSentence += "p";
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "Q";
            }
            else
            {
                Keyboard.curSentence += "q";
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "R";
            }
            else
            {
                Keyboard.curSentence += "r";
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "S";
            }
            else
            {
                Keyboard.curSentence += "s";
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "T";
            }
            else
            {
                Keyboard.curSentence += "t";
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "U";
            }
            else
            {
                Keyboard.curSentence += "u";
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "V";
            }
            else
            {
                Keyboard.curSentence += "v";
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "W";
            }
            else
            {
                Keyboard.curSentence += "w";
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "X";
            }
            else
            {
                Keyboard.curSentence += "x";
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "Y";
            }
            else
            {
                Keyboard.curSentence += "y";
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isBigChar)
            {
                Keyboard.curSentence += "Z";
            }
            else
            {
                Keyboard.curSentence += "z";
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Keyboard.curSentence += "0";
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Keyboard.curSentence += "1";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Keyboard.curSentence += "2";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Keyboard.curSentence += "3";
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Keyboard.curSentence += "4";
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Keyboard.curSentence += "5";
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Keyboard.curSentence += "6";
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Keyboard.curSentence += "7";
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Keyboard.curSentence += "8";
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Keyboard.curSentence += "9";
        }
    }
}
