using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[System.Serializable]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    float speedTarget = 1.0f;
    public static float[] _samples = new float[512];
    public static float[] _frequencyBand = new float[8];
    public static bool _offBeat = false;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        
        // gradually change the audioSource pitch to the speedTarget value
        _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, speedTarget, Time.deltaTime * 5.0f);

        //change _offBeat value when the bass is hit
        if (_frequencyBand[1] > 0.10){
            _offBeat = !_offBeat;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerPitchChange();
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void TriggerPitchChange()
    {
        speedTarget = Random.Range(0.5f, 1.5f);
    }

    public void ChangePitch(float pitch)
    {
        speedTarget = pitch;
    }

    void MakeFrequencyBands(){
        int count = 0;
        for (int i = 0; i < 8; i++){
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7){
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++){
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;
            _frequencyBand[i] = average * 10;
        }
    }
}
