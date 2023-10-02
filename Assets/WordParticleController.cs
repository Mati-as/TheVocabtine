using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WordParticleController : MonoBehaviour
{
    
    private ParticleSystem particleSystem;
    public TMP_FontAsset fontAsset;
    public Material textMaterial;
    public string text = "apple";
    private List<GameObject> textObjects = new List<GameObject>();

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    

    void Update()
    {
        // 모든 파티클의 위치 정보를 가져옴
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int particleCount = particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            EmitCharacterParticle(text[i]);
            // 파티클의 위치에 TMP 텍스트 위치 설정
            if (i < textObjects.Count)
            {
                textObjects[i].transform.position = particles[i].position;
               
            }
        }

       
    }

    void EmitCharacterParticle(char character)
    {
        Vector3 emitPosition = this.transform.position;

        // Particle emit 설정
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
        emitParams.position = emitPosition;
        emitParams.startLifetime = Mathf.Infinity;

        GameObject textObject = new GameObject("TMP_Char");
        textObject.transform.position = emitPosition;
        TMP_Text tmpText = textObject.AddComponent<TextMeshPro>();
    
        tmpText.font = fontAsset;
        tmpText.material = textMaterial;
        tmpText.text = character.ToString();
        tmpText.enableAutoSizing = true;
        tmpText.fontSizeMax = 100;
    
        // TMP 객체를 파티클의 자식으로 만들기 (이 부분은 변경하지 않아도 됩니다)
        textObject.transform.SetParent(this.transform);

        // 파티클 발사
        particleSystem.Emit(emitParams, 1);

        // 발사된 TMP 객체를 리스트에 추가
        textObjects.Add(textObject);
    }
}
