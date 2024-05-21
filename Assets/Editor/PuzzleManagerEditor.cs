using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(PuzzleManager))]
public class PuzzleManagerEditor : Editor
{
    bool showPepehaPuzzle1 = true;
    bool showPuzzleInfo = true;
    bool showPepehaPuzzle2 = true;
    bool showArtifactPuzzle = true;
    bool showPictureLabelPuzzle = true;

    SerializedProperty door;
    SerializedProperty puzzlesToComplete;
    SerializedProperty puzzle3TextDone;
    SerializedProperty puzzleFields;
    SerializedProperty inputFields;
    SerializedProperty createOwn;
    SerializedProperty artifactSlots;
    SerializedProperty pictureSlots;
    SerializedProperty pictureInputFields;

    void OnEnable()
    {
        door = serializedObject.FindProperty("door");
        puzzlesToComplete = serializedObject.FindProperty("puzzlesToComplete");
        puzzle3TextDone = serializedObject.FindProperty("puzzle3TextDone");
        puzzleFields = serializedObject.FindProperty("puzzleFields");
        inputFields = serializedObject.FindProperty("inputFields");
        createOwn = serializedObject.FindProperty("createOwn");
        artifactSlots = serializedObject.FindProperty("artifactSlots");
        pictureSlots = serializedObject.FindProperty("pictureSlots");
        pictureInputFields = serializedObject.FindProperty("pictureInputFields");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        showPepehaPuzzle1 = EditorGUILayout.Foldout(showPepehaPuzzle1, "Pepeha Puzzle");
        if (showPepehaPuzzle1)
        {
            EditorGUILayout.PropertyField(door);
        }

        showPuzzleInfo = EditorGUILayout.Foldout(showPuzzleInfo, "Puzzle Information");
        if (showPuzzleInfo)
        {
            EditorGUILayout.PropertyField(puzzlesToComplete, true);
            EditorGUILayout.PropertyField(puzzle3TextDone);
        }

        showPepehaPuzzle2 = EditorGUILayout.Foldout(showPepehaPuzzle2, "Pepeha Puzzle");
        if (showPepehaPuzzle2)
        {
            EditorGUILayout.PropertyField(puzzleFields, true);
            EditorGUILayout.PropertyField(inputFields, true);
            EditorGUILayout.PropertyField(createOwn);
        }

        showArtifactPuzzle = EditorGUILayout.Foldout(showArtifactPuzzle, "Artifact Puzzle");
        if (showArtifactPuzzle)
        {
            EditorGUILayout.PropertyField(artifactSlots, true);
        }

        showPictureLabelPuzzle = EditorGUILayout.Foldout(showPictureLabelPuzzle, "Picture Label Puzzle");
        if (showPictureLabelPuzzle)
        {
            EditorGUILayout.PropertyField(pictureSlots, true);
            EditorGUILayout.PropertyField(pictureInputFields, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
