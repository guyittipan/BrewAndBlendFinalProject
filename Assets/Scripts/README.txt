
Package: UpdatedUnityPackage
Contains C# scripts aligned to the Proposal/Diagram for Brew & Blend.
Files included:
- IngredientType.cs
- Ingredient.cs
- Recipe.cs (ScriptableObject)
- RecipeDatabase.cs
- IngredientSpawner.cs
- Board.cs (3x3)
- Cell.cs
- MixSystem.cs
- ScoreSystem.cs
- ComboSystem.cs
- RankSystem.cs
- TimerSystem.cs
- GameController.cs

Instructions:
1. Import scripts into Assets/Scripts/
2. Create prefabs for Ingredient types (Coffee, Milk, Caramel, Chocolate) using Ingredient.cs
3. Create Recipe assets via Create->Game->Recipe and add to RecipeDatabase on a GameObject
4. Create a Board GameObject and assign 9 Cell components (UI Buttons or Tiles). Assign IngredientSpawner.
5. Hook up MixSystem, ScoreSystem, ComboSystem, TimerSystem references in the scene.
6. Wire GameController Start/End methods to UI buttons.
