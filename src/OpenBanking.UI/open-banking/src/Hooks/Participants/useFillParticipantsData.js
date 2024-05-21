export const useFillParticipantsData = () => {
    const fillData = (data) => {
      if(!data){
        return [];
      }
      
      const dataArray = data.map((item) => {
          return {
            id: item.organizationId,
            status: item.status,
            name: item.name,
            autorizationServers: {
                logoURI: item.autorizationServers.logoURI,
                configurationURL: item.autorizationServers.configurationURL,
                discoveryAuthorization: item.autorizationServers.discoveryAuthorization     
            }
          };
        });

      return dataArray;
    }

    return [fillData];
}