MODULE ?= CliWrap
COMMAND ?= Run

lib-n-play:
	dotnet new classlib -n NiceShell.$(MODULE) -o `kebabed $(MODULE)/lib`
	dotnet new cli-play -n NiceShell.$(MODULE) -o `kebabed $(MODULE)/play`
	dotnet add `kebabed $(MODULE)/play` reference `kebabed $(MODULE)/lib`
	make -C `kebabed $(MODULE)/play` -f copaster.Makefile COMMAND=$(COMMAND)
	dotnet sln add `kebabed $(MODULE)/play` --in-root
	make -C `kebabed $(MODULE)/play` test

feature-branch:
	git switch --create $(BRANCH)

pr:
	git save "$(TITLE)"
	gh pr create --title "$(TITLE)" --body "" || true
	gh pr view --web

post-pr:
	sh ./small/switch-and-delete/.sh
	git pull