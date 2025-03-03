source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)"

COMMAND="git add -A"
log "Executing '$COMMAND' (adding current changes to git)"
$COMMAND

COMMAND="git commit -m \"$1\""
log "Executing '$COMMAND' (commiting changes to the current branch)"
$COMMAND


# git add -A && 
# git commit -m $1 && 
# CURRENT_BRANCH=$(git rev-parse --abbrev-ref HEAD) &&
# echo "Current branch: $CURRENT_BRANCH"