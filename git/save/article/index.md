# Custom Git Command I Use All The Time!

> Using Git Aliases To Simplify Saving Your Changes

[Makes Git 1.47x Times Nicer!](thumb.png)

Git is awesome, but Git could be cumbersome, as well. It seems like Git developers are well aware of that problem, that's why they introduced a feature, called Git Aliases. In this article, I'm going to introduce you to Git Aliases and guide you on how to create the one I use virtually every day!

> If you just want a quick script for adding the command, jump straight to the [TLDR](#tldr) at the end of this article.

## What is Git Alias?

## The Problem: The Verbosity Of Fully Saving Changes 

1. Add Changes to Git
2. Commit The Changes
3. Push the Changes, Creating a Remote Branch

## Making The Alias

## Improving Transparency with Nice-Shell

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

## TLDR;

