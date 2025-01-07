#! /bin/bash
LIGHT_CYAN='\033[1;36m'
RED='\033[31m'
NC='\033[0m'

log() {
    printf "${LIGHT_CYAN}${1}${NC}\n" >&2
}

throw() {
    printf "${RED}${1}. Exiting...${NC}\n" >&2 
    exit 1
}

ret() {
    printf %s "${1}"
    printf "\n" >&2
}