using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

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

    SerializedProperty keyboardBindings;
    SerializedProperty gamepadBindings;
    SerializedProperty gamepadButton;

    bool showManagers = true;
    bool showUIPanels = true;
    bool showConfirmationExit = true;
    bool showPuzzleUI = true;
    bool showPuzzleInfoUI = true;
    bool showArtifactUI = true;
    bool showPuzzle3UI = true;
    bool showPlayerSettings = true;
    bool showTargetButtons = true;
    bool showControllerSettings = true;
    bool showUIBinding = true;

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

        keyboardBindings = serializedObject.FindProperty("keyboardBindings");
        gamepadBindings = serializedObject.FindProperty("gamepadBindings");
        gamepadButton = serializedObject.FindProperty("gamepadButton");
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

        showConfirmationExit = EditorGUILayout.BeginFoldoutHeaderGroup(showConfirmationExit, "Confirmation Exit");
        if (showConfirmationExit)
        {
            EditorGUILayout.PropertyField(confirmationUI);
            EditorGUILayout.PropertyField(confirmationText);
            EditorGUILayout.PropertyField(yesButton);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showPuzzleUI = EditorGUILayout.BeginFoldoutHeaderGroup(showPuzzleUI, "Puzzle UI");
        if (showPuzzleUI)
        {
            EditorGUILayout.PropertyField(overlayActive);
            EditorGUILayout.PropertyField(room1Puzzle);
            EditorGUILayout.PropertyField(room2Puzzle);
            EditorGUILayout.PropertyField(room3Puzzle);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showPuzzleInfoUI = EditorGUILayout.BeginFoldoutHeaderGroup(showPuzzleInfoUI, "Puzzle 1/2 - Info UI");
        if (showPuzzleInfoUI)
        {
            EditorGUILayout.PropertyField(infoBookArtifact);
            EditorGUILayout.PropertyField(bookArtifactText);
            EditorGUILayout.PropertyField(bookArtifactImage);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showArtifactUI = EditorGUILayout.BeginFoldoutHeaderGroup(showArtifactUI, "Puzzle 2 UI");
        if (showArtifactUI)
        {
            EditorGUILayout.PropertyField(artifactUI1);
            EditorGUILayout.PropertyField(artifactUI2);
            EditorGUILayout.PropertyField(artifactUI3);
            EditorGUILayout.PropertyField(artifactUI4);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showPuzzle3UI = EditorGUILayout.BeginFoldoutHeaderGroup(showPuzzle3UI, "Puzzle 3 UI");
        if (showPuzzle3UI)
        {
            EditorGUILayout.PropertyField(horizontalPictureUI);
            EditorGUILayout.PropertyField(horizontalCurrentImage);
            EditorGUILayout.PropertyField(horizontalImageDescript);
            EditorGUILayout.PropertyField(verticalPictureUI);
            EditorGUILayout.PropertyField(verticalCurrentImage);
            EditorGUILayout.PropertyField(verticalPictureUI);

               EditorGUILayout.PropertyField(pictureUI1);
               EditorGUILayout.PropertyField(pictureUI2);
               EditorGUILayout.PropertyField(pictureUI3);
               EditorGUILayout.PropertyField(pictureUI4);
           }
           EditorGUILayout.EndFoldoutHeaderGroup();

           showPlayerSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showPlayerSettings, "Player Settings");
           if (showPlayerSettings)
           {
               EditorGUILayout.PropertyField(player);
               EditorGUILayout.PropertyField(playerInput);
               EditorGUILayout.PropertyField(playerSprite);
           }
           EditorGUILayout.EndFoldoutHeaderGroup();

           showTargetButtons = EditorGUILayout.BeginFoldoutHeaderGroup(showTargetButtons, "Target Buttons");
           if (showTargetButtons)
           {
               EditorGUILayout.PropertyField(mainMenuTarget);
               EditorGUILayout.PropertyField(optionsTarget);
               EditorGUILayout.PropertyField(acknowledgmentTarget);
               EditorGUILayout.PropertyField(creditsTarget);
               EditorGUILayout.PropertyField(pauseTarget);
               EditorGUILayout.PropertyField(controlsTarget);
               EditorGUILayout.PropertyField(confirmationTarget);

               EditorGUILayout.Space();

               EditorGUILayout.PropertyField(puzzle2Target);
               EditorGUILayout.PropertyField(puzzle3Target);
               EditorGUILayout.PropertyField(dialogueTarget);
               EditorGUILayout.PropertyField(dialogueOptionsTarget);
           }
           EditorGUILayout.EndFoldoutHeaderGroup();

           showControllerSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showControllerSettings, "Needed for Controller");
           if (showControllerSettings)
           {
               EditorGUILayout.PropertyField(isHoldingItem);
               EditorGUILayout.PropertyField(puzzle2IsOpen);
               EditorGUILayout.PropertyField(puzzle3IsOpen);
               EditorGUILayout.PropertyField(selector);
               EditorGUILayout.PropertyField(eventSystem);
           }
           EditorGUILayout.EndFoldoutHeaderGroup();

           showUIBinding = EditorGUILayout.BeginFoldoutHeaderGroup(showUIBinding, "UI for Binding");
           if (showUIBinding)
           {
               EditorGUILayout.PropertyField(keyboardBindings);
               EditorGUILayout.PropertyField(gamepadBindings);
               EditorGUILayout.PropertyField(gamepadButton);
           }
           EditorGUILayout.EndFoldoutHeaderGroup();

           serializedObject.ApplyModifiedProperties();
       }
   }
