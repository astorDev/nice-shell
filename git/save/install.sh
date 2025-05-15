git config --global alias.save '!f() { \
    source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)" && \
    if [ -z "$1" ]; then \
        throw "Commit message was not provided"; \
    fi && \
    log "Adding all files to git (git add -A)" && \
    git add -A && \
    log "Commiting changes (git commit -m \"${1}\")" && \
    git commit -m "$1" && \
    log "Pushing changes to remote repository (git push --set-upstream origin $(git current))" && \
    git push --set-upstream origin $(git current); \
}; f'

echo "✅ Installed \`git save\` alias"