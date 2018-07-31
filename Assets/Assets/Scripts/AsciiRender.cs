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
    private string preAsciiText;
    private string postAsciiText;
    private char[] asciiCharArray;
    private char[] greyscaleAscii = new char[] { '$', '@', 'B', '%', '8', '&', 'W', 'M', '#', '*', 'o', 'a', 'h', 'k', 'b', 'd', 'p', 'q', 'w', 'm', 'Z', 'O', '0', 'Q', 'L', 'C', 'J', 'U', 'Y', 'X', 'z', 'c', 'v', 'u', 'n', 'x', 'r', 'j', 'f', 't', '/', '\\', '|', '(', ')', '1', '{', '}', '[', ']', '?', '-', '_', '+', '~', '<', '>', 'i', '!', 'l', 'I', ';', ':', ',', '"', '^', '`', '\'', '.', ' ' };

    private void Start()
    {
        RectTransform transform = renderText.GetComponent<RectTransform>();
        transform.sizeDelta = new Vector2(Screen.width, Screen.height);

        preAsciiText = "<mspace=6><line-height=6>";
        postAsciiText = "</mspace>";
        
        charHeight = (int)(Screen.height / 6);
        charWidth = (int)(Screen.width / 6);

        renderTexture.height = charHeight;
        renderTexture.width = charWidth;

        arraySize = ((charWidth + 1) * charHeight) + preAsciiText.Length + postAsciiText.Length;
        asciiCharArray = new char[arraySize];

        for (int i = 0; i < preAsciiText.Length; i++)
        {
            asciiCharArray[i] = preAsciiText[i];
        }

        for (int i = 1; i < 10; i++)
        {
            asciiCharArray[arraySize - postAsciiText.Length + 1 - i] = postAsciiText[postAsciiText.Length - i];
        }
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
                int charSpace = preAsciiText.Length + y + x + (charWidth * y);
                asciiCharArray[charSpace] = getGreyscaleChar(Convert.ToDouble(renderGrid[x + (charWidth * (charHeight - 1 - y))].grayscale));
            }
            asciiCharArray[preAsciiText.Length + charWidth + ((charWidth + 1) * y)] = '\n';
        }
        string output = new string(asciiCharArray);
        renderText.text = output;
    }

    public char getGreyscaleChar(double hue)
    {
        return greyscaleAscii[(greyscaleAscii.Length - 1) - Convert.ToInt32(Math.Ceiling(hue * (greyscaleAscii.Length - 1)))];
    }
}
