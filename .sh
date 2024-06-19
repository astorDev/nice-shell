#! /bin/bash
LIGHT_CYAN='\033[1;36m'
RED='\033[31m'
NC='\033[0m'

log() {
    echo >&2 "${LIGHT_CYAN}${1}${NC}"
}

throw() {
    echo >&2 "${RED}${1}. Exiting...${NC}"
    exit 1
}

ret() {
    echo $1
}