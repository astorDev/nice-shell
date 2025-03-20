## Installing the script
# source .sh # for local testing
source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)"

do_light_work() {
    log "working just a little..."
    ret "some stuff"
}

do_hard_work() {
    log "working hard..."
    log "almost there, working tirelessly..."
    ret "much stuff"

    warn "Don't repeat at home! Oh, no one is listening..."
    throw "fell dead, but no one noticed"
}

do_stupid_work() {
    warn "doing stupid stuff... don't repeat it at home!"
    
    throw "fell dead!"
}

results=$(do_light_work)
echo "results so far: $results"
results+=', '
results+=$(do_hard_work)
echo "results so far: $results"
results+=', '
results+=$(do_stupid_work)
echo "results so far: $results"