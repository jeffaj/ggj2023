using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {
    public class LevelGrid : MonoBehaviour {

        #region Inspector Fields

        [SerializeField]
        private float _tileWidth = 1f;
        [SerializeField]
        private float _tileHeight = 1f;

        [SerializeField]
        private int _width = 8;
        [SerializeField]
        private int _height = 10;

        [SerializeField]
        private Vector2Int _playerStartPos = new Vector2Int(0, 11);

        [Header("Prefabs")]

        [SerializeField]
        private GameObject _dirtTilePrefab = null;

        #endregion

        public int Width => _width;
        public int Height => _height;

        public Vector2Int PlayerStartGridPosition => _playerStartPos;

        public Vector3 GetLocalPosition(Vector2 gridPosition) {
            return this.GetLocalPosition(gridPosition.x, gridPosition.y);
        }

        public Vector3 GetLocalPosition(float col, float row) {
            float x = _tileWidth * (col - (this.Width - 1) / 2f);
            float y = _tileHeight * row;
            return new Vector3(x, y, 0);
        }

        public bool IsValidPosition(Vector2Int gridPosition) {
            return this.IsValidPosition(gridPosition.x, gridPosition.y);
        }
        public bool IsValidPosition(int col, int row) {
            if (col < 0 || col >= this.Width)
                return false;
            if (row < 0 || row >= this.Height)
                return false;
            return true;
        }

        public Tile GetTile(Vector2Int gridPosition) {
            return this.GetTile(gridPosition.x, gridPosition.y);
        }
        public Tile GetTile(int col, int row) {
            if (!this.IsValidPosition(col, row)) {
                Debug.LogError($"Invalid coordinates: {col},{row}");
                return null;
            }

            return _tiles[row][col];
        }

        public void DestroyTile(Vector2Int gridPosition) {
            this.DestroyTile(gridPosition.x, gridPosition.y);
        }
        public void DestroyTile(int col, int row) {
            Tile tile = this.GetTile(col, row);
            if (tile == null)
                return;
            _tiles[row][col] = null;
            Destroy(tile.gameObject);
        }

        public void Initialize() {
            this.DestroyTiles();

            for (int r = 0; r < this.Height - 1; r++) {
                List<Tile> row = this.CreateDirtRow(r);
                _tiles.Add(row);
            }
            _tiles.Add(this.CreateTopRow());
        }

        private void Start() {
            this.Initialize();
        }

        private List<Tile> CreateDirtRow(int row) {
            List<Tile> rowTiles = new List<Tile>();
            for (int col=0; col < this.Width; col++) {
                DirtTile tile = Instantiate(_dirtTilePrefab, this.transform).GetComponent<DirtTile>();
                tile.transform.localPosition = this.GetLocalPosition(col, row);
                rowTiles.Add(tile);
            }
            return rowTiles;
        }
        /// <summary>
        /// Create top row, which is all empty.
        /// </summary>
        private List<Tile> CreateTopRow() {
            List<Tile> rowTiles = new List<Tile>();
            for (int col = 0; col < this.Width; col++) {
                rowTiles.Add(null);
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