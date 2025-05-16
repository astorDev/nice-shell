# Custom Git Command I Use All The Time!

> Using Git Aliases To Simplify Saving Your Changes

[Makes Git 1.47x Times Nicer!](thumb.png)

Git is awesome, but Git can be cumbersome as well. It seems like Git developers are well aware of that problem; that's why they introduced a feature called Git Aliases. In this article, I'm going to introduce you to Git Aliases and guide you on how to create the one I use virtually every day!

> If you just want a quick script for adding the command, jump straight to the [TLDR](#tldr) at the end of this article.

## What is Git Alias?

Git Aliases are a way to extend the `git` command line utils with new commands, perhaps giving an old command a shorter or clearer name. For example, we can create a one-word alias for the command showing us the information about the last commit:

```sh
git config --global alias.last 'log -1 HEAD'
```

Immediately after setting the alias, you will be able to use it as a git command:

```sh
git last
```

Getting in response something like this:

```text
commit 297625e0522f5488f7f923d6a1ece4c4f8ebdab8 (HEAD -> git-save-assets, origin/git-save-assets)
Author: Egor Tarasov <vosarat1995@google.com>
Date:   Thu May 15 22:05:48 2025 +0300

    git save article
```

From a technical perspective, the alias setup updated the `~/.gitconfig` file, adding a row to the `alias` section, like this:

```toml
[alias]
	last = log -1 HEAD
```

As you see, using git aliases, you can do what the name suggests: create a shorthand for a certain Git command you use frequently. However, to get the most out of them we'll need to go slightly beyond that.

## Unlocking Full Git Aliases Potential with ! operator

Let's imagine a situation where you need to perform a git operation, but also need to use a non-git command in a process. This is where the true power of git aliases comes into play. Starting your alias with the `!` operator you can call commands, that do not belong to Git. Let's see this in action:

```sh
git config --global alias.hello '!echo "Hello From Git Alias"'
```

With that in place, you will be able to call `git hello` and get the expected `Hello From Git Alias`.

You can even use shell arguments like this:

```sh
git config --global alias.echo '!echo "from git: $1"'
```

Here's what you will get from calling `git echo one`:

```text
from git: one one
```

Well, it printed what we expected, but for some reason it also duplicated the `one`. This is a peculiar behaviour of git aliases - for some reason, they print the arguments passed after the command output. Gladly, we can work around it by wrapping our script in a function and calling it:

```sh
!f() {

}; f
```

Here's how it will look in our case:

```sh
git config --global alias.echo '!f() {
    echo "from git: $1"
}; f'
```

With the updated setup, running `git echo one` will give us the compact `from git: one`. By the way, wrapping our scripts in functions also helps with complex script constructs like the `if`.

This is enough of the fundamentals. Now, let's jump to building something real!

## Getting Current Branch Name with `current` Alias

```sh
git config --global alias.current 'rev-parse --abbrev-ref HEAD'
```

```sh
git config --global alias.echo-current '!echo "📌 Current Git Branch: $(git current)"'
```

## Making The Alias. Solving The Verbosity Of Fully Saving Changes 

1. Add Changes to Git
2. Commit The Changes
3. Push the Changes, Creating a Remote Branch

```sh
git add --all
```

```sh
git commit --message "$1"
```

```sh
git push --set-upstream origin $(git current)
```

```sh
git config --global alias.save '!f() {
    git add --all
    git commit --message "$1"
    git push --set-upstream origin $(git current)
}; f'
```

## Improving Transparency with Nice-Shell

```sh
source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)"
```

```sh
if [ -z "$1" ]; then
    throw "Commit message was not provided"
fi
```

```sh
log "Adding all files to git (git add --all)"
```

```sh
source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)"

if [ -z "$1" ]; then
    throw "Commit message was not provided"
fi

log "Adding all files to git (git add --all)"
git add --all

log "Committing changes (git commit --message \"${1}\")"
git commit --message "$1"

log "Pushing changes to the remote repository (git push --set-upstream origin $(git current))"
git push  --set-upstream origin $(git current)

log "Changes have been saved successfully ✅" 
```

```sh
git config --global alias.save '!f() { 
    source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)"

    if [ -z "$1" ]; then
        throw "Commit message was not provided"
    fi

    log "Adding all files to git (git add --all)"
    git add --all

    log "Committing changes (git commit --message \"${1}\")"
    git commit --message "$1"

    log "Pushing changes to the remote repository (git push --set-upstream origin $(git current))"
    git push  --set-upstream origin $(git current)

    log "Changes have been saved successfully ✅" 
}; f'
```

```sh
git save "My Changes"
```

`Changes have been saved successfully ✅` printed! This wraps up this article. Let's do a quick recap and see a picture of what the result of our command might look like!

## TLDR;

In this article, we've created a git alias called `save`. It allows us to add, commit, and push changes using just one command. Instead of recreating it, you can install it straight from GitHub with this one-liner:

```sh
curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/main/git/save/install.sh | sh
```

After you see "✅ Installed the `git save` alias", you will be able to use the alias like this:

![](demo.png)

The script, as well as this article, is part of the [nice-shell repository](https://github.com/astorDev/nice-shell), trying to help your shell experience be nicer. Don't hesitate to give the repository a star! ⭐

Claps for this article are also appreciated! 😊
