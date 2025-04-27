# coursework-the-room-inside

# The Room: Inside — Git Commit Convention

## 1. Commit Message Format
Используем **Conventional Commits** с уточнениями для Unity:
```bash
<тип>(<область>): <сообщение>
```

## 2. Commit Types
```markdown
| Type       | Description                          | Example                          |
|------------|--------------------------------------|----------------------------------|
| `feat`     | Добавление новой функциональности    | `feat(cat): double jump ability` |
| `fix`      | Исправление ошибок                   | `fix(physics): wall clipping`    |
| `refactor` | Изменения кода без новых функций     | `refactor(ui): menu system`      |
| `docs`     | Обновление документации              | `docs: add API reference`        |
| `style`    | Изменения форматирования             | `style: code indentation`        |
| `test`     | Добавление тестов                    | `test: inventory tests`          |
| `chore`    | Вспомогательные изменения            | `chore: update dependencies`     |
| `asset`    | Работа с ассетами                    | `asset(sound): add meow FX`      |
| `ci`       | Настройки CI/CD                      | `ci: add GitHub Actions`         |
| `build`    | Сборка проекта                       | `build: Android support`         |
```
## 3. Scopes (Области)
Группируем по компонентам игры:

```markdown
- `cat`      — механики кота
- `room`     — взаимодействие с комнатой
- `ai`       — ИИ врагов/NPC
- `physics`  — коллайдеры, гравитация
- `ui`       — интерфейсы
- `audio`    — звуки и музыка
```
4. Примеры
```bash
# С фичей и областью:
git commit -m "feat(room): add destructible objects"

# С исправлением:
git commit -m "fix(cat): correct jump height calculation"

# С ассетом:
git commit -m "asset(audio): add ambient tracks for Level 3"
```
