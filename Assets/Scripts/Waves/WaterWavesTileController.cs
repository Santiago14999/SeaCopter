using System;
using UnityEngine;

public class WaterWavesTileController : MonoBehaviour
{
    private enum Direction { Right, Top, Left, Bottom } 

    [Tooltip("Player transform around which tiles will arrange.")]
    [SerializeField] private Transform _player;
    [Tooltip("There should be 9 water planes with WaterWavesTile attached to them.")]
    [SerializeField] private WaterWavesTile[] _tiles;
    [Tooltip("Width or height of single square tile.")]
    [SerializeField] private float _tileSize;

    private Vector2 _currentOrigin;

    private void Start()
    {
        if (_tiles.Length != 9)
            Debug.LogError("There should be 9 water planes with WaterWavesTile attached to them.");

        for (int i = 0; i < _tiles.Length; i++)
            _tiles[i].TileIndex = i;
    }

    private void Update()
    {
        if (_player.position.x - _currentOrigin.x > _tileSize * .5f)
            MoveTiles(Direction.Right);

        if (_player.position.x - _currentOrigin.x < -_tileSize * .5f)
            MoveTiles(Direction.Left);

        if (_player.position.z - _currentOrigin.y > _tileSize * .5f)
            MoveTiles(Direction.Top);

        if (_player.position.z - _currentOrigin.y < -_tileSize * .5f)
            MoveTiles(Direction.Bottom);
    }

    private void MoveTiles(Direction direction)
    {
        if (direction == Direction.Right)
        {
            _currentOrigin.x += _tileSize;
            for (int i = 0; i < 3; i++)
                _tiles[i * 3].Translate(_tileSize * 3, 0, 0);

            for (int i = 0; i < 9; i++)
                _tiles[i].TileIndex += (i % 3 == 0) ? 2 : -1;
        }
        else if (direction == Direction.Left)
        {
            _currentOrigin.x -= _tileSize;
            for (int i = 0; i < 3; i++)
                _tiles[3 * i + 2].Translate(-_tileSize * 3, 0, 0);

            for (int i = 0; i < 9; i++)
                _tiles[i].TileIndex += (i % 3 == 2) ? -2 : 1;
        }
        else if (direction == Direction.Top)
        {
            _currentOrigin.y += _tileSize;
            for (int i = 6; i < 9; i++)
                _tiles[i].Translate(0, 0, _tileSize * 3);

            for (int i = 0; i < 9; i++)
                _tiles[i].TileIndex += (i > 5) ? -6 : 3;
        }
        else if (direction == Direction.Bottom)
        {
            _currentOrigin.y -= _tileSize;
            for (int i = 0; i < 3; i++)
                _tiles[i].Translate(0, 0, -_tileSize * 3);

            for (int i = 0; i < 9; i++)
                _tiles[i].TileIndex += (i < 3) ? 6 : -3;
        }

        Array.Sort(_tiles);
    }
}
