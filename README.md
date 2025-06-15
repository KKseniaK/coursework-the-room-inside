# coursework-the-room-inside  
##  Демоверсия игры в жанре Point-and-Click / Point-and-Click Game Demo

## О проекте / About the Project

**RU:**  
Демонстрационная версия 2D point-and-click игры, созданная на игровом движке Unity в рамках учебного проекта.  
Игроку предстоит взаимодействовать с игровым миром, решать головоломки и следовать короткому сюжету через несколько сцен.  
Главная цель проекта — освоить практические навыки разработки игр: от проектирования логики до написания тестов.

**EN:**  
A 2D point-and-click game demo developed in Unity as an educational project.  
Players interact with the environment, solve puzzles, and follow a short storyline across several scenes.  
The main purpose of this project is to gain practical experience in game development — from logic design to unit testing.

## The Room: Inside — Git Commit Convention

### 1. Commit Message Format
Используем **Conventional Commits** с уточнениями для Unity:
```bash
<тип>(<область>): <сообщение>
```

### 2. Commit Types
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
```
### 3. Scopes (Области)
Группируем по компонентам игры:

```markdown
- `cat`      — механики кота
- `room`     — взаимодействие с комнатой
- `physics`  — коллайдеры, гравитация
- `ui`       — интерфейсы
- `audio`    — звуки и музыка
```
### 4. Примеры
```bash
# С фичей и областью:
git commit -m "feat(room): add destructible objects"

# С исправлением:
git commit -m "fix(cat): correct jump height calculation"

# С ассетом:
git commit -m "asset(audio): add ambient tracks for Level 3"
```
