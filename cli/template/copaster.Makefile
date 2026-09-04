COMMAND ?= MyCommandName

in-output:
	replace --all-cases CommandName $(COMMAND)
	rm -rf copaster.Makefile
