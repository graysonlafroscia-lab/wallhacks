using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Wall Hack Script - Makes opponents visible and glowing through walls
/// Attach this script to your camera or player object
/// </summary>
public class WallHack : MonoBehaviour
{
    [Header("Wall Hack Settings")]
    [SerializeField] private bool enableWallHack = true;
    [SerializeField] private Color glowColor = Color.red;
    [SerializeField] private float glowIntensity = 2f;
    [SerializeField] private float outlineWidth = 3f;
    
    [Header("Detection")]
    [SerializeField] private string opponentTag = "Enemy";
    [SerializeField] private float updateInterval = 0.1f;
    
    private List<Renderer> opponentRenderers = new List<Renderer>();
    private Dictionary<Renderer, Material> originalMaterials = new Dictionary<Renderer, Material>();
    private float updateTimer = 0f;
    
    private void Start()
    {
        if (enableWallHack)
        {
            InitializeWallHack();
        }
    }
    
    private void Update()
    {
        if (!enableWallHack) return;
        
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            updateTimer = 0f;
            UpdateOpponentVisibility();
        }
    }
    
    /// <summary>
    /// Initialize wall hack by finding all opponents and setting up materials
    /// </summary>
    private void InitializeWallHack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(opponentTag);
        
        foreach (GameObject enemy in enemies)
        {
            Renderer[] renderers = enemy.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                opponentRenderers.Add(renderer);
                // Store original material for later restoration
                originalMaterials[renderer] = new Material(renderer.material);
            }
        }
    }
    
    /// <summary>
    /// Update opponent visibility with glow effect
    /// </summary>
    private void UpdateOpponentVisibility()
    {
        // Find any newly spawned opponents
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(opponentTag);
        
        foreach (GameObject enemy in allEnemies)
        {
            Renderer[] renderers = enemy.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (!opponentRenderers.Contains(renderer))
                {
                    opponentRenderers.Add(renderer);
                    originalMaterials[renderer] = new Material(renderer.material);
                }
            }
        }
        
        // Apply glow effect to all opponents
        foreach (Renderer renderer in opponentRenderers)
        {
            if (renderer == null) continue;
            
            ApplyGlowEffect(renderer);
            DisableOcclusion(renderer);
        }
    }
    
    /// <summary>
    /// Apply glow effect to make opponent visible through walls
    /// </summary>
    private void ApplyGlowEffect(Renderer renderer)
    {
        Material glowMaterial = new Material(Shader.Find("Standard"));
        glowMaterial.SetColor("_Color", glowColor);
        glowMaterial.SetFloat("_Glossiness", 1f);
        
        // Enable emission for glow
        glowMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
        glowMaterial.EnableKeyword("_EMISSION");
        
        // Make material slightly transparent so it glows through walls
        glowMaterial.SetFloat("_Mode", 3); // Transparent mode
        glowMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        glowMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        glowMaterial.SetInt("_ZWrite", 0);
        glowMaterial.renderQueue = 3000;
        
        renderer.material = glowMaterial;
    }
    
    /// <summary>
    /// Disable occlusion culling so opponents are visible through walls
    /// </summary>
    private void DisableOcclusion(Renderer renderer)
    {
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }
    
    /// <summary>
    /// Toggle wall hack on/off (use with a key press)
    /// </summary>
    public void ToggleWallHack()
    {
        enableWallHack = !enableWallHack;
        
        if (!enableWallHack)
        {
            RestoreOriginalAppearance();
        }
        else
        {
            UpdateOpponentVisibility();
        }
    }
    
    /// <summary>
    /// Restore opponents to their original appearance
    /// </summary>
    private void RestoreOriginalAppearance()
    {
        foreach (var kvp in originalMaterials)
        {
            Renderer renderer = kvp.Key;
            Material originalMaterial = kvp.Value;
            
            if (renderer != null)
            {
                renderer.material = originalMaterial;
            }
        }
    }
    
    /// <summary>
    /// Set the glow color
    /// </summary>
    public void SetGlowColor(Color newColor)
    {
        glowColor = newColor;
    }
    
    /// <summary>
    /// Set the glow intensity
    /// </summary>
    public void SetGlowIntensity(float intensity)
    {
        glowIntensity = intensity;
    }
}
