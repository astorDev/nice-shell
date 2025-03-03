```sh
git config --global alias.save '!f() {  
    source /dev/stdin <<< "$(curl -sS https://raw.githubusercontent.com/astorDev/nice-shell/refs/heads/main/.sh)"

  git add -A &&  
  git commit -m "$1" &&  
  git push --set-upstream origin $(git rev-parse --abbrev-ref HEAD);  
}; f'
```