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
    private char[] asciiCharArray;
    private char[] greyscaleAscii = new char[] { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };

    private void Start()
    {
        asciiCharArray = new char[57814];
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
        asciiCharArray[57805] = '<';
        asciiCharArray[57806] = '/';
        asciiCharArray[57807] = 'm';
        asciiCharArray[57808] = 's';
        asciiCharArray[57809] = 'p';
        asciiCharArray[57810] = 'a';
        asciiCharArray[57811] = 'c';
        asciiCharArray[57812] = 'e';
        asciiCharArray[57813] = '>';
    }

    void Update()
    {
        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();

        Color[] renderGrid = tex2d.GetPixels(0, 0, 320, 180);

        for (int y = 0; y < 180; y++)
        {
            for (int x = 0; x < 320; x++)
            {
                int charSpace = 25 + (1 * y) + x + (320 * y);
                asciiCharArray[charSpace] = getGreyscaleChar(Convert.ToDouble(renderGrid[x + (320 * (179 - y))].grayscale));
            }
            asciiCharArray[25 + 320 + (321 * y)] = '\n';
        }
        string output = new string(asciiCharArray);
        renderText.text = output;
    }

    public char getGreyscaleChar(double hue)
    {
        int charPos = Convert.ToInt32(Math.Ceiling(hue * (greyscaleAscii.Length - 1)));
        return greyscaleAscii[(greyscaleAscii.Length - 1) - charPos];
    }
}
