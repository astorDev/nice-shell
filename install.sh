#!/bin/bash

REPO_URL="https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/installer/.sh"
INSTALL_PATH="/usr/local/bin/nice-shell.sh"
USER_PROFILE="$HOME/.bash_profile"  # Use .bash_profile for compatibility with macOS and Linux

echo "Downloading the latest functions script..."
curl -s -o "$INSTALL_PATH" "$REPO_URL"

echo "Making the functions script executable..."
chmod +x "$INSTALL_PATH"

if ! grep -q "source $INSTALL_PATH" "$USER_PROFILE"; then
    echo "Adding the source line to $USER_PROFILE"
    echo "\nsource $INSTALL_PATH" >> "$USER_PROFILE"
fi

echo "Installation complete! The profile will be automatically reloaded."

exec bash

log "We are ready to go! (See, the message is already nicer)"
