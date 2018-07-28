using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class AsciiRender : MonoBehaviour
{
    public RenderTexture renderTexture;
    public TextMeshProUGUI renderText;
    private int charWidth;
    private int charHeight;
    private int arraySize;
    private char[] asciiCharArray;
    private char[] greyscaleAscii = new char[] { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };

    private void Start()
    {
        charHeight = Screen.height / 6;
        charWidth = Screen.width / 6;
        arraySize = ((charWidth + 1) * charHeight) + 34;
        asciiCharArray = new char[arraySize];
        asciiCharArray[0] = '<';
        asciiCharArray[1] = 'm';
        asciiCharArray[2] = 's';
        asciiCharArray[3] = 'p';
        asciiCharArray[4] = 'a';
        asciiCharArray[5] = 'c';
        asciiCharArray[6] = 'e';
        asciiCharArray[7] = '=';
        asciiCharArray[8] = '6';
        asciiCharArray[9] = '>';
        asciiCharArray[10] = '<';
        asciiCharArray[11] = 'l';
        asciiCharArray[12] = 'i';
        asciiCharArray[13] = 'n';
        asciiCharArray[14] = 'e';
        asciiCharArray[15] = '-';
        asciiCharArray[16] = 'h';
        asciiCharArray[17] = 'e';
        asciiCharArray[18] = 'i';
        asciiCharArray[19] = 'g';
        asciiCharArray[20] = 'h';
        asciiCharArray[21] = 't';
        asciiCharArray[22] = '=';
        asciiCharArray[23] = '6';
        asciiCharArray[24] = '>';
        asciiCharArray[arraySize - 9] = '<';
        asciiCharArray[arraySize - 8] = '/';
        asciiCharArray[arraySize - 7] = 'm';
        asciiCharArray[arraySize - 6] = 's';
        asciiCharArray[arraySize - 5] = 'p';
        asciiCharArray[arraySize - 4] = 'a';
        asciiCharArray[arraySize - 3] = 'c';
        asciiCharArray[arraySize - 2] = 'e';
        asciiCharArray[arraySize - 1] = '>';
    }

    void Update()
    {
        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();

        Color[] renderGrid = tex2d.GetPixels(0, 0, charWidth, charHeight);

        for (int y = 0; y < charHeight; y++)
        {
            for (int x = 0; x < charWidth; x++)
            {
                int charSpace = 25 + (1 * y) + x + (charWidth * y);
                asciiCharArray[charSpace] = getGreyscaleChar(Convert.ToDouble(renderGrid[x + (charWidth * (charHeight - 1 - y))].grayscale));
            }
            asciiCharArray[25 + charWidth + ((charWidth + 1) * y)] = '\n';
        }
        string output = new string(asciiCharArray);
        renderText.text = output;
    }

    public char getGreyscaleChar(double hue)
    {
        return greyscaleAscii[(greyscaleAscii.Length - 1) - Convert.ToInt32(Math.Ceiling(hue * (greyscaleAscii.Length - 1)))];
    }
}
