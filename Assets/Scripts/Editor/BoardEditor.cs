using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Board board = (Board)target;
        GUILayout.Space(8);
        if (GUILayout.Button("Auto Assign Cells From Children (3x3)"))
        {
            Undo.RecordObject(board, "Auto Assign Cells From Children");
            board.AutoAssignCellsFromChildren();
            EditorUtility.SetDirty(board);
        }

        if (GUILayout.Button("Refill All Now"))
        {
            Undo.RecordObject(board, "Refill All");
            board.RefillAll();
            EditorUtility.SetDirty(board);
        }
    }
}
