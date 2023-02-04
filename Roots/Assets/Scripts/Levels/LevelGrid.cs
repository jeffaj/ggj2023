using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class LevelGrid : MonoBehaviour {

        #region Inspector Fields

        [SerializeField]
        private int _width = 8;
        [SerializeField]
        private int _height = 10;

        [SerializeField]
        private float _tileWidth = 1f;
        [SerializeField]
        private float _tileHeight = 1f;

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _dirtTilePrefab = null;

        #endregion

        public int Width => _width;
        public int Height => _height;

        public Vector3 GetTileLocalPosition(float col, float row) {
            float x = _tileWidth * (col - (this.Width - 1) / 2f);
            float y = _tileHeight * row;
            return new Vector3(x, y, 0);
        }

        public Tile GetTile(int col, int row) {
            if (col < 0 || col >= this.Width ||
                row < 0 || row >= this.Height) {
                Debug.LogError($"Invalid coordinates: {col},{row}");
                return null;
            }

            return _tiles[col][row];
        }

        public void DestroyTile(int col, int row) {
            Tile tile = this.GetTile(col, row);
            if (tile == null)
                return;
            Destroy(tile.gameObject);
        }

        public void Initialize() {
            this.DestroyTiles();

            for (int r = 0; r < this.Height; r++) {
                List<Tile> row = this.CreateRow(r);
                _tiles.Add(row);
            }
        }

        private void Start() {
            this.Initialize();
        }

        private List<Tile> CreateRow(int row) {
            List<Tile> rowTiles = new List<Tile>();
            for (int col=0; col < this.Width; col++) {
                DirtTile tile = Instantiate(_dirtTilePrefab, this.transform).GetComponent<DirtTile>();
                tile.transform.localPosition = this.GetTileLocalPosition(col, row);
                rowTiles.Add(tile);
            }
            return rowTiles;
        }

        private void DestroyTiles() {
            if (_tiles.Count <= 0)
                return;

            for (int c = 0; c < this.Width; c++) {
                for (int r = 0; r < this.Height; r++) {
                    this.DestroyTile(c, r);
                }
            }
            foreach (List<Tile> row in _tiles) {
                row.Clear();
            }
            _tiles.Clear();
        }

        private List<List<Tile>> _tiles = new List<List<Tile>>();
    }
}