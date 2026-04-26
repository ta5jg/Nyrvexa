# Nyrvexa — hızlı komutlar (Unity olmadan sim)
.PHONY: sim sim-build

sim: sim-build
	cd tools/SimRunner && dotnet run -c Release --no-build

sim-build:
	cd tools/SimRunner && dotnet build -c Release
