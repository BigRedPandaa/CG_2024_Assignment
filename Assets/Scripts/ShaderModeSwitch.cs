
using UnityEngine;

public class ShaderSwitcher : MonoBehaviour
{
    public Material diffuseMaterial;
    public Material specularMaterial;
    public Material diffuseSpecularMaterial;
    public Material AmbientMaterial;
    public Material OutlineShader;


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

            _currentShaderIndex = (_currentShaderIndex + 1) % 5;
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
            case 3:
                _renderer.material = AmbientMaterial;
                break;
            case 4:
                _renderer.material = OutlineShader;
                break;
        }
    }
}