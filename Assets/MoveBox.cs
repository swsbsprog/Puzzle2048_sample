using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    public Vector2Int? coord = null;
    public int number;
    public TextMesh text;
    public SpriteRenderer spriteRenderer;
    public Gradient gradient;
    public const int MaxMergeCount = 5;
    int mergeCount = 0;
    public enum BlockType { Color, Sprite}
    public BlockType blockType = BlockType.Color;
    public Sprite[] sprites;

    public void Awake()
    {
        text.gameObject.SetActive(blockType == BlockType.Color);
    }

    internal void SetNumber(int startValue)
    {
        number = startValue;
        switch (blockType)
        {
            case BlockType.Color:
                text.text = number.ToString();
                spriteRenderer.color = gradient.Evaluate((float)mergeCount / MaxMergeCount);
                break;
            case BlockType.Sprite:
                spriteRenderer.sprite = sprites[mergeCount];
                break;
        }

        if (mergeCount == MaxMergeCount)
        {
            Debug.LogWarning("게임 클리어");
            GameObject.Find("Canvas").transform.Find("GameClearButton").gameObject.SetActive(true);
        }
        mergeCount++;
    }

    public enum AnimationType
    {
        Scale,
        Move,
    }
    internal void Move(int x, int y, AnimationType animationType = AnimationType.Move)
    {
        TileManager.isMoving = true;
        Vector2 movePosition = new (x, y);
        if(coord != null)
            TileManager.instance.grid[coord.Value.x, coord.Value.y] = null;
        TileManager.instance.grid[x, y] = this;
        coord = new Vector2Int(x, y);
        switch (animationType)
        {
            case AnimationType.Scale:
                transform.position = movePosition;
                transform.localScale = Vector3.one * 0.8f;
                transform.DOScale(1, 0.3f);
                TileManager.isMoving = false;
                break;
            case AnimationType.Move:
                transform.DOMove(movePosition, 0.3f)
                    .OnComplete(() => TileManager.isMoving = false);
                break;
        }
    }
}
