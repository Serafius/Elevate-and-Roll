using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ColorPickerControl : MonoBehaviour
{
    public float currentHue, currentSat, currentVal;

    [SerializeField]
    private RawImage hueImage, satImage, outputImage;

    [SerializeField]
    private Slider hueSlider;

    [SerializeField]
    private TMP_InputField hexInputField;

    private Texture2D hueTexture, svTexture, outputTexture;

    [SerializeField]
    MeshRenderer changeThisColor;

    private void Start()
    {
        currentHue = 0;
        currentSat = 1;  // Start with full saturation
        currentVal = 1;  // Start with full value
        
        CreateHueImage();
        CreateSVImage();
        CreateOutputImage();

        UpdateOutputImage();
    }

    private void CreateHueImage()
    {
        hueTexture = new Texture2D(256, 1);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "Hue Texture";

        for (int x = 0; x < hueTexture.width; x++)
        {
            hueTexture.SetPixel(x, 0, Color.HSVToRGB((float)x / hueTexture.width, 1, 1));
        }

        hueTexture.Apply();
        hueImage.texture = hueTexture;
    }

    private void CreateSVImage()
    {
        svTexture = new Texture2D(16, 16);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SatValTexture";

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                // Map x to value (brightness) and y to saturation
                float value = (float)x / svTexture.width;
                float saturation = 1 - ((float)y / svTexture.height);
                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, saturation, value));
            }
        }

        svTexture.Apply();
        satImage.texture = svTexture;
    }

    public void SetSV(float s, float v)
    {
        currentSat = s;
        currentVal = v;
        UpdateOutputImage();
    }

    public void UpdateSVImage()
    {
        currentHue = hueSlider.value;

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                float value = (float)x / svTexture.width;
                float saturation = 1 - ((float)y / svTexture.height);
                svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue, saturation, value));
            }
        }

        svTexture.Apply();
        UpdateOutputImage();
    }

    private void CreateOutputImage()
    {
        outputTexture = new Texture2D(1, 16);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "OutputTexture";

        Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColor);
        }

        outputTexture.Apply();
        outputImage.texture = outputTexture;
    }

    private void UpdateOutputImage()
    {
        Color currentColor = Color.HSVToRGB(currentHue, currentSat, currentVal);

        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColor);
        }

        outputTexture.Apply();
        changeThisColor.material.SetColor("_BaseColor", currentColor);
    }
}