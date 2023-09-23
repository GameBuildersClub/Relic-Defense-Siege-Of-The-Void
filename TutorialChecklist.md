# Fall 2023 Game Builder's Club Tutorial Game

Relic Defense: Siege of the Void  

# Step-by-Step
## Introduction: The Basics
1. Introduce Unity UI: Hierarchy, Scene View, Game View, Inspector, Asset Menu.
1. Explain GameObjects and Transforms (empty GameObjects)
1. Add a default sprite to scene
1. Explain overview of relevant parts of code: variables, properties, methods, objects/classes. 
1. Explain visibility ("public", "protected", "" (package private), "private") and SerializeField for variables in inspector: default to protected
1. Create a test script: explain Monobehaviour methods (awake, start, update, fixedupdate)
1. Add basic code to each part of the script to indicate use

## Introduction: Moving a Character
1. Show character sprites: show Sprite inspector menu and add to scene
1. Add sprites to scene to create animations
1. Put sprites under player GameObject (Player > Visuals > PlayerSprite)
1. Add Rigidbody for Player GameObject (this is for moving with the physics engine): gravity scale is 0
1. Add PlayerController script: void Move(Vector2 direction)
1. Introduce package manager: install new Unity Input System - install and restart editor to change backends.
1. Create Input Actions (ScriptableObjects > Input > Controls): Add control scheme (keyboard), add action map (character), add move action (value, vector2, add up/down/left/right composite binding), save asset, check generate C# class.
1. Add PlayerInput component to player GameObject: Select Input Action (Controls) for PlayerInput's Actions, select (under Behavior) "Invoke Unity Events".
1. Add under Move event the method from PlayerController, change parameter to InputAction.CallbackContext and call .GetValue<Vector2>() on input.