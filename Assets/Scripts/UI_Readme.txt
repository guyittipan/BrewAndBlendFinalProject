
UI wiring quick checklist:
- ScoreText: Text UI showing score (assign to UIManager.scoreText)
- TimerText: Text UI showing remaining seconds (assign to UIManager.timerText)
- ComboText: Text UI showing current combo count (assign to UIManager.comboText)
- Hook UIManager.UpdateScore from ScoreSystem events if you add them. For now GameplayUI calls UpdateAll periodically or on events.
