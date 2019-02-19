## Hack Attack 2D Tower Defense

### Before you begin
1. Clone the project
2. Open it in Unity and wait until the initialization is done
3. Untrack `ProjectSettings/ProjectVersion.txt` and `Packages/manifest.json`

### How to untrack files in Git terminal
```bash
git update-index --assume-unchanged <file>
```

To ignore the files in step 3, do this:

```bash
git update-index --assume-unchanged ProjectSettings/ProjectVersion.txt
git update-index --assume-unchanged Packages/manifest.json
```
<b>Important:</b> Make sure the commands are executed in the root directory, `/hack-attack-td`
