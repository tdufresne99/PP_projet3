using UnityEngine;

public class RoomLight : MonoBehaviour
{
    [SerializeField] private bool _lightIsOn;
    [SerializeField] private GameObject _fakeLight;
    void Start()
    {
        if (_fakeLight != null) _fakeLight.SetActive(_lightIsOn);
    }

    public bool LightIsOn
    {
        get => _lightIsOn;
        set
        {
            if (_lightIsOn == value) return;
            _lightIsOn = value;
            _fakeLight.SetActive(_lightIsOn);
        }
    }

    private void ToggleLight(bool activate)
    {
        // Lumiere.SetActive(activate);
        string lightState = (activate) ? "allumé" : "fermé";
        Debug.Log("La lumière de la salle " + transform.parent.name + " est maintenant " + lightState);
    }
}