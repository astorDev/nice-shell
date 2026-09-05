#! /bin/bash
FOREGROUND_MAXED_CYAN="\x1B[38;2;0;255;255m"
FOREGROUND_MAXED_RED="\x1B[38;2;255;0;0m"
FOREGROUND_MAXED_BLUE="\x1B[38;2;0;0;255m"
FOREGROUND_MAXED_YELLOW="\x1B[38;2;255;255;0m"
FOREGROUND_RESET="\x1B[39m"

log() {
    printf "${FOREGROUND_MAXED_CYAN}${1}${FOREGROUND_RESET}\n" >&2
}

throw() {
    printf "${FOREGROUND_MAXED_RED}${1}. Exiting...${FOREGROUND_RESET}\n" >&2 
    exit 1
}

warn() {
    printf "${FOREGROUND_MAXED_YELLOW}⚠️ ${1}${FOREGROUND_RESET}\n" >&2 
}

ret() {
    printf %s "${1}"
    printf "\n" >&2
    # exit 0
}