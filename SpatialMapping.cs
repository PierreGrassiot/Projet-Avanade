using UnityEngine;
using UnityEngine.XR.WSA;

public class SpatialMapping : MonoBehaviour {

    public static SpatialMapping Instance { private set; get; }

    public static int PhysicsRaycastMask;

    public Material DrawMaterial;

    public bool drawVisualMeshes = false;
    
    // booléen activant/désactivant le spatial mapping 
    private bool mappingEnabled = true;

    // initialisation d'un layer pour les collisions
    private int physicsLayer = 31;

    // renderer associé au maillage du spatial mapping
    private SpatialMappingRenderer spatialMappingRenderer;

    // objet permettant la gestion physique des collisions
    private SpatialMappingCollider spatialMappingCollider;

    // Détermine quand le amillage doit être affiché
    public bool DrawVisualMeshes
    {
        get
        {
            return drawVisualMeshes;
        }
        set
        {
            drawVisualMeshes = value;

            if (drawVisualMeshes)
            {
                spatialMappingRenderer.visualMaterial = DrawMaterial;   // chargement du maillage
                spatialMappingRenderer.renderState = SpatialMappingRenderer.RenderState.Visualization;  // affichage du maillage
            }
            else
            {
                spatialMappingRenderer.renderState = SpatialMappingRenderer.RenderState.None;   // maillage non affiché
            }
        }
    }
 
    // Gère l'activation du rendu et des collisions du maillage
    public bool MappingEnabled
    {
        get
        {
            return mappingEnabled;
        }
        set
        {
            mappingEnabled = value;
            spatialMappingCollider.freezeUpdates = !mappingEnabled;
            spatialMappingRenderer.freezeUpdates = !mappingEnabled;
            gameObject.SetActive(mappingEnabled);
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()    // au lancement
    {
        spatialMappingRenderer = gameObject.GetComponent<SpatialMappingRenderer>(); // récupération du renderer natif associé au spatial mapping
        spatialMappingRenderer.surfaceParent = this.gameObject;
        spatialMappingCollider = gameObject.GetComponent<SpatialMappingCollider>(); // récupération du collider natif associé au spatial mapping
        spatialMappingCollider.surfaceParent = this.gameObject;
        spatialMappingCollider.layer = physicsLayer;
        PhysicsRaycastMask = 1 << physicsLayer;
        DrawVisualMeshes = drawVisualMeshes;
        MappingEnabled = mappingEnabled;
    }
}