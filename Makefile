MODULE ?= Cliwrap
COMMAND ?= Run

lib-n-play:
	dotnet new classlib -n NiceShell.$(MODULE) -o `cameled $(MODULE)/lib`
	dotnet new cli-play -n NiceShell.$(MODULE) -o `cameled $(MODULE)/play`
	dotnet add `cameled $(MODULE)/play` reference `cameled $(MODULE)/lib`
	make -C `cameled $(MODULE)/play` -f copaster.Makefile COMMAND=$(COMMAND)
	dotnet sln add `cameled $(MODULE)/play` --in-root
	make -C `cameled $(MODULE)/play` test