COMMAND ?= MyCommandName
PROJECT ?= ProjectName

in-output:
	replace --all-cases CommandName $(COMMAND)
	replace --all-cases ProjectName $(PROJECT)
	rm -rf copaster.Makefile
