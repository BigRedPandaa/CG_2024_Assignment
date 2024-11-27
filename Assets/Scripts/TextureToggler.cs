using UnityEngine;

public class TextureToggler : MonoBehaviour
{
    public Material Textured;
    public Material Untextured;

    private Renderer _renderer;
    private int _currentShaderIndex = 0;


    void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetShader();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            _currentShaderIndex = (_currentShaderIndex + 1) % 2;
            SetShader();
        }
    }

    void SetShader()
    {
        switch (_currentShaderIndex)
        {
            case 0:
                _renderer.material = Textured;
                break;
            case 1:
                _renderer.material = Untextured;
                break;
        }
    }
}