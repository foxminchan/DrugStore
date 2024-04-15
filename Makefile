include .env
export

.PHONY: publish
publish-all-dockers: publish-api publish-backoffice publish-storefront publish-identityserver

.PHONY: publish-api
publish-api:
	dotnet publish ./src/DrugStore.WebAPI/DrugStore.WebAPI.csproj --os linux --arch x64 /t:PublishContainer -c Release
	docker tag drugstore-webapi:latest ghcr.io/foxminchan/drugstore/drugstore-webapi:${VERSION}
	docker rmi drugstore-webapi:latest
	docker push ghcr.io/foxminchan/drugstore/drugstore-webapi:${VERSION}

.PHONY: publish-backoffice
publish-backoffice:
	dotnet publish ./src/DrugStore.Backoffice/DrugStore.Backoffice.csproj --os linux --arch x64 /t:PublishContainer -c Release
	docker tag drugstore-backoffice:latest ghcr.io/foxminchan/drugstore/drugstore-backoffice:${VERSION}
	docker rmi drugstore-backoffice:latest
	docker push ghcr.io/foxminchan/drugstore/drugstore-backoffice:${VERSION}

.PHONY: publish-storefront
publish-storefront:
	dotnet publish ./src/DrugStore.Storefront/DrugStore.Storefront.csproj --os linux --arch x64 /t:PublishContainer -c Release
	docker tag drugstore-storefront:latest ghcr.io/foxminchan/drugstore/drugstore-storefront:${VERSION}
	docker rmi drugstore-storefront:latest
	docker push ghcr.io/foxminchan/drugstore/drugstore-storefront:${VERSION}

.PHONY: publish-identityserver
publish-identityserver:
	dotnet publish ./src/DrugStore.IdentityServer/DrugStore.IdentityServer.csproj --os linux --arch x64 /t:PublishContainer -c Release
	docker tag drugstore-identityserver:latest ghcr.io/foxminchan/drugstore/drugstore-identityserver:${VERSION}
	docker rmi drugstore-identityserver:latest
	docker push ghcr.io/foxminchan/drugstore/drugstore-identityserver:${VERSION}
