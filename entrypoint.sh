#!/bin/bash
set -e
run_cmd="dotnet merchandise-service.dll --no-build -v d"

dotnet MerchandaiseMigrator.dll --no-build -v d -- --dryrun

dotnet MerchandaiseMigrator.dll --no-build -v d

>&2 echo "Merchandaise Service DB Migrations complete, starting app."
>&2 echo "Run Merchandaise service: $run_cmd"
exec $run_cmd
