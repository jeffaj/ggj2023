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
        [SerializeField]
        private GameObject _stoneTilePrefab = null;
        [SerializeField]
        private GameObject _goalTilePrefab = null;
        [SerializeField]
        private GameObject _fuelTilePrefab = null;
        [SerializeField]
        private GameObject _treasureTilePrefab = null;

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

            return _tiles[col,row];
        }

        public void DestroyTile(Vector2Int gridPosition) {
            this.DestroyTile(gridPosition.x, gridPosition.y);
        }
        public void DestroyTile(int col, int row) {
            Tile tile = this.GetTile(col, row);
            if (tile == null)
                return;
            _tiles[col,row] = null;
            Destroy(tile.gameObject);
        }

        public void Initialize(LevelConfig levelConfig) {
            // clear grid
            this.ResetTiles();

            // left and right walls
            for (int r=0; r < this.Height - 1; r++) {
                int c = 0;
                this.CreateTile<StoneTile>(_stoneTilePrefab, c, r);
                c = this.Width - 1;
                this.CreateTile<StoneTile>(_stoneTilePrefab, c, r);
            }

            // 0th row: goal blocks
            for (int c=1; c < this.Width - 1; c++) {
                int r = 0;
                this.CreateTile<GoalTile>(_goalTilePrefab, c, r);
            }

            // content blocks
            int contentBlocksCount = (this.Width - 2) * (this.Height - 2);
            int stoneBlocksCount = Mathf.FloorToInt(levelConfig.StoneDistribution * contentBlocksCount);
            int fuelBlocksCount = Mathf.FloorToInt(levelConfig.FuelDistribution * contentBlocksCount);
            int artifactCount = levelConfig.ArtifactDatas.Count;
            if (stoneBlocksCount + fuelBlocksCount + artifactCount >= contentBlocksCount * 0.9f) {
                Debug.LogError("too many non-dirt blocks");
                return;
            }

            // distribute stone blocks
            for (int i=0; i < stoneBlocksCount; i++) {
                // find unused space to place tile
                Vector2Int tilePos = GetRandomUnusedContentTile();

                // create tile
                this.CreateTile<StoneTile>(_stoneTilePrefab, tilePos.x, tilePos.y);
            }

            // distribute fuel blocks
            for (int i = 0; i < fuelBlocksCount; i++) {
                // find unused space to place tile
                Vector2Int tilePos = GetRandomUnusedContentTile();

                // create tile
                FuelTile fuelTile = this.CreateTile<FuelTile>(_fuelTilePrefab, tilePos.x, tilePos.y);
                fuelTile.Fuel = levelConfig.FuelPerBlock;
            }

            // treasure blocks
            for (int i=0; i < artifactCount; i++) {
                // find unused space to place tile
                Vector2Int tilePos = GetRandomUnusedContentTile();

                // create tile
                TreasureTile treasureTile = this.CreateTile<TreasureTile>(_treasureTilePrefab, tilePos.x, tilePos.y);
                treasureTile.ArtifactData = levelConfig.ArtifactDatas[i];
            }
            
            // dirt blocks anywhere in the content area where there isn't already a block
            // except for top row, which is reserved for empty space (where player starts)
            for (int r = 0; r < this.Height - 1; r++) {
                for (int c = 0; c < this.Width; c++) {
                    if (this.GetTile(c, r) != null)
                        continue;

                    this.CreateTile<DirtTile>(_dirtTilePrefab, c, r);
                }
            }

            // local method
            Vector2Int GetRandomUnusedContentTile() {
                Vector2Int tilePos = new Vector2Int();
                int contentWidth = this.Width - 2;
                do {
                    int randInt = Random.Range(0, contentBlocksCount);

                    // add 1 for left stone wall
                    tilePos.x = 1 + randInt % contentWidth;
                    // add 1 for goal row
                    tilePos.y = 1 + randInt / contentWidth;
                } while (this.GetTile(tilePos) != null);
                return tilePos;
            }
        }

        private void OnDestroy() {
            this.ResetTiles();
            _tiles = null;
        }

        private T CreateTile<T>(GameObject tilePrefab, int col, int row) where T : Tile {
            T tile = Instantiate(tilePrefab, this.transform).GetComponent<T>();
            tile.transform.localPosition = this.GetLocalPosition(col, row);
            _tiles[col, row] = tile;
            return tile;
        }

        private void ResetTiles() {
            // setup array
            if (_tiles == null) {
                _tiles = new Tile[this.Width, this.Height];
                return;
            }
            // clear array
            for (int c = 0; c < this.Width; c++) {
                for (int r = 0; r < this.Height; r++) {
                    this.DestroyTile(c, r);
                }
            }
        }

        private Tile[,] _tiles = null;
    }
}