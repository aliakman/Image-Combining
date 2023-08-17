using UnityEngine;

public class ComputeShaderImageCombining : MonoBehaviour
{
    public ComputeShader computeShader;
    public Material resultMaterial;
    public Texture2D leftPart;
    public Texture2D rightPart;

    private void Start()
    {
        int _kernelHandle = computeShader.FindKernel("PhotoCombine");

        RenderTexture _resultTexture = new RenderTexture(Screen.width, Screen.height, 0);
        _resultTexture.enableRandomWrite = true;
        _resultTexture.Create();

        computeShader.SetTexture(_kernelHandle, "resultTex", _resultTexture);
        computeShader.SetTexture(_kernelHandle, "leftTex", leftPart);
        computeShader.SetTexture(_kernelHandle, "rightTex", rightPart);
        computeShader.SetFloat("leftWidth", leftPart.width);
        computeShader.SetInt("texWidth", leftPart.width);
        computeShader.SetInt("texHeight", leftPart.height);

        computeShader.Dispatch(_kernelHandle, Screen.width, Screen.height, 1);
        //computeShader.SetTexture(kernel, "Result", renderTexture);

        resultMaterial.mainTexture = _resultTexture;
    }
}
