using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Floor : MonoBehaviour
{
    #region Inspector
    [Header("Required")]
    [SerializeField] private GameObject targetObject;

    [Header("Optional")]
    [SerializeField] private TilemapRenderer foreground;
    [SerializeField] private TilemapRenderer background;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player) == false)
        {
            return;
        }

        if (foreground != null)
        {
            foreground.sortingLayerName = nameof(foreground).FirstCharacterToUpper();
        }

        if (background != null)
        {
            background.sortingLayerName = nameof(background).FirstCharacterToUpper();
        }

        targetObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}