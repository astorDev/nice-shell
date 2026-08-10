T ?= template

install-template:
	dotnet new install $(T)

uninstall-template:
	dotnet new uninstall $(T)

reinstall-template:
	make uninstall-template
	make install-template

paste:
	cp -r $(T) $(T)-copy
	make -C $(T)-copy -f copaster.Makefile COMMAND=TestName

clean:
	rm -rf $(T)-copy