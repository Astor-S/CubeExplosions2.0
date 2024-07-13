using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AudioClip _clickSound;

    private AudioSource _audioSource;
    private Ray _ray;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = _clickSound;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ProcessClick();
    }

    public void ProcessClick()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.TryGetComponent(out Cube cube))
            {
                cube.Destroy();
                _audioSource.Play();
            }
        }
    }
}