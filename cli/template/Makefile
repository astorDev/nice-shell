ARGS ?= . --name John

run:
	dotnet run -- $(ARGS)

install:
	dotnet pack -c Release
	dotnet tool install --global --source ./bin/Release Template.Cli --allow-downgrade

install-from-nuget:
	dotnet tool install --global Template.Cli

uninstall:
	dotnet tool uninstall --global Template.Cli

reinstall:
	make uninstall || true
	make install