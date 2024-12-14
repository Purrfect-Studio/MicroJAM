using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControlador : MonoBehaviour
{
    public static AudioControlador Instancia { get; private set; }

    [Header("Mixers de �udio")]
    public AudioMixerGroup grupoPrincipal;
    public AudioMixerGroup grupoMusicas; // Grupo do mixer para m�sicas
    public AudioMixerGroup grupoSFX; // Grupo do mixer para efeitos sonoros

    [Header("Volumes")]
    public float volumeGeralConfiguracao = 1;
    public float volumeMusicaConfiguracao = 1;
    public float volumeEfeitosAudioConfiguracao = 1;

    [Header("Configura��es do Slider")]
    public float valorMaximoSlider = 10f; // Valor m�ximo do slider, deve ser ajustado conforme a configura��o do slider

    private void Awake()
    {
        GerenciarInstancia();
    }

    private void GerenciarInstancia()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instancia != this)
        {
            Destroy(Instancia.gameObject); // Destr�i a inst�ncia antiga em vez de destruir a nova
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Muda o volume da m�sica atrav�s do AudioMixerGroup
    public void MudarVolumeGeral(float valorSlider)
    {
        float volume = Mathf.Lerp(-80f, 0f, valorSlider / valorMaximoSlider); // Converte o valor do slider para o intervalo -80dB a 0dB
        grupoPrincipal.audioMixer.SetFloat("Master", volume);
        volumeGeralConfiguracao = valorSlider / valorMaximoSlider;
    }

    public void MudarVolumeMusica(float valorSlider)
{
    float volume = Mathf.Lerp(-80f, 0f, valorSlider / valorMaximoSlider);
    bool sucesso = grupoMusicas.audioMixer.SetFloat("Soundtrack", volume);

    if (!sucesso)
    {
        Debug.LogError("O parâmetro 'Soundtrack' não foi encontrado no AudioMixer!");
    }

    volumeMusicaConfiguracao = valorSlider / valorMaximoSlider;
}


    // Muda o volume dos efeitos sonoros atrav�s do AudioMixerGroup
    public void MudarVolumeEfeitos(float valorSlider)
    {
        float volume = Mathf.Lerp(-80f, 0f, valorSlider / valorMaximoSlider); // Converte o valor do slider para o intervalo -80dB a 0dB
        grupoSFX.audioMixer.SetFloat("SFX", volume);
        volumeEfeitosAudioConfiguracao = valorSlider / valorMaximoSlider;
    }

    // Configura o slider para mudar o volume da m�sica
    public void ConfigurarSliderVolumeGeral(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeGeral);
        slider.value = volumeGeralConfiguracao * valorMaximoSlider;
    }

    public void ConfigurarSliderVolumeMusica(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeMusica);
        slider.value = volumeMusicaConfiguracao * valorMaximoSlider;
    }

    // Configura o slider para mudar o volume dos efeitos sonoros
    public void ConfigurarSliderVolumeEfeitos(Slider slider)
    {
        slider.onValueChanged.AddListener(MudarVolumeEfeitos);
        slider.value = volumeEfeitosAudioConfiguracao * valorMaximoSlider;
    }

}
