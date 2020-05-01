using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile[] inDestructibleTiles;
    [SerializeField] private Tile destructibleTile;

    private bool canExplodeNextCell_Positive = true;
    private bool canExplodeNextCell_Negative = true;

    public void Explode(Vector3 worldPosition, GameObject explosion, int explosionLength = 1)   
    {
        Vector3Int cellOrigin = tilemap.WorldToCell(worldPosition);

        AudioManager.Instance.PlaySFX("Explosion");

        ExplodeCell(cellOrigin, explosion);

        for (int i = 1; i <= explosionLength; i++)
        {
            if (canExplodeNextCell_Positive)
            {
                canExplodeNextCell_Positive = ExplodeCell(cellOrigin + Vector3Int.right * i, explosion);
            }

            if (canExplodeNextCell_Negative)
            {
                canExplodeNextCell_Negative = ExplodeCell(cellOrigin + Vector3Int.right * -i, explosion);
            }
        }

        canExplodeNextCell_Negative = true;
        canExplodeNextCell_Positive = true;
        
        for (int i = 1; i <= explosionLength; i++)
        {
            if (canExplodeNextCell_Positive)
            {
                canExplodeNextCell_Positive = ExplodeCell(cellOrigin + Vector3Int.up * i, explosion);
            }

            if (canExplodeNextCell_Negative)
            {
                canExplodeNextCell_Negative = ExplodeCell(cellOrigin + Vector3Int.up * -i, explosion);
            }
        }

        canExplodeNextCell_Negative = true;
        canExplodeNextCell_Positive = true;
    }

    // Returns whether to explode next cell or not
    private bool ExplodeCell(Vector3Int cell, GameObject explosion)
    {
        Tile currentTile = tilemap.GetTile<Tile>(cell);

        foreach (var tile in inDestructibleTiles)
        {
            if (currentTile == tile)
            {
                return false;
            }
        }

        if (currentTile == destructibleTile)
        {
            tilemap.SetTile(cell, null);

            Vector3 pos = tilemap.GetCellCenterWorld(cell);
            Instantiate(explosion, pos, Quaternion.identity);

            return false;
        }

        Vector3 pos1 = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosion, pos1, Quaternion.identity);
        return true;
    }
}
