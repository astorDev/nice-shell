COMMAND ?= My
PROJECT ?= ProjectName

in-output:
	replace --all-cases Example $(COMMAND)
	replace --all-cases ProjectName $(PROJECT)
	rm -rf copaster.Makefile
