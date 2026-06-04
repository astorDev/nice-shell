run:
	dotnet run -- greet . --name John

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