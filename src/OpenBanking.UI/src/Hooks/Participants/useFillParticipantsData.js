export const useFillParticipantsData = () => {
    const fillData = (data) => {
      if(!data){
        return [];
      }

      const dataArray = data.participants.map((item) => {
          return {
            id: item.organizationId,
            status: item.status,
            name: item.name,
            autorizationServers: {
                logoURI: item.autorizationServers[0].logoURI,
                configurationURL: item.autorizationServers[0].configurationURL,
                discoveryAuthorization: item.autorizationServers[0].discoveryAuthorization     
            }
          };
        });

      return dataArray;
    }

    return [fillData];
}