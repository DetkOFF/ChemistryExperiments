using System;
using UnityEngine;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public enum ParticlesType
{
    Water,
    NaSmall,
    NaLarge
}
public class DropSpawner : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform particlesParent;
    
    [SerializeField] private GameObject WaterParticlePrefab;
    [SerializeField] private GameObject NaSmallParticlePrefab;
    [SerializeField] private GameObject NaLargeParticlePrefab;

    private float cooldownWater = 0;
    private float cooldownNaSmall = 0;
    private float cooldownNaLarge = 0;

    public ParticlesType CurrentParticlesType;

    private Vector3 touchPosWorld;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (((Vector2) mainCamera.ScreenToWorldPoint(Input.mousePosition)).y < 3.5f)
            {
                SpawnParticles(CurrentParticlesType);
            }
        }
        else
        {
            if(cooldownWater >= 0)
                cooldownWater -= Time.deltaTime;
            if(cooldownNaSmall >= 0)
                cooldownNaSmall -= Time.deltaTime;
            if(cooldownNaLarge >= 0)
                cooldownNaLarge -= Time.deltaTime;
        }
    }

    public void SetWaterParticles()
    {
        CurrentParticlesType = ParticlesType.Water;
    }
    public void SetNaSmallParticles()
    {
        CurrentParticlesType = ParticlesType.NaSmall;
    }
    public void SetNaLargeParticles()
    {
        CurrentParticlesType = ParticlesType.NaLarge;
    }

    private void SpawnParticles(ParticlesType _type)
    {
        switch (_type)
        {
            case ParticlesType.Water:
                cooldownWater -= Time.deltaTime;
                while (cooldownWater < 0)
                {
                    cooldownWater += 0.01f;
                    Instantiate(WaterParticlePrefab, (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition)+Random.insideUnitCircle*.2f, Quaternion.identity, particlesParent);
                }
                break;
            case ParticlesType.NaSmall:
                cooldownNaSmall -= Time.deltaTime;
                while (cooldownNaSmall < 0)
                {
                    cooldownNaSmall += 0.5f;
                    Instantiate(NaSmallParticlePrefab, (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition)+Random.insideUnitCircle*.2f, Quaternion.identity, particlesParent);
                }
                break;
            case ParticlesType.NaLarge: 
                cooldownNaLarge -= Time.deltaTime;
                while (cooldownNaLarge < 0)
                {
                    cooldownNaLarge += 1.5f;
                    Instantiate(NaLargeParticlePrefab, (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition)+Random.insideUnitCircle*.2f, Quaternion.identity, particlesParent);
                }
                break;
            default: break;
        }
    }
}
