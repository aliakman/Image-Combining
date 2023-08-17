using System.IO;
using UnityEngine;

public class ImageCombineHandlerWithPixels : MonoBehaviour
{

    [SerializeField] private Texture2D firstImage;
    [SerializeField] private Texture2D secondImage;

    [SerializeField] private Texture2D resultTexture;

    private void Start()
    {
        resultTexture = ManipulateAlphaChannel(firstImage, secondImage);
        var _bytes = resultTexture.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/ManipulatedImage.png", _bytes);

        Debug.Log("T2D file is saved.");
    }



    private Texture2D ManipulateAlphaChannel(Texture2D _firstImage, Texture2D _secImage)
    {
        var _firstData = _firstImage.GetPixels();
        var _secData = _secImage.GetPixels();
        int count = _firstData.Length;
        var _resultData = new Color[count];
        for (int i = 0; i < count; i++)
        {
            Color _firstColor = _firstData[i];
            Color _secColor = _secData[i];
            float _srcF = _secColor.a;
            float _destF = 1f - _secColor.a;
            float _alpha = _srcF + _destF * _firstColor.a;
            Color _resultColor = (_secColor * _srcF + _firstColor * _firstColor.a * _destF) / _alpha;
            _resultColor.a = _alpha;
            _resultData[i] = _resultColor;
        }
        var _result = new Texture2D(_firstImage.width, _firstImage.height);
        _result.SetPixels(_resultData);
        _result.Apply();
        return _result;
    }
}
