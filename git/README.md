```sh
git config --global alias.save '!f() {  
  git add -A &&  
  git commit -m "$1" &&  
  git push --set-upstream origin $(git rev-parse --abbrev-ref HEAD);  
}; f'
```