
using UnityEngine;

public class ShaderSwitcher : MonoBehaviour
{
    public Material diffuseMaterial;
    public Material specularMaterial;
    public Material diffuseSpecularMaterial;

    private Renderer _renderer;
    private int _currentShaderIndex = 0;


    void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetShader();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            _currentShaderIndex = (_currentShaderIndex + 1) % 3;
            SetShader();
        }
    }

    void SetShader()
    {
        switch (_currentShaderIndex)
        {
            case 0:
                _renderer.material = diffuseMaterial;
                break;
            case 1:
                _renderer.material = specularMaterial;
                break;
            case 2:
                _renderer.material = diffuseSpecularMaterial;
                break;
        }
    }
}