using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour
{
    private GridLayoutGroup _gridLayoutGroup;
    private Camera _camera;
    
    void Start()
    {
        _camera = Camera.main;
        
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();

        var sizeCoefficient = (float)_camera.pixelWidth / (float)_camera.pixelHeight;

        _gridLayoutGroup.cellSize = new Vector2(331.5f * sizeCoefficient, 214);
    }
}
