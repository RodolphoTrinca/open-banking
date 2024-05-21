import { useAxios } from "../Axios/useAxios";
import axios from "../../Apis/openBanking"
import { useFillParticipantsData } from './useFillParticipantsData';

export const useGetParticipants = () => {
    const [fillData] = useFillParticipantsData();
    const [response, error, loading, fetch] = useAxios({
        axiosInstance: axios,
        method: 'GET',
        url: '/participants'
    }, fillData);
    
    return [response, error, loading, fetch];
}