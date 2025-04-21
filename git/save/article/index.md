# Custom Git Command I Use All The Time!

> It Makes Git 1.47x Times Nicer!

```sh
source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)"

if [ -z "$1" ]; then
    throw "Commit message was not provided"
fi

log "Adding all files to git (git add -A)"
git add -A

log "Commiting changes (git commit -m \"${1}\")"
git commit -m "$1"

CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD)
log "Current branch: $CURRENT_BRANCH"

log "Pushing changes to remote repository (git push --set-upstream origin $CURRENT_BRANCH)"

git push  --set-upstream origin $CURRENT_BRANCH"
```