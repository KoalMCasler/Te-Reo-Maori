using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    SerializedProperty gameManager;
    SerializedProperty levelManager;
    SerializedProperty soundManager;
    SerializedProperty puzzleManager;

    SerializedProperty mainMenuUI;
    SerializedProperty acknowledgementUI;
    SerializedProperty gameplayUI;
    SerializedProperty dialogueUI;
    SerializedProperty dialogueUIOptions;
    SerializedProperty nextDialogueButton;
    SerializedProperty creditsUI;
    SerializedProperty controlsUI;
    SerializedProperty pauseUI;
    SerializedProperty optionsUI;
    SerializedProperty endGameUI;
    SerializedProperty projectInfoButton;

    SerializedProperty confirmationUI;
    SerializedProperty confirmationText;
    SerializedProperty yesButton;

    SerializedProperty overlayActive;
    SerializedProperty room1Puzzle;
    SerializedProperty room2Puzzle;
    SerializedProperty room3Puzzle;

    SerializedProperty infoBookArtifact;
    SerializedProperty bookArtifactText;
    SerializedProperty bookArtifactImage;

    SerializedProperty artifactUI1;
    SerializedProperty artifactUI2;
    SerializedProperty artifactUI3;
    SerializedProperty artifactUI4;
    SerializedProperty newArtifact1;
    SerializedProperty newArtifact2;
    SerializedProperty newArtifact3;
    SerializedProperty newArtifact4;

    SerializedProperty horizontalPictureUI;
    SerializedProperty horizontalCurrentImage;
    SerializedProperty horizontalImageDescript;
    SerializedProperty verticalPictureUI;
    SerializedProperty verticalCurrentImage;
    SerializedProperty verticalImageDescript;

    SerializedProperty pictureUI1;
    SerializedProperty pictureUI2;
    SerializedProperty pictureUI3;
    SerializedProperty pictureUI4;

    SerializedProperty player;
    SerializedProperty playerInput;
    SerializedProperty playerSprite;

    SerializedProperty mainMenuTarget;
    SerializedProperty optionsTarget;
    SerializedProperty acknowledgmentTarget;
    SerializedProperty creditsTarget;
    SerializedProperty pauseTarget;
    SerializedProperty controlsTarget;
    SerializedProperty confirmationTarget;
    SerializedProperty puzzle2Target;
    SerializedProperty puzzle3Target;
    SerializedProperty dialogueTarget;
    SerializedProperty dialogueOptionsTarget;

    SerializedProperty isHoldingItem;
    SerializedProperty puzzle2IsOpen;
    SerializedProperty puzzle3IsOpen;
    SerializedProperty selector;
    SerializedProperty eventSystem;
    SerializedProperty isGamepadConnected;

    SerializedProperty keyboardBindings;
    SerializedProperty gamepadBindings;
    SerializedProperty bindingButton;

    bool showManagers = true;
    bool showUIPanels = true;
    bool showConfirmationUI = true;
    bool showPuzzleUI = true;
    bool showArtifactUI = true;
    bool showPictureUI = true;
    bool showPlayerSettings = true;
    bool showControllerSettings = true;
    bool showBindingUI = true;

    void OnEnable()
    {
        gameManager = serializedObject.FindProperty("gameManager");
        levelManager = serializedObject.FindProperty("levelManager");
        soundManager = serializedObject.FindProperty("soundManager");
        puzzleManager = serializedObject.FindProperty("puzzleManager");

        mainMenuUI = serializedObject.FindProperty("MainMenuUI");
        acknowledgementUI = serializedObject.FindProperty("AcknowledgementUI");
        gameplayUI = serializedObject.FindProperty("GameplayUI");
        dialogueUI = serializedObject.FindProperty("DialogueUI");
        dialogueUIOptions = serializedObject.FindProperty("DialogueUIOptions");
        nextDialogueButton = serializedObject.FindProperty("NextDialogueButton");
        creditsUI = serializedObject.FindProperty("CreditsUI");
        controlsUI = serializedObject.FindProperty("ControlsUI");
        pauseUI = serializedObject.FindProperty("PauseUI");
        optionsUI = serializedObject.FindProperty("OptionsUI");
        endGameUI = serializedObject.FindProperty("EndGameUI");
        projectInfoButton = serializedObject.FindProperty("ProjectInfoButton");

        confirmationUI = serializedObject.FindProperty("ConfirmationUI");
        confirmationText = serializedObject.FindProperty("confirmationText");
        yesButton = serializedObject.FindProperty("yesButton");

        overlayActive = serializedObject.FindProperty("overlayActive");
        room1Puzzle = serializedObject.FindProperty("Room1Puzzle");
        room2Puzzle = serializedObject.FindProperty("Room2Puzzle");
        room3Puzzle = serializedObject.FindProperty("Room3Puzzle");

        infoBookArtifact = serializedObject.FindProperty("InfoBookArtifact");
        bookArtifactText = serializedObject.FindProperty("bookArtifactText");
        bookArtifactImage = serializedObject.FindProperty("bookArtifactImage");

        artifactUI1 = serializedObject.FindProperty("ArtifactUI1");
        artifactUI2 = serializedObject.FindProperty("ArtifactUI2");
        artifactUI3 = serializedObject.FindProperty("ArtifactUI3");
        artifactUI4 = serializedObject.FindProperty("ArtifactUI4");
        newArtifact1 = serializedObject.FindProperty("newArtifact1");
        newArtifact2 = serializedObject.FindProperty("newArtifact2");
        newArtifact3 = serializedObject.FindProperty("newArtifact3");
        newArtifact4 = serializedObject.FindProperty("newArtifact4");

        horizontalPictureUI = serializedObject.FindProperty("horizontalPictureUI");
        horizontalCurrentImage = serializedObject.FindProperty("horizontalCurrentImage");
        horizontalImageDescript = serializedObject.FindProperty("horizontalImageDescript");
        verticalPictureUI = serializedObject.FindProperty("verticalPictureUI");
        verticalCurrentImage = serializedObject.FindProperty("verticalCurrentImage");
        verticalImageDescript = serializedObject.FindProperty("verticalImageDescript");

        pictureUI1 = serializedObject.FindProperty("pictureUI1");
        pictureUI2 = serializedObject.FindProperty("pictureUI2");
        pictureUI3 = serializedObject.FindProperty("pictureUI3");
        pictureUI4 = serializedObject.FindProperty("pictureUI4");

        player = serializedObject.FindProperty("player");
        playerInput = serializedObject.FindProperty("playerInput");
        playerSprite = serializedObject.FindProperty("playerSprite");

        mainMenuTarget = serializedObject.FindProperty("mainMenuTarget");
        optionsTarget = serializedObject.FindProperty("optionsTarget");
        acknowledgmentTarget = serializedObject.FindProperty("acknowledgmentTarget");
        creditsTarget = serializedObject.FindProperty("creditsTarget");
        pauseTarget = serializedObject.FindProperty("pauseTarget");
        controlsTarget = serializedObject.FindProperty("controlsTarget");
        confirmationTarget = serializedObject.FindProperty("confirmationTarget");
        puzzle2Target = serializedObject.FindProperty("puzzle2Target");
        puzzle3Target = serializedObject.FindProperty("puzzle3Target");
        dialogueTarget = serializedObject.FindProperty("dialogueTarget");
        dialogueOptionsTarget = serializedObject.FindProperty("dialogueOptionsTarget");

        isHoldingItem = serializedObject.FindProperty("isHoldingItem");
        puzzle2IsOpen = serializedObject.FindProperty("puzzle2IsOpen");
        puzzle3IsOpen = serializedObject.FindProperty("puzzle3IsOpen");
        selector = serializedObject.FindProperty("selector");
        eventSystem = serializedObject.FindProperty("eventSystem");
        isGamepadConnected = serializedObject.FindProperty("isGamepadConnected");

        keyboardBindings = serializedObject.FindProperty("keyboardBindings");
        gamepadBindings = serializedObject.FindProperty("gamepadBindings");
        bindingButton = serializedObject.FindProperty("bindingButton");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        showManagers = EditorGUILayout.BeginFoldoutHeaderGroup(showManagers, "Managers");
        if (showManagers)
        {
            EditorGUILayout.PropertyField(gameManager);
            EditorGUILayout.PropertyField(levelManager);
            EditorGUILayout.PropertyField(soundManager);
            EditorGUILayout.PropertyField(puzzleManager);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showUIPanels = EditorGUILayout.BeginFoldoutHeaderGroup(showUIPanels, "UI Panels");
        if (showUIPanels)
        {
            EditorGUILayout.PropertyField(mainMenuUI);
            EditorGUILayout.PropertyField(acknowledgementUI);
            EditorGUILayout.PropertyField(gameplayUI);
            EditorGUILayout.PropertyField(dialogueUI);
            EditorGUILayout.PropertyField(dialogueUIOptions);
            EditorGUILayout.PropertyField(nextDialogueButton);
            EditorGUILayout.PropertyField(creditsUI);
            EditorGUILayout.PropertyField(controlsUI);
            EditorGUILayout.PropertyField(pauseUI);
            EditorGUILayout.PropertyField(optionsUI);
            EditorGUILayout.PropertyField(endGameUI);
            EditorGUILayout.PropertyField(projectInfoButton);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showConfirmationUI = EditorGUILayout.BeginFoldoutHeaderGroup(showConfirmationUI, "Confirmation UI");
        if (showConfirmationUI)
        {
            EditorGUILayout.PropertyField(confirmationUI);
            EditorGUILayout.PropertyField(confirmationText);
            EditorGUILayout.PropertyField(yesButton);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showPuzzleUI = EditorGUILayout.BeginFoldoutHeaderGroup(showPuzzleUI, "Puzzle UI");
        if (showPuzzleUI)
        {
            EditorGUILayout.PropertyField(overlayActive);
            EditorGUILayout.PropertyField(room1Puzzle);
            EditorGUILayout.PropertyField(room2Puzzle);
            EditorGUILayout.PropertyField(room3Puzzle);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showArtifactUI = EditorGUILayout.BeginFoldoutHeaderGroup(showArtifactUI, "Artifact UI");
        if (showArtifactUI)
        {
            EditorGUILayout.PropertyField(infoBookArtifact);
            EditorGUILayout.PropertyField(bookArtifactText);
            EditorGUILayout.PropertyField(bookArtifactImage);
            EditorGUILayout.PropertyField(artifactUI1);
            EditorGUILayout.PropertyField(artifactUI2);
            EditorGUILayout.PropertyField(artifactUI3);
            EditorGUILayout.PropertyField(artifactUI4);
            EditorGUILayout.PropertyField(newArtifact1);
            EditorGUILayout.PropertyField(newArtifact2);
            EditorGUILayout.PropertyField(newArtifact3);
            EditorGUILayout.PropertyField(newArtifact4);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showPictureUI = EditorGUILayout.BeginFoldoutHeaderGroup(showPictureUI, "Picture UI");
        if (showPictureUI)
        {
            EditorGUILayout.PropertyField(horizontalPictureUI);
            EditorGUILayout.PropertyField(horizontalCurrentImage);
            EditorGUILayout.PropertyField(horizontalImageDescript);
            EditorGUILayout.PropertyField(verticalPictureUI);
            EditorGUILayout.PropertyField(verticalCurrentImage);
            EditorGUILayout.PropertyField(verticalImageDescript);
            EditorGUILayout.PropertyField(pictureUI1);
            EditorGUILayout.PropertyField(pictureUI2);
            EditorGUILayout.PropertyField(pictureUI3);
            EditorGUILayout.PropertyField(pictureUI4);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showPlayerSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showPlayerSettings, "Player Settings");
        if (showPlayerSettings)
        {
            EditorGUILayout.PropertyField(player);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showControllerSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showControllerSettings, "Controller Settings");
        if (showControllerSettings)
        {
            EditorGUILayout.PropertyField(mainMenuTarget);
            EditorGUILayout.PropertyField(optionsTarget);
            EditorGUILayout.PropertyField(acknowledgmentTarget);
            EditorGUILayout.PropertyField(creditsTarget);
            EditorGUILayout.PropertyField(pauseTarget);
            EditorGUILayout.PropertyField(controlsTarget);
            EditorGUILayout.PropertyField(confirmationTarget);
            EditorGUILayout.PropertyField(puzzle2Target);
            EditorGUILayout.PropertyField(puzzle3Target);
            EditorGUILayout.PropertyField(dialogueTarget);
            EditorGUILayout.PropertyField(dialogueOptionsTarget);
            EditorGUILayout.PropertyField(isHoldingItem);
            EditorGUILayout.PropertyField(puzzle2IsOpen);
            EditorGUILayout.PropertyField(puzzle3IsOpen);
            EditorGUILayout.PropertyField(selector);
            EditorGUILayout.PropertyField(eventSystem);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space();

        showBindingUI = EditorGUILayout.BeginFoldoutHeaderGroup(showBindingUI, "Binding UI");
        if (showBindingUI)
        {
            EditorGUILayout.PropertyField(keyboardBindings);
            EditorGUILayout.PropertyField(gamepadBindings);
            EditorGUILayout.PropertyField(bindingButton);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}